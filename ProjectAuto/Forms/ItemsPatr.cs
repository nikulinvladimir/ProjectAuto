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
    public partial class ItemsPatr : Form
    {

        ConnectDB ConnectDB;
        string path = Environment.CurrentDirectory;
        List<Part> listParts;
        List<GoodsPart> listGoodsParts;
        List<MissingPart> listNoPrice;


        public ItemsPatr(int n)
        {
            //InitializeComponent();

            ConnectDB = new ConnectDB();

            listParts = new List<Part>();
            listParts = ConnectDB.GetDescriptionParts();

            listGoodsParts = new List<GoodsPart>();
            listGoodsParts = ConnectDB.GetPartDescriptionInPrice(n);

            listNoPrice = new List<MissingPart>();
            listNoPrice = ConnectDB.GetPartDescriptionNoPrice(n);


            Part part = new Part();
            foreach (var item in listParts)
            {
                if (item.id == n)
                {
                    part = item;
                }
            }  

            ShowDescription(part);  

            foreach (var item in listGoodsParts)
            {
                CreatePanelPart(item.namePart,item.linkImagePart,item.price);
            }

            CreateHeaderNoPrice();

            foreach (var item in listNoPrice)
            {
                CreatePanelNoPrice(item.namePart, item.articlePart, item.productInStock);
            }

        }


        void ShowDescription(Part part)
        {

            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelImage = new System.Windows.Forms.Panel();
            this.PictureBoxSchemeImagePart = new System.Windows.Forms.PictureBox();
            this.panelHeaderGoods = new System.Windows.Forms.Panel();
            this.labelCountGoods = new System.Windows.Forms.Label();
            this.labelArticleGoods = new System.Windows.Forms.Label();
            this.labelNameGoods = new System.Windows.Forms.Label();

            this.panelHeaderGoods.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxSchemeImagePart)).BeginInit();  
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.panelImage);
            this.flowLayoutPanel1.Controls.Add(this.panelHeaderGoods); 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(561, 548);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panelImage
            // 
            this.panelImage.Controls.Add(this.PictureBoxSchemeImagePart);
            this.panelImage.Location = new System.Drawing.Point(3, 3);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(535, 228);
            this.panelImage.TabIndex = 0;
            // 
            // PictureBoxSchemeImagePart
            // 
            this.PictureBoxSchemeImagePart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBoxSchemeImagePart.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxSchemeImagePart.Name = "PictureBoxSchemeImagePart";
            this.PictureBoxSchemeImagePart.Padding = new System.Windows.Forms.Padding(5);
            this.PictureBoxSchemeImagePart.Size = new System.Drawing.Size(535, 228);
            this.PictureBoxSchemeImagePart.TabIndex = 0;
            this.PictureBoxSchemeImagePart.TabStop = false;
            this.PictureBoxSchemeImagePart.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxSchemeImagePart.Image = Image.FromFile(path + part.linkPictureScheme);
            // 
            // panelHeaderGoods
            // 
            this.panelHeaderGoods.Controls.Add(this.labelNameGoods);
            this.panelHeaderGoods.Controls.Add(this.labelCountGoods);
            this.panelHeaderGoods.Controls.Add(this.labelArticleGoods);
            this.panelHeaderGoods.Location = new System.Drawing.Point(3, 237);
            this.panelHeaderGoods.Name = "panelArticle";
            this.panelHeaderGoods.Size = new System.Drawing.Size(535, 23);
            this.panelHeaderGoods.TabIndex = 1;      
            // 
            // labelCountGoods
            // 
            this.labelCountGoods.AutoSize = true;
            this.labelCountGoods.Location = new System.Drawing.Point(488, 4);
            this.labelCountGoods.Name = "labelCountGoods";
            this.labelCountGoods.Size = new System.Drawing.Size(35, 13);
            this.labelCountGoods.TabIndex = 1;
            this.labelCountGoods.Text = part.countGoods; 
            // 
            // labelArticleGoods
            // 
            this.labelArticleGoods.AutoSize = true;
            this.labelArticleGoods.Location = new System.Drawing.Point(13, 4);
            this.labelArticleGoods.Name = "labelArticleGoods";
            this.labelArticleGoods.Size = new System.Drawing.Size(35, 13);
            this.labelArticleGoods.TabIndex = 0;
            this.labelArticleGoods.Text = part.articlePart;
            // 
            // labelNameGoods
            // 
            this.labelNameGoods.AutoSize = true;
            this.labelNameGoods.Location = new System.Drawing.Point(241, 4);
            this.labelNameGoods.Name = "labelNameGoods";
            this.labelNameGoods.Size = new System.Drawing.Size(35, 13);
            this.labelNameGoods.TabIndex = 2;
            this.labelNameGoods.Text = part.nameGoods;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 548);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ItemsPatr";
            this.Text = "p";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxSchemeImagePart)).EndInit();
            
            this.ResumeLayout(false);

            this.panelHeaderGoods.ResumeLayout(false);
            this.panelHeaderGoods.PerformLayout();

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        Panel CreatePanelNoPrice(string name,string article,string countProd)
        {
            Panel panelMissingPart = new Panel();

            panelMissingPart = new System.Windows.Forms.Panel();
            this.labelNameNoPrice = new System.Windows.Forms.Label();
            this.labelArticleNoPrice = new System.Windows.Forms.Label();
            this.labelCountProductNoPrice = new System.Windows.Forms.Label();

            
            // panelMissingPart
            // 
            panelMissingPart.Controls.Add(this.labelCountProductNoPrice);
            panelMissingPart.Controls.Add(this.labelArticleNoPrice);
            panelMissingPart.Controls.Add(this.labelNameNoPrice);
            panelMissingPart.Location = new System.Drawing.Point(3, 421);
            panelMissingPart.Name = "panelMissingPart";
            panelMissingPart.Size = new System.Drawing.Size(535, 21);
            panelMissingPart.TabIndex = 4;
            // 
            // labelNameNoPrice
            // 
            this.labelNameNoPrice.AutoSize = true;
            this.labelNameNoPrice.Location = new System.Drawing.Point(10, 1);
            this.labelNameNoPrice.Name = "labelNameNoPrice";
            this.labelNameNoPrice.Size = new System.Drawing.Size(35, 13);
            this.labelNameNoPrice.TabIndex = 0;
            this.labelNameNoPrice.Text = name;
            // 
            // labelArticleNoPrice
            // 
            this.labelArticleNoPrice.AutoSize = true;
            this.labelArticleNoPrice.Location = new System.Drawing.Point(340, 1);
            this.labelArticleNoPrice.Name = "labelArticleNoPrice";
            this.labelArticleNoPrice.Size = new System.Drawing.Size(35, 13);
            this.labelArticleNoPrice.TabIndex = 1;
            this.labelArticleNoPrice.Text = article;
            // 
            // labelCountProductNoPrice
            // 
            this.labelCountProductNoPrice.AutoSize = true;
            this.labelCountProductNoPrice.Location = new System.Drawing.Point(400, -5);
            this.labelCountProductNoPrice.Name = "labelCountProductNoPrice";
            this.labelCountProductNoPrice.Size = new System.Drawing.Size(35, 13);
            this.labelCountProductNoPrice.TabIndex = 2;
            this.labelCountProductNoPrice.Text = countProd;

            panelMissingPart.ResumeLayout(false);
            panelMissingPart.PerformLayout();
            panelMissingPart.SuspendLayout();


            this.flowLayoutPanel1.Controls.Add(panelMissingPart);

            return panelMissingPart;
        }

        Panel CreatePanelPart(string name, string imagePath, string price)
        {
            Panel panelPart = new Panel();

            panelPart = new System.Windows.Forms.Panel();
            this.pictureBoxImagePart = new System.Windows.Forms.PictureBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelNamePart = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagePart)).BeginInit();
            // 
            // panelPart
            // 
            panelPart.Controls.Add(this.pictureBoxImagePart);
            panelPart.Controls.Add(this.labelPrice);
            panelPart.Controls.Add(this.labelNamePart);
            panelPart.Location = new System.Drawing.Point(3, 266);
            panelPart.Name = "panelPart";
            panelPart.Size = new System.Drawing.Size(535, 120);
            panelPart.TabIndex = 2;
            // 
            // pictureBoxImagePart
            // 
            this.pictureBoxImagePart.Location = new System.Drawing.Point(6, 3);
            this.pictureBoxImagePart.Name = "pictureBoxImagePart";
            this.pictureBoxImagePart.Size = new System.Drawing.Size(121, 114);
            this.pictureBoxImagePart.TabIndex = 2;
            this.pictureBoxImagePart.TabStop = false;
            this.pictureBoxImagePart.SizeMode = PictureBoxSizeMode.StretchImage;
            //this.pictureBoxImagePart.Image = Image.FromFile(@"\partImage\Подушка УАЗ-3151,452 двигателя комплект 4шт.в упаковке "АВЕС"(Ульяновск)5.jpg")
            this.pictureBoxImagePart.Image = Image.FromFile(Environment.CurrentDirectory + imagePath);
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPrice.Location = new System.Drawing.Point(438, 74);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(51, 20);
            this.labelPrice.TabIndex = 1;
            this.labelPrice.Text = price;
            // 
            // labelNamePart
            // 
            this.labelNamePart.AutoSize = true;
            this.labelNamePart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F,System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNamePart.Location = new System.Drawing.Point(149, 14);
            this.labelNamePart.Name = "labelNamePart";
            this.labelNamePart.Size = new System.Drawing.Size(51, 20);
            this.labelNamePart.TabIndex = 0;
            this.labelNamePart.MaximumSize = new Size(200, 50);
            this.labelNamePart.Text = name;


            panelPart.ResumeLayout(false);
            panelPart.PerformLayout();
            panelPart.SuspendLayout();
            this.flowLayoutPanel1.Controls.Add(panelPart);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagePart)).EndInit();

            return panelPart;
        }




        void CreateHeaderNoPrice()
        {

            this.panelHeadrNoPrice = new System.Windows.Forms.Panel();
            this.labelHeaderNoPrice = new System.Windows.Forms.Label();

            this.flowLayoutPanel1.Controls.Add(this.panelHeadrNoPrice);

            this.panelHeadrNoPrice.Controls.Add(this.labelHeaderNoPrice);
            this.panelHeadrNoPrice.Location = new System.Drawing.Point(3, 392);
            this.panelHeadrNoPrice.Name = "panelHeadrNoPrice";
            this.panelHeadrNoPrice.Size = new System.Drawing.Size(535, 23);
            this.panelHeadrNoPrice.TabIndex = 3;
            // 
            // labelHeaderNoPrice
            // 
            this.labelHeaderNoPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHeaderNoPrice.AutoSize = true;
            this.labelHeaderNoPrice.Location = new System.Drawing.Point(3, 0);
            this.labelHeaderNoPrice.Name = "labelHeaderNoPrice";
            this.labelHeaderNoPrice.Size = new System.Drawing.Size(175, 13);
            this.labelHeaderNoPrice.TabIndex = 0;
            this.labelHeaderNoPrice.Text = "Запчасти отсутствуют в продаже";


            this.panelHeadrNoPrice.ResumeLayout(false);
            this.panelHeadrNoPrice.PerformLayout();
            this.panelHeadrNoPrice.SuspendLayout();
        }

        void InitView()
        {

            
            //this.panelHeadrNoPrice.SuspendLayout();     
            //this.panelHeadrNoPrice = new System.Windows.Forms.Panel();
            //this.labelHeaderNoPrice = new System.Windows.Forms.Label();



            //// 
            //// panelPart
            //// 
            //this.panelPart.Controls.Add(this.pictureBoxImagePart);
            //this.panelPart.Controls.Add(this.labelPrice);
            //this.panelPart.Controls.Add(this.labelNamePart);
            //this.panelPart.Location = new System.Drawing.Point(3, 266);
            //this.panelPart.Name = "panelPart";
            //this.panelPart.Size = new System.Drawing.Size(535, 120);
            //this.panelPart.TabIndex = 2;
            //// 
            //// pictureBoxImagePart
            //// 
            //this.pictureBoxImagePart.Location = new System.Drawing.Point(6, 3);
            //this.pictureBoxImagePart.Name = "pictureBoxImagePart";
            //this.pictureBoxImagePart.Size = new System.Drawing.Size(121, 114);
            //this.pictureBoxImagePart.TabIndex = 2;
            //this.pictureBoxImagePart.TabStop = false;
            //// 
            //// labelPrice
            //// 
            //this.labelPrice.AutoSize = true;
            //this.labelPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            //this.labelPrice.Location = new System.Drawing.Point(438, 74);
            //this.labelPrice.Name = "labelPrice";
            //this.labelPrice.Size = new System.Drawing.Size(51, 20);
            //this.labelPrice.TabIndex = 1;
            //this.labelPrice.Text = "label3";
            //// 
            //// labelNamePart
            //// 
            //this.labelNamePart.AutoSize = true;
            //this.labelNamePart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            //this.labelNamePart.Location = new System.Drawing.Point(149, 14);
            //this.labelNamePart.Name = "labelNamePart";
            //this.labelNamePart.Size = new System.Drawing.Size(51, 20);
            //this.labelNamePart.TabIndex = 0;
            //this.labelNamePart.Text = "label3";

            // 
            // panelHeadrNoPrice
            // 
            //this.panelHeadrNoPrice.Controls.Add(this.labelHeaderNoPrice);
            //this.panelHeadrNoPrice.Location = new System.Drawing.Point(3, 392);
            //this.panelHeadrNoPrice.Name = "panelHeadrNoPrice";
            //this.panelHeadrNoPrice.Size = new System.Drawing.Size(535, 23);
            //this.panelHeadrNoPrice.TabIndex = 3;
            //// 
            //// labelHeaderNoPrice
            //// 
            //this.labelHeaderNoPrice.AutoSize = true;
            //this.labelHeaderNoPrice.Location = new System.Drawing.Point(3, 0);
            //this.labelHeaderNoPrice.Name = "labelHeaderNoPrice";
            //this.labelHeaderNoPrice.Size = new System.Drawing.Size(175, 13);
            //this.labelHeaderNoPrice.TabIndex = 0;
            //this.labelHeaderNoPrice.Text = "Запчасти отсутствуют в продаже";
            // 
            //// panelMissingPart
            //// 
            //this.panelMissingPart.Controls.Add(this.labelCountProductNoPrice);
            //this.panelMissingPart.Controls.Add(this.labelArticleNoPrice);
            //this.panelMissingPart.Controls.Add(this.labelNameNoPrice);
            //this.panelMissingPart.Location = new System.Drawing.Point(3, 421);
            //this.panelMissingPart.Name = "panelMissingPart";
            //this.panelMissingPart.Size = new System.Drawing.Size(535, 21);
            //this.panelMissingPart.TabIndex = 4;
            //// 
            //// labelNameNoPrice
            //// 
            //this.labelNameNoPrice.AutoSize = true;
            //this.labelNameNoPrice.Location = new System.Drawing.Point(10, 1);
            //this.labelNameNoPrice.Name = "labelNameNoPrice";
            //this.labelNameNoPrice.Size = new System.Drawing.Size(35, 13);
            //this.labelNameNoPrice.TabIndex = 0;
            //this.labelNameNoPrice.Text = "label2";
            //// 
            //// labelArticleNoPrice
            //// 
            //this.labelArticleNoPrice.AutoSize = true;
            //this.labelArticleNoPrice.Location = new System.Drawing.Point(193, 1);
            //this.labelArticleNoPrice.Name = "labelArticleNoPrice";
            //this.labelArticleNoPrice.Size = new System.Drawing.Size(35, 13);
            //this.labelArticleNoPrice.TabIndex = 1;
            //this.labelArticleNoPrice.Text = "label2";
            //// 
            //// labelCountProductNoPrice
            //// 
            //this.labelCountProductNoPrice.AutoSize = true;
            //this.labelCountProductNoPrice.Location = new System.Drawing.Point(454, 1);
            //this.labelCountProductNoPrice.Name = "labelCountProductNoPrice";
            //this.labelCountProductNoPrice.Size = new System.Drawing.Size(35, 13);
            //this.labelCountProductNoPrice.TabIndex = 2;
            //this.labelCountProductNoPrice.Text = "label2";
            // 
            // ItemsPatr
            //  
            //this.panelPart.ResumeLayout(false);
            //this.panelPart.PerformLayout();
            //((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagePart)).EndInit();
            //this.panelHeadrNoPrice.ResumeLayout(false);
            //this.panelHeadrNoPrice.PerformLayout();


        }



        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.PictureBox PictureBoxSchemeImagePart;
        private System.Windows.Forms.Panel panelHeaderGoods;
        //private System.Windows.Forms.Panel panelPart;
        private System.Windows.Forms.Panel panelHeadrNoPrice;
        //private System.Windows.Forms.Panel panelMissingPart;
        private System.Windows.Forms.Label labelHeaderNoPrice;
        private System.Windows.Forms.Label labelNameNoPrice;
        private System.Windows.Forms.Label labelCountGoods;
        private System.Windows.Forms.Label labelArticleGoods;
        private System.Windows.Forms.PictureBox pictureBoxImagePart;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelNamePart;
        private System.Windows.Forms.Label labelCountProductNoPrice;
        private System.Windows.Forms.Label labelArticleNoPrice;
        private System.Windows.Forms.Label labelNameGoods;

    }
}
