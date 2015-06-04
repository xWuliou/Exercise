using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TestWeChat
{
    public class HttpHandler:IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string postString = string.Empty;
            if(HttpContext.Current.Request.HttpMethod.ToUpper()=="POST")
            {                
                using(Stream stream = HttpContext.Current.Request.InputStream)
                {
                    byte[] postBytes = new byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }
                if(!string.IsNullOrEmpty(postString))
                {
                    Execute(postString);
                }
            }
            else
            {
                
                Auth();
            }
        }    





        public void Execute(string postStr)
        {
            
        }


        private bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] arr = { token, timestamp, nonce};
            Array.Sort(arr);
            string tmpStr = string.Join("", arr);
            
            var sha1 = new SHA1CryptoServiceProvider();
            byte[] str1 =Encoding.ASCII.GetBytes(tmpStr);
            byte[] str2 = sha1.ComputeHash(str1);
            tmpStr = BitConverter.ToString(str2).Replace("-", "");          
            if (tmpStr.ToLower() == signature)
                return true;
            else
                return false;

        }

        /// <summary>
        /// 身份验证
        /// </summary>
        private void Auth()
        {

            string token = ConfigurationManager.AppSettings["token"];

            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            if (echoString == null) return;

            if (CheckSignature(token, signature, timestamp, nonce))
                if (!string.IsNullOrEmpty(echoString))
                {
                    HttpContext.Current.Response.Write(echoString);
                    HttpContext.Current.Response.End();
                }


        }
    }
}