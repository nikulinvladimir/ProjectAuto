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

        public Form2()
        {
            InitializeComponent();
            DB = new ConnectDB();
            parser = new Parser();
        }

        #region ParsAutoCatalog

        void ParsAutoCatalog()
        {
            //parser.ParsAutoCatalog();

        }
        #endregion



        //Добоавлние данных в базу
        #region Добовлние в базу данных

        void AddDataBase()
        {

            //DB.SetAuto(parser.automobiles);
            //DB.SetCatalogPart(parser.listCategoryPart);
            //DB.SetRepairPart(parser.listRerairParts);
            //DB.SetSubCategoryPart(parser.listSubCetegoryParts);
            //DB.SetPartDescription(parser.listPartsDiscription);
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
            Thread thread = new Thread(ParsAutoCatalog);
            thread.Start();
        }
        #endregion

        #region Button Test

        private void button2_Click(object sender, EventArgs e)
        {

            parser.ParsLinkRepairs();

            DB.SetPartDescription(parser.listPartsDiscription);

            foreach (var part in parser.listPartsDiscription)
            {
                if (part.goodsParts.GetType() == typeof(List<GoodsPart>))
                    DB.SetPartDescriptionInPrice(part.goodsParts);

                if (part.missingParts.GetType() == typeof(List<MissingPart>))
                    DB.SetPartDescriptionNoPrice(part.missingParts);

                return;
            }

            //foreach (var item in parser.automobiles)
            //{
            //    richTextBox1.AppendText(item.nameAuto + " " + item.productInStock + " " + item.model + "\n");
            //}

            //foreach (var item in parser.listCategoryPart)
            //{
            //    richTextBox1.AppendText(item.id + " " + item.categoryName + " " + item.autoId + "\n");
            //}

            //foreach (var Subitem in parser.listSubCetegoryParts)
            //{
            //    richTextBox1.AppendText(Subitem.id + " " + Subitem.nameSubCategoryPart + " " + Subitem.categoryId + "\n");
            //}

            //foreach (var repPart in parser.listRerairParts)
            //{
            //    richTextBox1.AppendText(repPart.id + " " + repPart.nameRepairPart + " " + repPart.subCategoryId + "\n");
            //}


        }

        #endregion

    }
}
