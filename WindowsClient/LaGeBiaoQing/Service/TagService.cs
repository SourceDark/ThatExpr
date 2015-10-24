using LaGeBiaoQing.Model;
using LaGeBiaoQing.Utility;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace LaGeBiaoQing.Service
{
    class TagService
    {
        public static List<TagContent> GetAllTagContents()
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            String response = NetworkUtility.SyncRequest("tags/all");
            Dictionary<String, Int64> dic = Serializer.Deserialize<Dictionary<String, Int64>>(response);
            List<TagContent> tagContents = new List<TagContent>();
            foreach (string key in dic.Keys)
            {
                TagContent tagContent = new TagContent();
                tagContent.content = key;
                tagContent.useAmount = dic[key];
                tagContents.Add(tagContent);
            }
            tagContents.Sort(delegate (TagContent a, TagContent b) { return b.useAmount.CompareTo(a.useAmount); });
            return tagContents;
        }
    }
}
