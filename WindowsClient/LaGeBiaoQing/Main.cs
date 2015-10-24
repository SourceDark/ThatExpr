using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using LaGeBiaoQing.Service;
using LaGeBiaoQing.Model;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Net;
using System.Drawing;

namespace LaGeBiaoQing
{
    public partial class Main : Form
    {
        // System infos 

        private string cachePath;

        // Variables

        private List<TagContent> tagContents;
        private List<Expr> exprs;

        // Components

        private int selectTagContentIndex;
        private List<PictureBox> pictureBoxs = new List<PictureBox>();

        // Handles

        private IntPtr qqHandle;

        // External Functions

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        // Functions

        public Main()
        {
            InitializeComponent();

            tagsLoader.RunWorkerAsync();

            Process qqProcess = Process.GetProcessesByName("QQ").FirstOrDefault();
            if (qqProcess != null)
            {
                qqHandle = qqProcess.MainWindowHandle;
            }

            // Create a path to store exprs.
            cachePath = Directory.GetCurrentDirectory() + "\\expr";
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox1.Enabled = false;
            ComboBox comboBox = sender as ComboBox;
            selectTagContentIndex = comboBox.SelectedIndex;
            exprsLoader.RunWorkerAsync();
        }

        // Tags Loader Functions

        private void tagsLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = TagService.GetUsingTagContents();
        }

        private void tagsLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dictionary<string, long> dic = e.Result as Dictionary<string, long>;
            tagContents = new List<TagContent>();
            foreach (string key in dic.Keys)
            {
                TagContent tagContent = new TagContent();
                tagContent.content = key;
                tagContent.useAmount = dic[key];
                tagContents.Add(tagContent);
            }
            tagContents.Sort(delegate (TagContent a, TagContent b) { return b.useAmount.CompareTo(a.useAmount); });
            List<string> list = new List<string>();
            foreach (TagContent tagContent in tagContents)
            {
                list.Add((tagContent.content.Length > 0 ? tagContent.content : "默认") + "(" + tagContent.useAmount + ")");
            }
            comboBox1.Items.Clear();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = list[0];
        }

        // Flow

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Exprs Loader Functions

        private void exprsLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            exprs = ExprService.GetAllExprsByTagContent(tagContents[selectTagContentIndex].content);
        }

        private void exprsLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // clear
            flowLayoutPanel1.Controls.Clear();
            pictureBoxs.Clear();

            // generate new pictures
            int i = 0;
            foreach (Expr expr in exprs)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = 40;
                pictureBox.Height = 40;
                pictureBox.Tag = i++;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Click += PictureBox_Click;
                pictureBox.MouseEnter += PictureBox_MouseEnter;
                // add to list
                flowLayoutPanel1.Controls.Add(pictureBox);
                pictureBoxs.Add(pictureBox);
            }

            // Start downloading pictures
            PictureLoader.RunWorkerAsync();
        }

        // Picture Box Functions

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            string[] s = new string[1];
            s[0] = cachePath + "\\" + exprs[(int)pictureBox.Tag].md5 + exprs[(int)pictureBox.Tag].extension;
            DataObject dataObject = new DataObject();
            dataObject.SetData(DataFormats.FileDrop, s);
            dataObject.SetData(DataFormats.Bitmap, pictureBox.Image);
            Clipboard.SetDataObject(dataObject);
            if (qqHandle != null)
            {
                SetForegroundWindow(qqHandle);
                SendKeys.SendWait("^V");
            }
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            largerViewerr.Image = pictureBox.Image;
        }

        // Picture Loader Functions

        private void PictureLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            WebClient client = new WebClient();
            for (int i = 0; i < exprs.Count; i++)
            {
                string url = Properties.Settings.Default["ExprUrl"] + "/" + exprs[i].md5 + exprs[i].extension;
                if (!File.Exists(cachePath + "\\" + exprs[i].md5 + exprs[i].extension))
                {
                    client.DownloadFile(url, cachePath + "\\" + exprs[i].md5 + exprs[i].extension);
                }
                worker.ReportProgress(i);
            }
        }

        private void PictureLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int index = e.ProgressPercentage;
            pictureBoxs[index].ImageLocation = cachePath + "\\" + exprs[index].md5 + exprs[index].extension;
        }

        private void PictureLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            comboBox1.Enabled = true;
        }
    }
}
