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

namespace LaGeBiaoQing
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            AsyncRequest asyncRequest = NetworkUtility.Request;
            IAsyncResult iAsyncResult = asyncRequest.BeginInvoke("tags/all", new AsyncCallback(allTagsRequestComplete), null);
        }

        void allTagsRequestComplete(IAsyncResult ar)
        {
            AsyncResult result = (AsyncResult)ar;
            AsyncRequest asyncRequest = (AsyncRequest)result.AsyncDelegate;
            string returnValue = asyncRequest.EndInvoke(ar);
            Console.WriteLine(returnValue);
        }
    }
}
