using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaGeBiaoQing.Model
{
    public class Expr
    {
        // Basic
        public long id = 0;
        public string md5 = null;
        public string extension = null;

        // Extended
        public Collection collection;

        public string filename()
        {
            return md5 + extension;
        }
    }
}
