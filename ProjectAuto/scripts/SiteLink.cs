﻿using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AngleSharp;
using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.XPath;
using System.Windows.Forms;

namespace ProjectAuto
{
    class SiteLink
    {
        ConnectDB connectDB = new ConnectDB();
        string pathToImage = @"D:\Works Projects\ProjectAuto\ProjectAuto\imageAuto\";


        // получение страницы возвращение ответа
        #region GetPage

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

        #endregion

        // Парсинг страницы автокаталога 
        #region ParsAutoCatalog

        public List<Automobile> ParsAutoCatalog(string response)
        {
            HtmlParser htmlParser = new HtmlParser();
            var doc = htmlParser.ParseDocument(response);
            List<Automobile> product = new List<Automobile>();

            List<string> linkImg = new List<string>();
            int CountID = 0;
            foreach (var item in doc.QuerySelectorAll(".catalog-models>>li"))
            {
                product.Add(new Automobile
                {
                    ID = ++CountID,
                    nameAuto = item.QuerySelector(".model_item").TextContent,
                    autoLink = "www.avtoall.ru" + item.QuerySelector(".model_item") == null ? "" : "www.avtoall.ru" + item.QuerySelector(".model_item").GetAttribute("href"),
                    catalogYears = item.QuerySelector("b>u") == null ? "" : item.QuerySelector("b>u").TextContent,
                    productInStock = item.QuerySelector("b>ins") == null ? "" : item.QuerySelector("b>ins").TextContent,
                    model = item.QuerySelector("b>small") == null ? "" : item.QuerySelector("b>small").TextContent,
                    ImgLink = item.QuerySelector("img").GetAttribute("src"), //сохраняет ссылки на картинки
                    imgPath = pathToImage + item.QuerySelector("img").GetAttribute("src").Remove(0, item.QuerySelector("img").GetAttribute("src").Length - 6)
                });
            }


            List<CategoryRepairPart> CategoryRepairPart = new List<CategoryRepairPart>();

            int CatalogPartCount = doc.QuerySelectorAll(".catalog-groups-tree>li").Count();

            for (int i = 1; i < CatalogPartCount; i++)
            {
                var CatalogName = doc.DocumentElement.SelectSingleNode($"//*[@id='autoparts_tree']/ul/li[{i}]/a");
                var SubNamePart = doc.DocumentElement.SelectSingleNode($"//*[@id='autoparts_tree']/ul/li[{1}]/ul");
                CategoryRepairPart.Add(new CategoryRepairPart { categoryName = CatalogName.TextContent });
            }


            return product;

        }

        #endregion

        // загрузка картинки по ссылке
        #region DownloadFileAsync

        public void DownloadFileAsync(string s)
        {
            WebClient client = new WebClient();
            client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:70.0) Gecko/20100101 Firefox/70.0";
            client.Headers["Accept-Encoding"] = "gzip, deflate, br";
            client.Headers["Accept"] = "image/webp,*/*";
            client.DownloadFileTaskAsync(new Uri(s), @"D:\Works Projects\ProjectAuto\ProjectAuto\imageAuto\" + s.Remove(0, s.Length - 6));
            
        }

        #endregion

        // Парсинг всех ссылок на авто с автозапчастями
        #region ParsCategoryRepairPart

        public List<CategoryRepairPart> ParsCategoryRepairPart(string response)
        {
            HtmlParser htmlParser = new HtmlParser();

            var doc = htmlParser.ParseDocument(response);
            // add part in catalog
            List<CategoryRepairPart> parts = new List<CategoryRepairPart>();

            foreach (var item in doc.QuerySelectorAll(".catalog-groups-tree>li"))
            {
                parts.Add(new CategoryRepairPart
                {
                    categoryName = item.QuerySelector("a").TextContent,
                });

            }
            return parts;

        }

        #endregion


        public List<CategoryRepairPart> ParsSubRepairPart(string response)
        {
            HtmlParser htmlParser = new HtmlParser();
            var doc = htmlParser.ParseDocument(response);

            // add part in catalog  
            List<CategoryRepairPart> CategoryRepairPart = new List<CategoryRepairPart>();

            int CatalogPartCount = doc.QuerySelectorAll(".catalog-groups-tree>li").Count();

            for (int i = 1; i < CatalogPartCount; i++)
            {
                var CatalogName = doc.DocumentElement.SelectSingleNode($"//*[@id='autoparts_tree']/ul/li[{i}]/a");
                var SubNamePart = doc.DocumentElement.SelectSingleNode($"//*[@id='autoparts_tree']/ul/li[{1}]/ul");
                CategoryRepairPart.Add(new CategoryRepairPart { categoryName = CatalogName.TextContent });
            }
            //var items = doc.DocumentElement.SelectSingleNode("//*[@id='autoparts_tree']/ul/li[1]/a");
            //CategoryRepairPart.Add(new SubRepairParts { nameSubPart = items.TextContent });
            //foreach (var item in items)
            {

                //var selector = "*";//ссылки на каталоги запчастей
                //CategoryRepairPart.Add(new SubRepairParts { nameSubPart = item.TextContent });
                //CategoryRepairPart.Add(new SubRepairParts
                //{  

                //    //nameSubPart = item.QuerySelector(selectors)
                //    //namePart = item.QuerySelector(selector).InnerHtml,
                //});


                //var selectorAll = "li>ul>li";//subprt

                //var a = item.QuerySelectorAll(selector);

                //foreach (var s in a)
                //{
                //    parts.Add(new SubRepairParts
                //    {
                //        //nameSubPart = s.InnerHtml,
                //        nameSubPart = s.QuerySelector(selectorAll).InnerHtml,
                //    });
                //}
                //parts.Add(new SubRepairParts
                //{
                //    //nameSubPart = item.InnerHtml,
                //    nameSubPart = item.QuerySelectorAll(selector),
                //});
            }





            return CategoryRepairPart;

        }
    }
}
