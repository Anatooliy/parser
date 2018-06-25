using AngleSharp.Parser.Html;
using Parser.Core;
using Parser.Core.Site;
using Parser.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Parser.Controllers
{
    public class HomeController : Controller
    {
        ParserContext db = new ParserContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product book = db.Products.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Parse()
        {            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ParseStart()
        {
            int start = 1;
            int end = 1;

            if(Request.Params["start"] != null)
            {
                start = Int32.Parse(Request.Params["start"]);
            }

            if (Request.Params["end"] != null)
            {
                end = Int32.Parse(Request.Params["end"]);
            }
                             
            List<Product> newProductsList = new List<Product>();

            ParserWorker<List<Product>> parser = new ParserWorker<List<Product>>(
                   new SiteParser(),
                   new SiteSettings(start, end)
               );           

            for (int i = parser.Settings.StartPoint; i <= parser.Settings.EndPoint; i++)
            {
                var source = await parser.Loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();
                var document = await domParser.ParseAsync(source);
                var result = parser.Parser.Parse(document);

                newProductsList.AddRange(result);                
            }
            
            foreach (var product in newProductsList)
            {
                Product matches = db.Products.FirstOrDefault(p => p.Name == product.Name);
                
                if (matches == null)
                {
                    string imgPath = parser.Settings.BaseUrl + product.ImgUrl;
                    string fileName = System.IO.Path.GetFileName(imgPath);
                    string newPath = Server.MapPath("~/Images/Catalog/") + System.IO.Path.GetFileName(fileName);

                    await FilesLoader.DownloadFileAsync(imgPath, newPath);
                    product.ImgName = fileName;

                    db.Products.Add(product);
                }
            }           
             
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}