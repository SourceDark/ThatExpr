using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaGeBiaoQing.Model
{
    public class Collection
    {
        // Basic
        public long id;
        public long expr_id;
        public string owner;
        public string content;

        // Extended
        public Expr expr;
    }
}
