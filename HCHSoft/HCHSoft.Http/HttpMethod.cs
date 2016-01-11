using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HCHSoft.Http
{
    public class HttpMethod
    {
        public static string CallHttpPost(string url, string param, int timeout)
        {
            string content = string.Empty;
            byte[] bs = Encoding.UTF8.GetBytes(param);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.ContentLength = bs.Length;
            req.Timeout = timeout;
            req.ReadWriteTimeout = timeout;
            req.Credentials = CredentialCache.DefaultCredentials;

            try
            {
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }

                using (WebResponse wr = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(wr.GetResponseStream());
                    content = sr.ReadToEnd();
                    sr.Close();
                    wr.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return content;
        }

        public static string CallHttpGet(string url, int timeout)
        {
            string content = string.Empty;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            req.ReadWriteTimeout = timeout;
            req.Timeout = timeout;

            try
            {
                using (WebResponse wr = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(wr.GetResponseStream());
                    content = sr.ReadToEnd();
                    sr.Close();
                    wr.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return content;
        }
    }
}
