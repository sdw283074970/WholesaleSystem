using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WholesaleSystem.Manager
{
    public class WebServiceManager
    {
        public static string QueryPostWebService(string URL, Hashtable Pars)
        {
            var _manager = new XmlManager();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            var requestXml = _manager.GenerateXml(Pars["paramsJson"], Pars["appToken"].ToString(), Pars["appKey"].ToString(), Pars["service"].ToString()).ToString();

            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes(requestXml);

            request.Method = "POST";
            request.ContentType = "application/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;

            Stream requestStream = request.GetRequestStream();

            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;

            //SetWebRequest(request);
            //byte[] data = EncodePars(requestXml);
            //var data = Encoding.UTF8.GetBytes(requestXml.ToString());
            //WriteRequestData(request, data);
            //var result = request.GetResponse();
            //return ReadXmlResponse(result);
        }

        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 10000;
        }

        private static byte[] EncodePars(Hashtable Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }

        private static void WriteRequestData(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
        }

        private static string ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                //sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }

        private static XmlDocument ReadXmlResponse(WebResponse response)
        {
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var retXml = sr.ReadToEnd();
            sr.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXml);
            return doc;
        }
    }
}
