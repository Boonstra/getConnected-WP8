﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace GetConnected
{
    class RESTRequest
    {

        public string url;
        private Dictionary<string,string> parameters;
        public string result;

        public RESTRequest(string url)
        {
            this.url = url;
            parameters = new Dictionary<string,string>();
        }

        public void putValue(string key, string value)
        {
            parameters.Add(key, value);
        }

        public void doRequest()
        {
            url += "?";
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                url += pair.Key+"="+pair.Value+"&";
            }
            url.Remove(url.Length-1);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            request.BeginGetResponse(Response_Completed, request);
        }

        private void Response_Completed(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                this.result = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(this.result);
            }
        }
    }
}
