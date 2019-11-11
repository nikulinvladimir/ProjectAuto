using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAuto
{
    public partial class Form2 : Form
    {
        ConnectDB DB;
        SiteLink site;


        List<string> link = new List<string>();

        public Form2()
        {
            InitializeComponent();
            DB = new ConnectDB();
            site = new SiteLink();
        }

        #region ParsAutoCatalog
  

        void ParsAutoCatalog()
        {
            string link = "https://www.avtoall.ru/catalog/paz-20/";
            string response = site.GetPage(link);
            List<Automobile> automobiles = new List<Automobile>();

            automobiles = site.ParsAutoCatalog(response);

            foreach (var item in automobiles)
            {
                richTextBox1.AppendText(item.linkAuto + " ");

                //DB.SetAuto(item); /// запись в базу данных авто (ссылки,текст,картинки)
                //ParsRepairsParts(item.linkAuto); /// парсинг катигорий запчастей
                
            }
        }
        #endregion

        #region ParsRepairParts

        void ParsRepairsParts(string link)
        {    
            string response = site.GetPage(link);

            List<CategoryRepairPart> repairParts = new List<CategoryRepairPart>();

            repairParts = site.ParsRepairPart(response);
           
            //DB.SetRepairsPart(repairParts);  
        }

        #endregion

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

            //foreach (var automobile in DB.GetAuto())
            //{
            //    richTextBox1.AppendText($"\n Название " + automobile.ID + $"\n № каталога " + automobile.linkAuto + $"\n Год каталога " + automobile.catalogYears + $"\n № Модели " + automobile.model + $"\n Запчастей в наличии " + automobile.productInStock + $"\n   ");
            //}
        }
    }
}
