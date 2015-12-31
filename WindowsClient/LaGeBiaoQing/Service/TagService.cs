using LaGeBiaoQing.Model;
using LaGeBiaoQing.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LaGeBiaoQing.Service
{
    class TagService
    {
        public static List<TagContent> GetAllTagContents()
        {
            String response = NetworkUtility.SyncRequest("tags/all");
            Dictionary<String, Int64> dic = JsonConvert.DeserializeObject<Dictionary<string, long>>(response);
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

        public static List<TagContent> GetMyTagContents()
        {
            String response = NetworkUtility.SyncRequest("tags/my");
            Dictionary<String, Int64> dic = JsonConvert.DeserializeObject<Dictionary<string, long>>(response);
            List<TagContent> tagContents = new List<TagContent>();
            foreach (string key in dic.Keys)
            {
                TagContent tagContent = new TagContent();
                tagContent.content = key;
                tagContent.useAmount = dic[key];
                tagContents.Add(tagContent);
            }
            tagContents.Sort(delegate (TagContent a, TagContent b) { return b.useAmount.CompareTo(a.useAmount); });
            SettingUtility.setUsedTags(tagContents);
            return tagContents;
        }
    }
}
