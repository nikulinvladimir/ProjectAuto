using AngleSharp.Html.Parser;
using AngleSharp.XPath;
using AngleSharp.Html;
using AngleSharp;
using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.IO;
using System.Linq;
using AngleSharp.Dom;
using System.Threading;
using System.Xml.XPath;

namespace ProjectAuto
{
    class Parser
    {
        ConnectDB connectDB = new ConnectDB();
        string pathToImage = @"D:\Works Projects\ProjectAuto\ProjectAuto\imageAuto\";
        const string SITEURL = "www.avtoall.ru";
        public List<SubCategoryParts> listSubCetegoryParts;
        public List<RepairPart> listRerairParts;
        public List<CategoryPart> listCategoryPart;

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

        public List<Automobile> ParsAutoCatalog(string link)
        {
            string response = GetPage(link);
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
                    autoLink = SITEURL + item.QuerySelector(".model_item") == null ? "" : SITEURL + item.QuerySelector(".model_item").GetAttribute("href"),
                    catalogYears = item.QuerySelector("b>u") == null ? "" : item.QuerySelector("b>u").TextContent,
                    productInStock = item.QuerySelector("b>ins") == null ? "" : item.QuerySelector("b>ins").TextContent,
                    model = item.QuerySelector("b>small") == null ? "" : item.QuerySelector("b>small").TextContent,
                    ImgLink = item.QuerySelector("img").GetAttribute("src"), //сохраняет ссылки на картинки
                    imgPath = @"D:\Works Projects\ProjectAuto\ProjectAuto\imageAuto\" + item.QuerySelector("img").GetAttribute("src").Remove(0, item.QuerySelector("img").GetAttribute("src").Length - 6)
                });
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
        int idCountCategory = 0;
        static int idCountSubCategory = 0;
        int idRepairPart = 0;
        public async void ParsCategoryRepairPart(string link, int idAuto)
        { 
            string response = GetPage(link);
            HtmlParser htmlParser = new HtmlParser();
            var doc = htmlParser.ParseDocument(response);


            var config = Configuration.Default.WithXPath();
            var context = BrowsingContext.New(config);
            var d = await context.OpenAsync(res => res.Content(response));
            

            listCategoryPart = new List<CategoryPart>();
            listSubCetegoryParts = new List<SubCategoryParts>();
            listRerairParts = new List<RepairPart>();




            var nodes = doc.DocumentElement.SelectNodes("//*[@id='autoparts_tree']/ul/li");  
            if (nodes != null)
            {
                int CountCategory = 0;
                foreach (IElement item in nodes)
                {
                    idCountCategory++;
                    CountCategory++;                   
                   var CatalogName = item.SelectSingleNode($"//*[@id='autoparts_tree']/ul/li[{CountCategory}]/a");

                    listCategoryPart.Add(new CategoryPart
                    {
                        id = idCountCategory,
                        categoryName = CatalogName.TextContent,
                        autoId = idAuto,
                    });

                    Thread.Sleep(500);

                    int CountSubCategory = 0;
                    foreach (IElement subItem in item.SelectNodes($"//*[@id='autoparts_tree']/ul/li[{CountCategory}]/ul/li"))
                    {
                        idCountSubCategory++;
                        CountSubCategory++;
                        var SubCatalogName = subItem.SelectSingleNode($"//*[@id='autoparts_tree']/ul/li[{CountCategory}]/ul/li[{CountSubCategory}]/a/text()");
                        var productInStock = subItem.SelectSingleNode($"//*[@id='autoparts_tree']/ul/li[{CountCategory}]/ul/li[{CountSubCategory}]/a/em/text()");

                        listSubCetegoryParts.Add(new SubCategoryParts
                        {
                            id = idCountSubCategory,
                            nameSubCategoryPart = SubCatalogName.TextContent,
                            categoryId = idCountCategory,
                            countProductinStock = productInStock == null ? "" : productInStock.TextContent,
                        });

                        int CountPepairPart = 0;
                        foreach (IElement repairPart in item.SelectNodes($"//*[@id='autoparts_tree']/ul/li[{CountCategory}]/ul/li[{CountSubCategory}]/ul/li"))
                        {
                            idRepairPart++;
                            CountPepairPart++;
                            var nameRepairPart = repairPart.SelectSingleNode($"//*[@id='autoparts_tree']/ul/li[{CountCategory}]/ul/li[{CountSubCategory}]/ul/li[{CountPepairPart}]/a/text()");
                            var partInStock = subItem.SelectSingleNode($"//*[@id='autoparts_tree']/ul/li[{CountCategory}]/ul/li[{CountSubCategory}]/ul/li[{CountPepairPart}]/em");
                            var linkInPart = d.QuerySelector($"[xpath>'//*[@id=\"autoparts_tree\"]/ul/li[{CountCategory}]/ul/li[{CountSubCategory}]/ul/li[{CountPepairPart}]/a']").GetAttribute("href");

                            listRerairParts.Add(new RepairPart
                            {
                                id = idRepairPart,
                                nameRepairPart = nameRepairPart.TextContent,
                                subCategoryId = idCountSubCategory,
                                countProductinStock = partInStock == null ? "" : partInStock.TextContent,
                                repairPartLink = SITEURL + linkInPart

                            });


                        }
                    }

                }


            }
          
        }

        #endregion

           
    }
}
