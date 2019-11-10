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
    public partial class ViewAuto : Form
    {
        
        public ViewAuto()
        {
            InitializeComponent();

            ConnectDB connect = new ConnectDB();
            Automobile auto = new Automobile();
            List<Automobile> ListAuto = new List<Automobile>();
            ListAuto = connect.GetAuto();

            CreadFlowPanel();
            
            for (int i = 0; i < ListAuto.Count; i++)
            {
                CreatView(new Panel(), "Panel"+i, ListAuto[i]);
            }

        }

        void CreadFlowPanel()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.AutoScroll = true;
            
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(701, 561);
            this.flowLayoutPanel1.TabIndex = 9;

        }

        void CreatView(Panel panel, string namePanel, Automobile auto)
        {
            ///Добовление панелей на flowPanel
            this.flowLayoutPanel1.Controls.Add(panel);


           // panel = new System.Windows.Forms.Panel();
            panel.SuspendLayout();

            this.labelModel = new System.Windows.Forms.Label();
            this.pictureBox0 = new System.Windows.Forms.PictureBox();
            this.labelProductInStock = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelCatalogYears = new System.Windows.Forms.Label();   
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).BeginInit();
            this.SuspendLayout();
          
            // 
            // panel1
            // 
            panel.Controls.Add(this.labelModel);
            panel.Controls.Add(this.pictureBox0);
            panel.Controls.Add(this.labelProductInStock);
            panel.Controls.Add(this.labelName);
            panel.Controls.Add(this.labelCatalogYears);
            panel.Location = new System.Drawing.Point(10, 10);
            panel.Margin = new System.Windows.Forms.Padding(10);
            panel.Name = namePanel;                                  // изменяем имя панели
            panel.Padding = new System.Windows.Forms.Padding(5);
            panel.Size = new System.Drawing.Size(200, 200);
            panel.TabIndex = 5;
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(9, 182);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(34, 13);
            this.labelModel.TabIndex = 4;
            this.labelModel.Text = auto.model;
            // 
            // pictureBox0
            // 
            this.pictureBox0.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox0.Location = new System.Drawing.Point(5, 5);
            this.pictureBox0.Name = "pictureBox0";
            this.pictureBox0.Size = new System.Drawing.Size(190, 100);
            this.pictureBox0.TabIndex = 0;
            this.pictureBox0.TabStop = false;
            // 
            // labelProductInStock
            // 
            this.labelProductInStock.AutoSize = true;
            this.labelProductInStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelProductInStock.Location = new System.Drawing.Point(8, 128);
            this.labelProductInStock.Name = "labelProductInStock";
            this.labelProductInStock.Size = new System.Drawing.Size(122, 13);
            this.labelProductInStock.TabIndex = 3;
            this.labelProductInStock.Text = auto.productInStock;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(8, 108);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(92, 20);
            this.labelName.TabIndex = 1;
            this.labelName.Text = auto.nameAuto;
            // 
            // labelCatalogYears
            // 
            this.labelCatalogYears.AutoSize = true;
            this.labelCatalogYears.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCatalogYears.Location = new System.Drawing.Point(106, 113);
            this.labelCatalogYears.Name = "labelCatalogYears";
            this.labelCatalogYears.Size = new System.Drawing.Size(86, 13);
            this.labelCatalogYears.TabIndex = 2;
            this.labelCatalogYears.Text = auto.catalogYears;
            // 
            // ViewAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 561);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ViewAuto";
            this.Text = "ViewAuto";
            this.flowLayoutPanel1.ResumeLayout(false);
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).EndInit();
            this.ResumeLayout(false);

            #region MyRegion
            /////Добовление панелей на flowPanel
            //this.flowLayoutPanel1.Controls.Add(this.panel1);


            //this.panel1 = new System.Windows.Forms.Panel();
            //this.panel1.SuspendLayout();

            //this.labelModel = new System.Windows.Forms.Label();
            //this.pictureBox0 = new System.Windows.Forms.PictureBox();
            //this.labelProductInStock = new System.Windows.Forms.Label();
            //this.labelName = new System.Windows.Forms.Label();
            //this.labelCatalogYears = new System.Windows.Forms.Label();
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).BeginInit();
            //this.SuspendLayout();

            //// 
            //// panel1
            //// 
            //this.panel1.Controls.Add(this.labelModel);
            //this.panel1.Controls.Add(this.pictureBox0);
            //this.panel1.Controls.Add(this.labelProductInStock);
            //this.panel1.Controls.Add(this.labelName);
            //this.panel1.Controls.Add(this.labelCatalogYears);
            //this.panel1.Location = new System.Drawing.Point(10, 10);
            //this.panel1.Margin = new System.Windows.Forms.Padding(10);
            //this.panel1.Name = "panel1";
            //this.panel1.Padding = new System.Windows.Forms.Padding(5);
            //this.panel1.Size = new System.Drawing.Size(200, 200);
            //this.panel1.TabIndex = 5;
            //// 
            //// labelModel
            //// 
            //this.labelModel.AutoSize = true;
            //this.labelModel.Location = new System.Drawing.Point(9, 182);
            //this.labelModel.Name = "labelModel";
            //this.labelModel.Size = new System.Drawing.Size(34, 13);
            //this.labelModel.TabIndex = 4;
            //this.labelModel.Text = "672М";
            //// 
            //// pictureBox0
            //// 
            //this.pictureBox0.Dock = System.Windows.Forms.DockStyle.Top;
            //this.pictureBox0.Location = new System.Drawing.Point(5, 5);
            //this.pictureBox0.Name = "pictureBox0";
            //this.pictureBox0.Size = new System.Drawing.Size(190, 100);
            //this.pictureBox0.TabIndex = 0;
            //this.pictureBox0.TabStop = false;
            //// 
            //// labelProductInStock
            //// 
            //this.labelProductInStock.AutoSize = true;
            //this.labelProductInStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            //this.labelProductInStock.Location = new System.Drawing.Point(8, 128);
            //this.labelProductInStock.Name = "labelProductInStock";
            //this.labelProductInStock.Size = new System.Drawing.Size(122, 13);
            //this.labelProductInStock.TabIndex = 3;
            //this.labelProductInStock.Text = "596 товаров в наличии";
            //// 
            //// labelName
            //// 
            //this.labelName.AutoSize = true;
            //this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            //this.labelName.Location = new System.Drawing.Point(8, 108);
            //this.labelName.Name = "labelName";
            //this.labelName.Size = new System.Drawing.Size(92, 20);
            //this.labelName.TabIndex = 1;
            //this.labelName.Text = "ПАЗ -672М";
            //// 
            //// labelCatalogYears
            //// 
            //this.labelCatalogYears.AutoSize = true;
            //this.labelCatalogYears.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            //this.labelCatalogYears.Location = new System.Drawing.Point(106, 113);
            //this.labelCatalogYears.Name = "labelCatalogYears";
            //this.labelCatalogYears.Size = new System.Drawing.Size(86, 13);
            //this.labelCatalogYears.TabIndex = 2;
            //this.labelCatalogYears.Text = "Каталог 1987 г.";
            //// 
            //// ViewAuto
            //// 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(701, 561);
            //this.Controls.Add(this.flowLayoutPanel1);
            //this.Name = "ViewAuto";
            //this.Text = "ViewAuto";
            //this.flowLayoutPanel1.ResumeLayout(false);
            //this.panel1.ResumeLayout(false);
            //this.panel1.PerformLayout();
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).EndInit();
            //this.ResumeLayout(false);
            #endregion

        }

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        //private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.PictureBox pictureBox0;
        private System.Windows.Forms.Label labelProductInStock;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelCatalogYears;

    }
}
