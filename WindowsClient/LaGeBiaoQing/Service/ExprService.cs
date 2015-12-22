using LaGeBiaoQing.Model;
using LaGeBiaoQing.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaGeBiaoQing.Service
{
    class ExprService
    {
        public static List<Expr> GetAllExprsByTagContent(string tagContent)
        {
            String response = NetworkUtility.SyncRequest("exprs/all?tag=" + tagContent);
            List<Expr> list = JsonConvert.DeserializeObject<List<Expr>>(response);
            return list;
        }

        public static List<Expr> GetMyExprsByTagContent(string tagContent)
        {
            String response = NetworkUtility.SyncRequest("exprs/my?tag=" + tagContent);
            List<Expr> list = JsonConvert.DeserializeObject<List<Expr>>(response);
            return list;
        }
    }
}
