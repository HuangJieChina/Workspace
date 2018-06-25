using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Test
{
    public class HttpBase
    {
        protected const string ServerUri = "http://localhost:5447/api/";

        protected string HttpPost(string postUrl, string paramData)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = Encoding.GetEncoding("UTF-8").GetBytes(paramData);  // 转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/json";
                webReq.ContentLength = byteArray.Length;
                using (Stream newStream = webReq.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length);    // 写入参数
                }
                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8")))
                    {
                        ret = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ret;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public string HttpGet(string url, Dictionary<string, string> paramters)
        {
            string result = string.Empty;
            try
            {
                if (paramters != null)
                {
                    foreach (string key in paramters.Keys)
                    {
                        url += url.Contains("?") ? "&" : "?";
                        url += key + "=" + paramters[key];
                        // this.setHeaderValue(wbRequest.Headers, key, headers[key]);
                    }
                }

                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";
                
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        result = sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 增加 Header
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void setHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

    }
}
