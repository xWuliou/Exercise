using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace TestWeChat
{
    public class Menu
    {
        public static  void CreateMenu()
        {
            string createMenuUrl = ConfigurationManager.AppSettings["createMenuUrl"] + "access_token = "+ConfigurationManager.AppSettings["token"];
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(createMenuUrl);
            request.Method = "post";
            request.ContentType = "application/json";
            string content = "";
            using(var stream = request.GetRequestStream())
            using(var streamWriter = new StreamWriter(stream))
            {
               streamWriter.Write(content);
            }
        }   
    }
}