using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaGeBiaoQing.Utility
{
    class ExprUtility
    {
        static private string cachePath { get; set; }

        private Object thislock = new Object();

        public string getRemoteExprFile()
        {
            lock (thislock)
            {

            }
        }
    }
}
