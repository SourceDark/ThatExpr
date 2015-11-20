using LaGeBiaoQing.Model;
using System.IO;

namespace LaGeBiaoQing.Utility
{
    class FileUtility
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

        static public string FullPath(Expr expr)
        {
            return CachePath + "\\" + expr.filename();
        }
    }
}
