using LaGeBiaoQing.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaGeBiaoQing.Service;

namespace LaGeBiaoQing
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            tagsLoader.RunWorkerAsync();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tagsLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = CollectionService.GetUsingTagContents();
        }

        private void tagsLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dictionary<string, long> dic = e.Result as Dictionary<string, long>;
            List<string> list = new List<string>();
            foreach (string key in dic.Keys)
            {
                list.Add(key + "(" + dic[key] + ")");
            }
            Console.WriteLine(list.First());
            comboBox1.Items.Clear();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = list[0];
        }
    }
}
