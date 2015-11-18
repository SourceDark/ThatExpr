using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using LaGeBiaoQing.Service;
using LaGeBiaoQing.Model;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Net;
using LaGeBiaoQing.View.ComboBoxes;
using LaGeBiaoQing.View.TabPages;

namespace LaGeBiaoQing
{
    public partial class Main : Form
    {
        // System infos 

        private string cachePath;

        // Variables
        
        private List<Expr> exprs;

        // Components

        private TagContent selectTagContent;
        private List<PictureBox> pictureBoxs = new List<PictureBox>();

        // Custom Components
        private TagContentComboBox tagContentComboBox;
        private CollectionTagPage collectionTagPage;

        // Handles

        private IntPtr qqHandle;

        // External Functions

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        // Functions

        public Main()
        {
            InitializeComponent();
            InitializeCustomComponent();
            
            tagContentComboBox.loadTagContents();

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

        public void InitializeCustomComponent()
        {
            tagContentComboBox = new TagContentComboBox(TagContentComboBoxType.InDiscoverTabPage);
            tagContentComboBox.FormattingEnabled = true;
            tagContentComboBox.Location = new System.Drawing.Point(6, 8);
            tagContentComboBox.Name = "tagContentComboBox";
            tagContentComboBox.Size = new System.Drawing.Size(121, 20);
            tagContentComboBox.TabIndex = 0;
            tagContentComboBox.SelectTagContent += TagContentComboBox_SelectTagContent;
            tagContentComboBox.SelectNewest += TagContentComboBox_SelectNewest;
            DiscoverPage.Controls.Add(tagContentComboBox);

            collectionTagPage = new CollectionTagPage();
            IntPtr h = this.tabControl1.Handle;
            tabControl1.TabPages.Insert(0, collectionTagPage);
            tabControl1.SelectedIndex = 0;
        }

        private void TagContentComboBox_SelectTagContent(object sender, TagContent selectTagContent)
        {
            this.selectTagContent = selectTagContent;
            // Disable tagContentComboBox to pretent error
            tagContentComboBox.Enabled = false;
            exprsLoader.RunWorkerAsync();
        }

        private void TagContentComboBox_SelectNewest(object sender)
        {
            // TODO Can't simply use null to represent newest, since there may be more type
            this.selectTagContent = null;
            // Disable tagContentComboBox to pretent error
            tagContentComboBox.Enabled = false;
            exprsLoader.RunWorkerAsync();
        }

        // Flow

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Exprs Loader Functions

        private void exprsLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            if (selectTagContent != null)
            {
                exprs = ExprService.GetAllExprsByTagContent(selectTagContent.content);
            }
            else
            {
                exprs = ExprService.GetAllExprsByTagContent("");
            }
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
                pictureBox.Width = (flowLayoutPanel1.Width - flowLayoutPanel1.Margin.All * 2 - flowLayoutPanel1.Padding.Right - 40) / 5;
                pictureBox.Height = pictureBox.Width;
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
            tagContentComboBox.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void largerViewerr_Click(object sender, EventArgs e)
        {

        }
    }
}
