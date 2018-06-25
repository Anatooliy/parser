using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using Parser.Models;

namespace Parser.Core.Site
{
    class SiteParser : IParser<List<Product>>
    {
        public List<Product> Parse(IHtmlDocument document)
        {
            var list  = new List<Product>();
            var items = document.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("catalog-item-info"));
            
            foreach(var item in items)
            {                
                
                list.Add(new Product
                    {
                        Name = item.QuerySelector(".item-title").TextContent,
                        Author = item.QuerySelector(".article").TextContent,
                        Description = item.QuerySelector(".item-desc").TextContent,
                        Price = Int32.Parse(item.QuerySelector(".catalog-item-price-value").TextContent.Replace(" ", "")),
                        ImgUrl = item.QuerySelector(".item_img").GetAttribute("src")                    
                    }
                );                
            }

            return list;
        }
    }
}
