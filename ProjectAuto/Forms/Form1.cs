using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAuto
{
    public partial class Form1 : Form
    {
        ConnectDB DB;
        SiteLink site;
        List<Automobile> automobiles;
        string link = "https://www.avtoall.ru/catalog/paz-20/";

        public Form1()
        {
            InitializeComponent();
            DB = new ConnectDB();
            site = new SiteLink();

        }


        void Pars()
        {
            string response = site.GetPage(link);
            automobiles = new List<Automobile>();
            automobiles = site.ParsAutoCatalog(response);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Pars();

            foreach (var item in automobiles)
            {
                //DB.SetAuto(item);
            }

            foreach (var automobile in DB.GetAuto())
            {
                //richTextBox1.AppendText ($"\n Название " + automobile.nameAuto + $"\n № каталога " + automobile.linkAuto + $"\n Год каталога " + automobile.catalogYears + $"\n № Модели " + automobile.model + $"\n Запчастей в наличии " + automobile.productInStock + $"\n   "); 
            }  

        }
    }
}
