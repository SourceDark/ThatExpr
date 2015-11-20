using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using LaGeBiaoQing.Service;
using LaGeBiaoQing.Model;
using LaGeBiaoQing.View.ComboBoxes;
using LaGeBiaoQing.View.TabPages;
using LaGeBiaoQing.View.PictureBoxs;
using LaGeBiaoQing.Utility;
using LaGeBiaoQing.View;

namespace LaGeBiaoQing
{
    public partial class Main : Form
    {
               
        // Functions

        public Main()
        {
            InitializeComponent();
            InitializeCustomComponent();
        }

        public void InitializeCustomComponent()
        {
            tabControl1.Controls.Add(new CollectionTagPage());
            tabControl1.Controls.Add(new DiscoverTabPage());
        }
    }
}
