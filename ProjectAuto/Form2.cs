using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAuto
{
    public partial class Form2 : Form
    {
        ConnectDB DB;
        Parser parser;
        string link = "https://www.avtoall.ru/catalog/paz-20/";
        List<string> links = new List<string>();
        public Form2()
        {
            InitializeComponent();
            DB = new ConnectDB();
            parser = new Parser();
        }

        #region ParsAutoCatalog


        void ParsAutoCatalog()
        {
            List<Automobile> automobiles = new List<Automobile>();
            automobiles = parser.ParsAutoCatalog(link);

            #region outputTextInAuto
            //foreach (var item in automobiles)
            //{
            //    richTextBox1.AppendText($"\n Название " + item.ID + $"\n № каталога " + item.autoLink + $"\n Год каталога " + item.catalogYears + $"\n № Модели " + item.model + $"\n Запчастей в наличии " + item.productInStock + $"\n   " + item.catalogId + $"\n   " + item.ImgLink + $"\n   " + item.imgPath);
            //}
            #endregion          

            foreach (var item in automobiles)
            {
                //DB.SetAuto(item); /// запись в базу данных авто (ссылки,текст,картинки)
                //links.Add(item.autoLink);

                ParsCatalogParts(item.autoLink,item.ID); /// парсинг катигорий запчасте
                
            }
        }
        #endregion

        #region ParsCatalogParts

        void ParsCatalogParts(string link, int idAuto)
        {

            parser.ParsCategoryRepairPart(link, idAuto);

            //DB.SetCatalogPart(catalogParts);
            
            foreach (var item in parser.listCategoryPart)
            {
                //richTextBox1.AppendText(item.id + " " + item.categoryName + " " + item.autoId + "\n");
                //ParsSubCategoryParts(item.id);      

            }

            foreach (var Subitem in parser.listSubCetegoryParts)
            {
                richTextBox1.AppendText(Subitem.id + " " + Subitem.nameSubCategoryPart + " " + Subitem.categoryId + "\n");

            }

            foreach (var repPart in parser.listRerairParts)
            {
                //richTextBox1.AppendText(repPart.id + " " + repPart.nameRepairPart + " " + repPart.subCategoryId +"\n");
            }
            DB.SetRepairPart(parser.listRerairParts);
            //DB.SetSubCategoryPart(parser.listSubCetegoryParts);
        }

        #endregion

        //void ParsSubCategoryParts(string link)
        //{
        //    //List<SubCategoryParts> ListSubCategoryParts = new List<SubCategoryParts>();
           
        //    //ListSubCategoryParts = parser.ParsSubCategoryPart(link, 1);

        //    //DB.SetSubCategoryPart(ListSubCategoryParts);
  

        //}

        #region ViewRun
        void RunFormView()
        {
            ViewAuto viewAuto = new ViewAuto();
            viewAuto.ShowDialog();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(RunFormView);
            thread.Start();
        }
        #endregion

        #region ButtonStarPars
        private void button1_Click_1(object sender, EventArgs e)
        {
            ParsAutoCatalog();
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

            foreach (var automobile in DB.GetAuto())
            {
                richTextBox1.AppendText($"\n Название " + automobile.ID + $"\n № каталога " + automobile.autoLink + $"\n Год каталога " + automobile.catalogYears + $"\n № Модели " + automobile.model + $"\n Запчастей в наличии " + automobile.productInStock + $"\n   " + automobile.catalogId + $"\n   " + automobile.ImgLink + $"\n   " + automobile.ImgLink);
            }
        }
    }
}
