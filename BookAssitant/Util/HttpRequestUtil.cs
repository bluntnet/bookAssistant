using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace BookAssistant.Util {
    class HttpRequestUtil {
        private static void Debug(string msg) {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        public static string requestURL(string url) {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            long length = request.GetResponse().ContentLength;


            System.IO.Stream ns = request.GetResponse().GetResponseStream();
            StreamReader sr = new StreamReader(ns, Encoding.UTF8);
            string respHtml = sr.ReadToEnd();
            ns.Close();
            return respHtml;
        }
        public static string requestURL(string url, Dictionary<string, string> myparams) {
            StringBuilder sb = new StringBuilder(url);
            if (url.IndexOf("?") == -1) {
                sb.Append("?");
            };
            foreach (KeyValuePair<string, string> item in myparams) {
                if (!sb.ToString().EndsWith("?")) {
                    sb.Append("&");
                }
                sb.Append(item.Key + "=" + item.Value);

            }
            Debug("reqeustSearchUrl====" + sb.ToString());
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sb.ToString());
            long length = request.GetResponse().ContentLength;


            System.IO.Stream ns = request.GetResponse().GetResponseStream();
            StreamReader sr = new StreamReader(ns, Encoding.UTF8);
            string respHtml = sr.ReadToEnd();
            ns.Close();
            return respHtml;
        }
        public static string requestURL(string url, string encode) {
            string strResult = "";
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //声明一个HttpWebRequest请求
                request.Timeout = 60000;

                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding(encode);
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
                streamReader.Close();
            } catch {
                throw;
            }
            return strResult;
        }
        public static string requestPOST(string url, Dictionary<String, String> postData, string encode) {
            string strResult = "";
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //声明一个HttpWebRequest请求
                request.Timeout = 30000;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding(encode);
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
                streamReader.Close();
            } catch {
                throw;
            }
            return strResult;
        }
    }
}
