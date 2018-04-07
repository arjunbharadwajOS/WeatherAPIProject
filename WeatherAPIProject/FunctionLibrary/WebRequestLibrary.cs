using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherAPIProject.FunctionLibrary
{
    
    public class WebRequestLibrary
    {

        public string EndPoint { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public WebRequestLibrary()
        {
            EndPoint = "";
            Method = "Get";
            ContentType = "application/JSON";
            PostData = "";
        }

        public WebRequestLibrary(string endpoint, string method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/json";
            PostData = postData;
        }

       
        public string Request(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            
            using (var response = (HttpWebResponse)request.GetResponse())
            {

                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Faile: Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }

        public string RequestwithValidation(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;
            string message = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    message = String.Format("Unauthroized access ", response.StatusCode);
                }

                return message;
            }
        }

        public string RequestType(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;


            using (var response = (HttpWebResponse)request.GetResponse())
            {
                
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Faile: Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                return response.ContentType.ToString() + "-" +  response.ContentLength.ToString();

            }

        }


            }

}

