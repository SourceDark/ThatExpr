using LaGeBiaoQing.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace LaGeBiaoQing.Utility
{
    class NetworkUtility
    {
        public static String FullUrl(Expr expr)
        {
            return Properties.Settings.Default["ExprUrl"] + "/" + expr.filename();
        }

        public static String SyncRequest(String uri)
        {
            String requestUrl = Properties.Settings.Default["ApiUrl"] + "/" + Properties.Settings.Default["IdString"] + "/" + uri;
            Console.WriteLine(requestUrl);
            
            WebRequest request = WebRequest.Create(requestUrl);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }

        public static String PostAsync(String uri, Dictionary<String, String> parameters)
        {
            String requestUrl = Properties.Settings.Default["ApiUrl"] + "/" + Properties.Settings.Default["IdString"] + "/" + uri;
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(parameters);
                var response = client.PostAsync(requestUrl, content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
