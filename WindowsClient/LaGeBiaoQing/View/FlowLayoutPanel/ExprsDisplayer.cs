using LaGeBiaoQing.Class;
using LaGeBiaoQing.Model;
using LaGeBiaoQing.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaGeBiaoQing.View.ExprsDisplayer
{
    class RemoteExprsLoader : BackgroundWorker
    {
        public long id;
        public string tagContent;
        public string idString;
    }

    class ExprsDisplayer : FlowLayoutPanel
    {
        private List<Expr> exprs;
        private long requestId;
        private string tagContent;
        private string idString;

        ExprsDisplayer()
        {
            requestId = 0;
        }

        // Load Remote Exprs

        public void loadRemoteExprs(string tagContent, string idString)
        {
            RemoteExprsLoader worker = new RemoteExprsLoader();
            // TODO This type of operation may cause race condition, but it's not a big problem here(since user can hardly doing things that quickly)
            worker.id = ++requestId;
            worker.tagContent = tagContent;
            worker.idString = idString;

            worker.DoWork += RemoteExprsLoader_DoWork;
            worker.RunWorkerCompleted += RemoteExprsLoader_RunWorkerCompleted;

            worker.RunWorkerAsync();
        }

        private void RemoteExprsLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            RemoteExprsLoader worker = sender as RemoteExprsLoader;
            if (worker.idString == null)
            {
                e.Result = ExprService.GetAllExprsByTagContent(worker.tagContent);
            }
            else
            {
                e.Result = ExprService.GetMyExprsByTagContent(worker.tagContent);
            }
        }

        private void RemoteExprsLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        public void loadRecentlyUsedExprs()
        {

        }
    }
}
