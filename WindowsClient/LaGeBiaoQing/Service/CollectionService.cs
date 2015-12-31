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
        public static void createCollection(string content, long exprId)
        {
            Dictionary<string, string> paras = new Dictionary<string, string>
            {
               { "content", content },
               { "exprId", exprId.ToString() }
            };
            Response<Tag> response = JsonConvert.DeserializeObject<Response<Tag>>(NetworkUtility.PostAsync("collections", paras));
            if (response.status == Response<Tag>.responseStatus.failure)
            {
                MessageBox.Show(response.reason, "收藏失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("收藏成功", "收藏成功", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        public static void removeCollection(long collectionId)
        {
            Response<Tag> response = JsonConvert.DeserializeObject<Response<Tag>>(NetworkUtility.DeleteAsync("collections/" + collectionId));
            if (response.status == Response<Tag>.responseStatus.failure)
            {
                MessageBox.Show(response.reason, "收藏失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("收藏成功", "收藏成功", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }
    }
}
