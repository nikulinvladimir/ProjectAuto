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
    public partial class CatalogParts : Form
    { 
        List<CategoryPart> listCategoryParts;
        ConnectDB connectDB = new ConnectDB();
        List<string> SubcategoryName;

        public CatalogParts(int n)
        {
            //InitializeComponent();
            listCategoryParts = new List<CategoryPart>();
            listCategoryParts = connectDB.GetCategoryParts(n);


            List<string> categoryName = new List<string>();
            categoryName = GetNameCategoryPart();

            Init(categoryName);
        }

        int GetIdCategoryPart(string nameCategory)
        {
            int idCategory = 0;
            foreach (var item in listCategoryParts)
            {
                if (item.categoryName == nameCategory)
                {
                   idCategory = item.id;
                }
            }
            return idCategory;
        }

        List<string> GetNameCategoryPart()
        {
            List<string> name = new List<string>();
            foreach (var item in listCategoryParts)
            {
                name.Add(item.categoryName);
            }
            return name;
        }

        void Init(List<string> category)
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.listBox1.DataSource = category;
            //this.listBox1.Controls.Add(listBox2);
            this.listBox1.DoubleClick += ListBox1_DoubleClick;
            // 
            //listBox2
            //
            this.listBox2.Visible = false;
            // CatalogParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 511);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button1);
            this.Name = "CatalogParts";
            this.Text = "CatalogParts";
            this.ResumeLayout(false);
            //
            //button1
            //
            this.button1.Location = new System.Drawing.Point(13, 465);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            this.listBox2.Visible = false;  
            
        }

        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            string nameListItem = ((ListBox)sender).SelectedItem.ToString();
            int id = GetIdCategoryPart(nameListItem);
            SubcategoryName = new List<string>();
            SubcategoryName = connectDB.GetSubCategoryParts(id);

            listBox1.Visible = false;
            this.listBox2.Visible = true;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(13, 13);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(541, 446);
            this.listBox2.TabIndex = 1;
            this.listBox2.DataSource=SubcategoryName; 
            //MessageBox.Show(nameListItem);
        }

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button1;

    }
}
