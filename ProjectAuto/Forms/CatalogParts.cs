﻿using System;
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
    public partial class CatalogParts : Form
    {
        CategoryRepairPart categoryRepair;
        List<CategoryRepairPart> listCategoryRepair;
        ConnectDB connectDB = new ConnectDB();

        public CatalogParts()
        {
            InitializeComponent();


            categoryRepair = new CategoryRepairPart();

            listCategoryRepair = new List<CategoryRepairPart>();

            listCategoryRepair = connectDB.GetCategoryRepairsParts();

            init();
        }

       

         public void init()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(13, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(541, 446);
            this.listBox1.TabIndex = 1;
            this.listBox1.Items.Add("a");

            this.listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            // 
            // CatalogParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 511);
            this.Controls.Add(this.listBox1);
            this.Name = "CatalogParts";
            this.Text = "CatalogParts";
            this.ResumeLayout(false);


        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListBox listBox1;
  
    }
}
