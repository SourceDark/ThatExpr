using System;
using System.IO;
using System.Net;

namespace LaGeBiaoQing.Utility
{
    class NetworkUtility
    {
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
    }
}
