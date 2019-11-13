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
    public partial class ViewAuto : Form
    {   
        public ViewAuto()
        {
            InitializeComponent();

            CreateAutoView autoView = new CreateAutoView();

            autoView.RunForm(this);
           
        }
          
    }
}
