using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Parser.Core
{
    public class FilesLoader
    {
        public static async Task DownloadFileAsync(string imgPath, string newPath)
        {
            using (WebClient client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(imgPath), newPath);
            }
        }
    }
}