using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaGeBiaoQing.Model
{
    class Expr
    {
        public long id = 0;
        public string md5 = null;
        public string extension = null;
        public string fullPath = null;

        public string filename()
        {
            return md5 + extension;
        }
    }
}
