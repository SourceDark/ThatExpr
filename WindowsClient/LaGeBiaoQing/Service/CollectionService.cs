using LaGeBiaoQing.Model;
using LaGeBiaoQing.Service;
using LaGeBiaoQing.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaGeBiaoQing.Service
{
    class Response<T>
    {
        public enum responseStatus { failure, success };
        public responseStatus status = 0;
        public string reason = null;
        public T response = default(T);
    }

    class CollectionService
    {
        public static void CreateCollection(string content, long exprId)
        {
            Dictionary<string, string> paras = new Dictionary<string, string>
            {
               { "content", content },
               { "exprId", exprId.ToString() }
            };
            Response<Tag> response = JsonConvert.DeserializeObject<Response<Tag>>(NetworkUtility.PostAsync("collections", paras));
            if (response.status == Response<Tag>.responseStatus.failure)
            {
                MessageBox.Show(response.reason, "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("收藏成功", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        public static bool RemoveCollection(long collectionId)
        {
            Response<Tag> response = JsonConvert.DeserializeObject<Response<Tag>>(NetworkUtility.DeleteAsync("collections/" + collectionId));
            if (response.status == Response<Tag>.responseStatus.failure)
            {
                MessageBox.Show(response.reason, "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                MessageBox.Show("取消收藏成功", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.None);
                return true;
            }
        }

        public static List<Collection> GetCollectionByContent(bool onlyMine, string content)
        {
            string jsonStr = NetworkUtility.GetAsync("collections?onlyMine=" + onlyMine + "&content=" + content);
            List<Collection> collections = JsonConvert.DeserializeObject<List<Collection>>(jsonStr);
            return collections;
        }
    }
}
