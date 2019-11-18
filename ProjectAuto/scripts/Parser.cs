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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectAuto
{

    delegate void eventEnd(string message);

    class Parser
    {
        public event eventEnd parsEnd;
        
        ConnectDB connectDB = new ConnectDB();
        const string SITEURL = "www.avtoall.ru";
        const string link = "https://www.avtoall.ru/catalog/paz-20/";

        public List<Automobile> automobiles;
        public List<CategoryPart> listCategoryPart;
        public List<SubCategoryParts> listSubCetegoryParts;
        public List<RepairPart> listRerairParts;
        public List<Part> listPartsDiscription;



        public List<string> test;

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

            //parsEnd?.Invoke($" End getPage {link}" + @"\");

            return response;

        }

        #endregion

        // Парсинг страницы автокаталога 
        #region ParsAutoCatalog

        public void ParsAutoCatalog()
        {
            string response = GetPage(link);
            HtmlParser htmlParser = new HtmlParser();
            var doc = htmlParser.ParseDocument(response);
            automobiles = new List<Automobile>();
            int CountID = 0;
            foreach (var item in doc.QuerySelectorAll(".catalog-models>>li"))
            {
                automobiles.Add(new Automobile
                {
                    ID = ++CountID,
                    nameAuto = item.QuerySelector(".model_item").TextContent,
                    autoLink = SITEURL + item.QuerySelector(".model_item") == null ? "" : SITEURL + item.QuerySelector(".model_item").GetAttribute("href"),
                    catalogYears = item.QuerySelector("b>u") == null ? "" : item.QuerySelector("b>u").TextContent,
                    productInStock = item.QuerySelector("b>ins") == null ? "" : item.QuerySelector("b>ins").TextContent,
                    model = item.QuerySelector("b>small") == null ? "" : item.QuerySelector("b>small").TextContent,
                    ImgLink = item.QuerySelector("img").GetAttribute("src"), //сохраняет ссылки на картинки
                    imgPath = DownloadFile(item.QuerySelector("img").GetAttribute("src"), "imageAuto", item.QuerySelector(".model_item").TextContent),
                });
            }

            parsEnd?.Invoke("End ParsAutoCatalog");
        }

        #endregion

        // Парсинг всех ссылок на авто с автозапчастями
        #region ParsCategoryRepairPart


        int idAuto = 0;
        int idCountCategory = 0;
        static int idCountSubCategory = 0;
        int idRepairPart = 0;
        public async void ParsCategoryRepairPart()
        {
            listCategoryPart = new List<CategoryPart>();
            listSubCetegoryParts = new List<SubCategoryParts>();
            listRerairParts = new List<RepairPart>();

            foreach (var a in automobiles)
            {
                idAuto++;
                string response = GetPage(a.autoLink);
                HtmlParser htmlParser = new HtmlParser();
                var doc = htmlParser.ParseDocument(response);


                var config = Configuration.Default.WithXPath();
                var context = BrowsingContext.New(config);
                var d = await context.OpenAsync(res => res.Content(response));


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

            parsEnd?.Invoke("End ParsCategoryRepairPart");
        }

        #endregion


        #region Pars Links Repairs parts

        /// <summary>
        /// Парсит ссылки на каждую запчасть
        /// </summary>
        /// 

        public void ParsLinkRepairs()
        {
            listPartsDiscription = new List<Part>();
            test = new List<string>();

            List<MissingPart> listMissingParts = new List<MissingPart>();
            List<GoodsPart> goodsParts = new List<GoodsPart>();

            int id = 0;
            int idPrice = 0;
            int idNoPrice = 0;

            foreach (var part in connectDB.GetRepairsParts())
            {

                //string response = GetPage("https://www.avtoall.ru/catalog/paz-20/avtobusy-36/paz_672m-393/obshiiy_vid_dvigatelya-43/");
                string response = GetPage(part.repairPartLink);
                HtmlParser htmlParser = new HtmlParser();
                var doc = htmlParser.ParseDocument(response);

                foreach (var item in doc.DocumentElement.QuerySelectorAll("*body"))
                {
                    id++;

                    var articlePart = item.QuerySelector(".part .number");
                    var countGoods = item.QuerySelector(".part .count>span");
                    var nameGoods = item.QuerySelector(".color-block>h1");
                    var linkPictureScheme = item.QuerySelector("#picture_img").GetAttribute("src");

                    foreach (var goods in doc.DocumentElement.QuerySelectorAll(".item"))
                    {
                        idPrice++;

                        var linkImagePart = goods.QuerySelector(".image>a>img").GetAttribute("src");
                        var namePart = goods.QuerySelector(".item-name");
                        var price = goods.QuerySelector(".price-internet");

                        goodsParts.Add(new GoodsPart
                        {
                            id = idPrice,
                            namePart = namePart.TextContent,
                            linkImagePart = linkImagePart == null ? "\noImage.gif" : DownloadFile(linkImagePart, "partImage", namePart.TextContent),
                            price = price == null ? "" : price.TextContent,
                            parentId = id

                        });

                    }

                    foreach (var misPart in doc.DocumentElement.QuerySelectorAll(".parts.not-price .part"))
                    {
                        idNoPrice++;

                        var article = misPart.QuerySelector(".part>.number");
                        var namePart = misPart.QuerySelector(".part>.name");
                        var count = misPart.QuerySelector(".part>.count");

                        listMissingParts.Add(new MissingPart
                        {
                            id = idNoPrice,
                            namePart = namePart.TextContent,
                            articlePart = article.TextContent,
                            productInStock = count.TextContent,
                            parentId = id
                        });
                    }


                    listPartsDiscription.Add(new Part
                    {
                        id = id,
                        linkPictureScheme = DownloadFile(linkPictureScheme, "schemaImage", nameGoods.TextContent),
                        missingParts = listMissingParts,
                        countGoods = countGoods == null ? "" : countGoods.TextContent,
                        articlePart = articlePart.TextContent,
                        goodsParts = goodsParts,
                        nameGoods = nameGoods.TextContent

                    });

                }

            }
            parsEnd.Invoke("End ParsLinkRepairs");
        }

        #endregion

        // загрузка картинки по ссылке
        #region DownloadFile

        public string DownloadFile(string s, string path, string name)
        {
            string folder = $@"\{path}\";

            string realPath = folder + name.Remove(10, name.Length - 10) + s.Remove(0, s.Length - 5); ;

            if (!IsUrlValid(s))
                return folder + "noImage.gif";

            if (IsSave(realPath))
                return realPath;

            WebClient client = new WebClient();
            client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:70.0) Gecko/20100101 Firefox/70.0";
            client.Headers["Accept-Encoding"] = "gzip, deflate, br";
            client.Headers["Accept"] = "image/webp,*/*";
            client.DownloadFileTaskAsync(new Uri(s), Environment.CurrentDirectory + realPath);

            parsEnd.Invoke($"End download{realPath} " + @"\n");

            return realPath;
        }

        private bool IsUrlValid(string url)
        {
            return Regex.IsMatch(url, @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }


        bool IsSave(string nameImage)
        {
            return File.Exists(Environment.CurrentDirectory + nameImage);
        }

        #endregion
 
        
    }
}
