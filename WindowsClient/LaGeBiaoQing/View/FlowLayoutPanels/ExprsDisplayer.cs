using LaGeBiaoQing.Model;
using LaGeBiaoQing.Service;
using LaGeBiaoQing.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaGeBiaoQing.View.FlowLayoutPanels
{
    class RemoteExprsLoader : BackgroundWorker
    {
        public List<Expr> exprs;
        public long id;
        public string tagContent;
        public string idString;
    }

    class ExprsDisplayer : FlowLayoutPanel
    {
        private long requestId;

        public ExprsDisplayer()
        {
            requestId = 0;
        }

        // This is always in main thread, so we don't have lock it
        public void loadRemoteExprs(string tagContent, string idString)
        {
            this.Controls.Clear();

            RemoteExprsLoader worker = new RemoteExprsLoader();
            worker.id = ++requestId;
            worker.tagContent = tagContent;
            worker.idString = idString;

            worker.WorkerReportsProgress = true;
            worker.DoWork += RemoteExprsLoader_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += RemoteExprsLoader_RunWorkerCompleted;

            worker.RunWorkerAsync();
        }

        private void RemoteExprsLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            RemoteExprsLoader worker = sender as RemoteExprsLoader;
            if (worker.idString == null)
            {
                worker.exprs = ExprService.GetAllExprsByTagContent(worker.tagContent);
            }
            else
            {
                worker.exprs = ExprService.GetMyExprsByTagContent(worker.tagContent);
            }
            for (int i = 0; i < worker.exprs.Count; i++)
            {
                worker.exprs[i].fullPath = ExprUtility.getRemoteExprFile(worker.exprs[i]);
                worker.ReportProgress(i);
            }
        }

        // This is always in main thread, so we don't have lock it
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RemoteExprsLoader worker = sender as RemoteExprsLoader;
            if (worker.id == requestId)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Width = (this.Width - this.Margin.All * 2 - this.Padding.Right - 40) / 5;
                pictureBox.Height = pictureBox.Width;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                int index = e.ProgressPercentage;
                pictureBox.ImageLocation = worker.exprs[index].fullPath;
                //pictureBox.Click += PictureBox_Click;
                //pictureBox.MouseEnter += PictureBox_MouseEnter;
                this.Controls.Add(pictureBox);
            }
        }

        // This is always in main thread, so we don't have lock it
        private void RemoteExprsLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        public void loadRecentlyUsedExprs()
        {

        }
    }
}
