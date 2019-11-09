using AngleSharp.Html.Parser;
using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAuto
{
    class SiteLink
    {
        List<string> linkImg;

        public string GetPage(string link)
        {
            HttpRequest request = new HttpRequest();
            request.AddHeader("Accept:", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            request.AddHeader("Accept-Encoding:", "gzip, deflate, br");
            request.AddHeader("Accept-Language:", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.AddHeader("User-Agent:", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:70.0) Gecko/20100101 Firefox/70.0");
            request.AddHeader("Host", "www.avtoall.ru");

            request.KeepAlive = true;
            request.UserAgent = Http.ChromeUserAgent();

            string response = request.Get(link).ToString();

            return response;

        }

        public List<Automobile> ParsAutoCatalog(string response)
        {
            HtmlParser htmlParser = new HtmlParser();
            var doc = htmlParser.ParseDocument(response);

            List<Automobile> product = new List<Automobile>();
            linkImg = new List<string>();

            foreach (var item in doc.QuerySelectorAll(".catalog-models>>li"))
            {
                product.Add(new Automobile
                {
                    nameAuto = item.QuerySelector(".model_item").TextContent,
                    img = item.QuerySelector("img").GetAttribute("src"),
                    linkAuto = item.QuerySelector(".model_item") == null ? "" : item.QuerySelector(".model_item").GetAttribute("href"),
                    catalogYears = item.QuerySelector("b>u") == null ? "" : item.QuerySelector("b>u").TextContent,
                    productInStock = item.QuerySelector("b>ins") == null ? "" : item.QuerySelector("b>ins").TextContent,
                    model = item.QuerySelector("b>small") == null ? "" : item.QuerySelector("b>small").TextContent,

                });
                linkImg.Add(item.QuerySelector("img").GetAttribute("src"));
            }
            //DownloadFileAsync().GetAwaiter();
            return product;

        }
      
        async Task DownloadFileAsync()
        {
            
            foreach (var item in linkImg)
            {
                WebClient client = new WebClient();
                client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:70.0) Gecko/20100101 Firefox/70.0";
                client.Headers["Accept-Encoding"] = "gzip, deflate, br";
                client.Headers["Accept"] = "image/webp,*/*";
                Thread.Sleep(1000);
                await client.DownloadFileTaskAsync(new Uri(item), @"D:\Works Projects\ProjectAuto\ProjectAuto\scripts\imageAuto\" + item.Remove(0, item.Length - 6)); 
            }

        }
    }
}
