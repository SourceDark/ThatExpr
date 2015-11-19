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
        static private string cachePath;
        static private string CachePath
        {
            get
            {
                if (cachePath == null)
                {
                    cachePath = Directory.GetCurrentDirectory() + "\\expr";
                    if (!Directory.Exists(cachePath))
                    {
                        Directory.CreateDirectory(cachePath);
                    }
                }
                return cachePath;
            }
        }

        static private List<string> downloadingExprs;
        static private List<string> DownloadingExprs
        {
            get
            {
                return downloadingExprs ?? (downloadingExprs = new List<string>());
            }
        }


        static private string fullPath(Expr expr)
        {
            return CachePath + "\\" + expr.filename();
        }

        static private string fullUrl(Expr expr)
        {
            return Properties.Settings.Default["ExprUrl"] + "/" + expr.filename();
        }

        static private Object lock1 = new Object();
        static public string getRemoteExprFile(Expr expr)
        {
            bool duty;
            lock (lock1)
            {
                if (!DownloadingExprs.Contains(expr.md5))
                {
                    if (File.Exists(fullPath(expr))) {
                        return fullPath(expr);
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
                client.DownloadFile(fullUrl(expr), fullPath(expr));
                DownloadingExprs.Remove(expr.md5);
                return fullPath(expr);
            }
            else
            {
                while (DownloadingExprs.Contains(expr.md5));
                return fullPath(expr);
            }
        }
    }
}
