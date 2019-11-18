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


        public CatalogParts(int n)
        {
            InitializeComponent();
            //инициализация категорий запчастей и присвоение из БД
            listCategoryParts = new List<CategoryPart>();
            listCategoryParts = connectDB.GetCategoryParts(n);

            //инициализация Под категорий запчастей и присвоение из БД
            listSubParts = new List<SubCategoryParts>();
            listSubParts = connectDB.GetSubCategoryParts();

            //инициализация запчастей и присвоение из БД
            listRepairsParts = new List<RepairPart>();
            listRepairsParts = connectDB.GetRepairsParts();


            TreeNode categoryNodes = new TreeNode();
            TreeNode subCategoryNodes = new TreeNode();

            foreach (var item in listCategoryParts)
            {
                categoryNodes = treeView1.Nodes.Add(item.id.ToString(), item.categoryName);

                foreach (var subItem in listSubParts)
                {
                    if (item.id == subItem.categoryId)
                        subCategoryNodes = categoryNodes.Nodes.Add(subItem.id.ToString(), subItem.nameSubCategoryPart);

                    foreach (var repairItem in listRepairsParts)
                    {
                        if (repairItem.subCategoryId == subItem.id && item.id == subItem.categoryId)
                        {
                            subCategoryNodes.Nodes.Add(repairItem.id.ToString(), repairItem.nameRepairPart);
                        }
                    }

                }
            }


            treeView1.DoubleClick += TreeView1_DoubleClick;


        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            if (((TreeView)sender).SelectedNode.Level == 2)

            {
                TreeNode currentNode = new TreeNode();
                currentNode = ((TreeView)sender).SelectedNode;
                int n = int.Parse(currentNode.Name);

                Thread thread = new Thread(new ParameterizedThreadStart(StartItemPart));
                thread.Start(n);
            }
        }

        public void StartItemPart(object n)
        {

            ItemsPatr itemsPatr = new ItemsPatr((int)n);
            itemsPatr.ShowDialog();

        }



    }
}
