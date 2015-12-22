using LaGeBiaoQing.Model;
using LaGeBiaoQing.Service;
using LaGeBiaoQing.Utility;
using LaGeBiaoQing.View.PictureBoxs;
using System.Collections.Generic;
using System.ComponentModel;
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

    public delegate void MouseOnExprEventHandler(object sender, Expr expr);

    class ExprsDisplayer : FlowLayoutPanel
    {
        private long requestId;

        public event MouseOnExprEventHandler MouseOnExpr;

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
            worker.ProgressChanged += RemoteExprsLoader_ProgressChanged;
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
                ExprUtility.getRemoteExprFile(worker.exprs[i]);
                worker.ReportProgress(i);
            }
        }

        private void RemoteExprsLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RemoteExprsLoader worker = sender as RemoteExprsLoader;
            if (worker.id == requestId)
            {
                int index = e.ProgressPercentage;
                ExprDisplayer exprDisplayer = new ExprDisplayer(worker.exprs[index]);
                exprDisplayer.Width = (this.Width - this.Margin.All * 2 - this.Padding.Right - 40) / 5;
                exprDisplayer.Height = exprDisplayer.Width;
                exprDisplayer.SizeMode = PictureBoxSizeMode.Zoom;
                exprDisplayer.ImageLocation = FileUtility.FullPath(worker.exprs[index]);
                exprDisplayer.MouseEnter += ExprDisplayer_MouseEnter;
                this.Controls.Add(exprDisplayer);
            }
        }

        private void RemoteExprsLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        public void loadRecentlyUsedExprs()
        {
            this.Controls.Clear();

            List<Expr> exprs = SettingUtility.getRecentlyUsedExprs();
            foreach (Expr expr in exprs) {
                ExprDisplayer exprDisplayer = new ExprDisplayer(expr);
                exprDisplayer.Width = (this.Width - this.Margin.All * 2 - this.Padding.Right - 40) / 5;
                exprDisplayer.Height = exprDisplayer.Width;
                exprDisplayer.SizeMode = PictureBoxSizeMode.Zoom;
                exprDisplayer.ImageLocation = FileUtility.FullPath(expr);
                exprDisplayer.MouseEnter += ExprDisplayer_MouseEnter;
                this.Controls.Add(exprDisplayer);
            }
        }

        private void ExprDisplayer_MouseEnter(object sender, System.EventArgs e)
        {
            ExprDisplayer exprDisplayer = sender as ExprDisplayer;
            if (this.MouseOnExpr != null)
            {
                MouseOnExpr(this, exprDisplayer.expr);
            }
        }


    }
}
