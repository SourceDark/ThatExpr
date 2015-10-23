using LaGeBiaoQing.Utility;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace LaGeBiaoQing.Service
{
    class CollectionService
    {
        public static Dictionary<String, Int64> GetUsingTagContents()
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            String response = NetworkUtility.SyncRequest("tags/all");
            Console.WriteLine(response);
            Dictionary<String, Int64> dic = Serializer.Deserialize<Dictionary<String, Int64>>(response);
            return dic;
        }
    }
}
