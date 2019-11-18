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
    public partial class CatalogParts : Form
    { 
        List<CategoryPart> listCategoryParts;
        List<SubCategoryParts> listSubParts;
        List<RepairPart> listRepairsParts;

        ConnectDB connectDB = new ConnectDB();

        int idSubPart = 0;
        int idRepairPart = 0;

        //List<string> SubcategoryName;

        public CatalogParts(int n)
        {
            //InitializeComponent();
            //инициализация категорий запчастей и присвоение из БД
            listCategoryParts = new List<CategoryPart>();
            listCategoryParts = connectDB.GetCategoryParts(n);

            //инициализация Под категорий запчастей и присвоение из БД
            listSubParts = new List<SubCategoryParts>();
            listSubParts = connectDB.GetSubCategoryParts();

            //инициализация запчастей и присвоение из БД
            listRepairsParts = new List<RepairPart>();
            listRepairsParts = connectDB.GetRepairsParts();

            //Категории автозапчасте
            List<string> categoryName = new List<string>();
            categoryName = GetNameCategoryPart();



            Init(categoryName);
        }

        #region Get data CategoryPart
        //поиск по категирии ид по имени категории для поиска Подкатегорий
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
        //заполнение категорий авто
        List<string> GetNameCategoryPart()
        {
            List<string> name = new List<string>();

            foreach (var item in listCategoryParts)
            {
                name.Add(item.categoryName);
            }
            return name;
        }

        #endregion

        #region Get data SubPart
        //поиск ИД по названию подкаталоги для поиска запчастей
        int GetIdRepairPart(string nameSubPart,int idSubPart)
        {
            int idCategory = 0;

            foreach (var item in listSubParts)
            {
                if (item.nameSubCategoryPart == nameSubPart&& item.categoryId == idSubPart)
                {
                    idCategory = item.id;
                }

            }
            return idCategory;
        }

        //создание подкатегории по ИД категории
        List<string> GetNameSubPart(int id)
        {
            List<string> nameSubParts = new List<string>();

            foreach (var item in listSubParts)
            {
                if (item.categoryId == id)
                {
                    nameSubParts.Add(item.nameSubCategoryPart);
                } 
            }
            return nameSubParts;
        }

        #endregion

        #region Get data Repairs Parts
        //заполнение листа запчастими по ид подкаталога
        List<string> GetNameRepairsParts(int id)
        {
            List<string> nameParts = new List<string>();

            foreach (var item in listRepairsParts)
            {
                if (item.subCategoryId == id)
                {
                    nameParts.Add(item.nameRepairPart);
                }
            }
            return nameParts;
        }
        // поиск ИД запчасти по названию что бы открыть нужную запчасть
        int GetIdRepairsParts(string nameSubPart,int idsubpart)
        {
            int idCategory = 0;

            foreach (var item in listRepairsParts)
            {
                if (item.nameRepairPart == nameSubPart&&item.subCategoryId == idsubpart)
                {
                    idCategory = item.id;
                }

            }
            return idCategory;
        }


        #endregion

        #region Init Dynemic elements

        void Init(List<string> category)
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
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
            this.listBox3.Visible = false;
            // CatalogParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 511);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox3);
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

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            this.listBox2.Visible = false;  
            
        }  

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button button1;

        #endregion


        #region Создание листБоксов на все категории(нечего умнее не смог придумать:()


        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            string nameListItem = ((ListBox)sender).SelectedItem.ToString();

            idSubPart = GetIdCategoryPart(nameListItem);

            List<string> SubcategoryName = new List<string>();
            SubcategoryName = GetNameSubPart(idSubPart);


            listBox1.Visible = false;
            this.listBox2.Visible = true;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(13, 13);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(541, 446);
            this.listBox2.TabIndex = 1;
            this.listBox2.DataSource = SubcategoryName;
            this.listBox2.DoubleClick += ListBox2_DoubleClick;
           //MessageBox.Show(idSubPart.ToString());
        }


        private void ListBox2_DoubleClick(object sender, EventArgs e)
        {
            string nameSubRepairParts = ((ListBox)sender).SelectedItem.ToString();
            idRepairPart = GetIdRepairPart(nameSubRepairParts,idSubPart);

            List<string> SubcategoryName = new List<string>();
            SubcategoryName = GetNameRepairsParts(idRepairPart);

            listBox1.Visible = false;
            listBox2.Visible = false;
            this.listBox3.Visible = true;
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(13, 13);
            this.listBox3.Name = "listBox2";
            this.listBox3.Size = new System.Drawing.Size(541, 446);
            this.listBox3.TabIndex = 1;
            this.listBox3.DataSource = SubcategoryName;
            this.listBox3.DoubleClick += ListBox3_DoubleClick;
            //MessageBox.Show(idRepairPart.ToString());
        }

        private void ListBox3_DoubleClick(object sender, EventArgs e)
        {
            string nameSubRepairParts = ((ListBox)sender).SelectedItem.ToString();
            int id = GetIdRepairsParts(nameSubRepairParts, idRepairPart);

            Thread thread = new Thread(new ParameterizedThreadStart(StartItemPart));
            thread.Start(id);

            
        }
        #endregion

        public  void StartItemPart(object idPart)
        {
            ItemsPatr itemsPatr = new ItemsPatr((int)idPart);
            itemsPatr.ShowDialog(); 
           
        }


    }
}
