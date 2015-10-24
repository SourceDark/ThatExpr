using LaGeBiaoQing.Model;
using LaGeBiaoQing.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LaGeBiaoQing.Service
{
    class ExprService
    {
        public static List<Expr> GetAllExprsByTagContent(string tagContent)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            String response = NetworkUtility.SyncRequest("exprs/all?tag=" + tagContent);
            List<Expr> list = Serializer.Deserialize<List<Expr>>(response);
            return list;
        }
    }
}
