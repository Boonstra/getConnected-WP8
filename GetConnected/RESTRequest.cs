using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetConnected
{
    class RESTRequest
    {

        public string url;
        private BackgroundWorker bw = new BackgroundWorker();
        private List<string> parameters;

        public RESTRequest(string url)
        {
            this.url = url;
        }

        public void doRequest()
        {
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
                string results = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(results);
            }
        }
    }
}
