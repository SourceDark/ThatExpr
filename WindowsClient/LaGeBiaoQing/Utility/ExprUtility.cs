using LaGeBiaoQing.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace LaGeBiaoQing.Utility
{
    class ExprUtility
    {
        static private List<string> downloadingExprs;
        static private List<string> DownloadingExprs
        {
            get
            {
                return downloadingExprs ?? (downloadingExprs = new List<string>());
            }
        }
        
        static private Object lock1 = new Object();
        static public string getRemoteExprFile(Expr expr)
        {
            bool duty;
            lock (lock1)
            {
                if (!DownloadingExprs.Contains(expr.md5))
                {
                    if (File.Exists(FileUtility.FullPath(expr))) {
                        return FileUtility.FullPath(expr);
                    }
                    else
                    {
                        DownloadingExprs.Add(expr.md5);
                        duty = true;
                    }
                }
                else
                {
                    duty = false;
                }
            }
            if (duty)
            {
                WebClient client = new WebClient();
                client.DownloadFile(NetworkUtility.FullUrl(expr), FileUtility.FullPath(expr));
                DownloadingExprs.Remove(expr.md5);
                return FileUtility.FullPath(expr);
            }
            else
            {
                while (DownloadingExprs.Contains(expr.md5));
                return FileUtility.FullPath(expr);
            }
        }
    }
}
