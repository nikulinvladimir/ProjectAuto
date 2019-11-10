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

        List<RepairPart> repairParts;

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
                DB.SetAuto(item);
            }
        }
        #endregion

        #region ParsRepairParts

        void ParsRepairsParts()
        {
            string link = "https://www.avtoall.ru/catalog/paz-20/avtobusy-36/paz_672m-393/";
            string response = site.GetPage(link);
            repairParts = new List<RepairPart>();
            repairParts = site.ParsRepairPart(response);
        }

        #endregion

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


        private void button1_Click_1(object sender, EventArgs e)
        {

            ParsRepairsParts();

            #region Выыод Спаршеных данных
            foreach (var item in repairParts)
            {
                richTextBox1.AppendText(item.namePart);
            }

            //foreach (var automobile in DB.GetAuto())
            //{
            //    richTextBox1.AppendText($"\n Название " + automobile.nameAuto + $"\n № каталога " + automobile.linkAuto + $"\n Год каталога " + automobile.catalogYears + $"\n № Модели " + automobile.model + $"\n Запчастей в наличии " + automobile.productInStock + $"\n   ");
            //}
            #endregion

    
        }
    }
}
