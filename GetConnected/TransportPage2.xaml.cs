using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;

namespace GetConnected
{
    public partial class Page1 : PhoneApplicationPage
    {

        String transportMode;
        Dictionary<string, double> coordinatesFrom = new Dictionary<string,double>();
        Dictionary<string, double> coordinatesTo = new Dictionary<string, double>();
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public Page1()
        {
            InitializeComponent();
        }

        private void nextPage(object sender, RoutedEventArgs e)
        {
            

            TextBox textBoxFrom = (TextBox) locationFrom,
                   textBoxTo = (TextBox) locationTo,
                   textBoxDate = (TextBox) date,
                   textBoxTime = (TextBox) time;

            String textFrom = locationFrom.Text,
                   textTo = locationTo.Text,
                   textDate = date.Text,
                   textTime = time.Text;

            coordinatesFrom.Add("lat", 0);
            coordinatesFrom.Add("lng", 0);
            coordinatesTo.Add("lat", 0);
            coordinatesTo.Add("lng", 0);


            RadioButton radioButtonArrival = (RadioButton) arrival;
            bool arriveBy = (bool) arrival.IsChecked;
            /*
            String url = "https://maps.googleapis.com/maps/api/geocode/json";//?address=" + textFrom + "&region=nl&sensor=true";
            RESTRequest r = new RESTRequest(url);
            r.putValue("address", textFrom);
            r.putValue("region", "nl");
            r.putValue("sensor", "true");
            r.doRequest();
            */
            String url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + textFrom + "&region=nl&sensor=true";
            HttpWebRequest locationFromRequest = (HttpWebRequest) WebRequest.Create(url);
            locationFromRequest.Method = "GET";
            IAsyncResult result = locationFromRequest.BeginGetResponse(getLocationFromCoordinates, locationFromRequest);
            Thread.Sleep(2000);
            System.Diagnostics.Debug.WriteLine("from:" + coordinatesFrom["lat"] + "," + coordinatesFrom["lng"]);
            /*
            url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + textTo + "&region=nl&sensor=true";

            HttpWebRequest locationToRequest = (HttpWebRequest)WebRequest.Create(url);
            locationToRequest.Method = "GET";
            locationToRequest.BeginGetResponse(getLocationToCoordinates, locationToRequest);

            while (coordinatesTo["lat"] == 0)
            {
                //wait
            }
            System.Diagnostics.Debug.WriteLine("to:" + coordinatesTo["lat"] + "," + coordinatesTo["lng"]);

            url = "http://145.37.92.55:8081/opentripplanner-api-webapp/ws/plan?_dc=1382083769026&arriveBy=" + arriveBy + "&time=" + textTime + "&ui_date=" + textDate + "&date=" + textDate + "&mode=" + transportMode + "&optimize=QUICK&maxWalkDistance=1609&walkSpeed=1.341&toPlace=" + coordinatesTo["lat"] + "," + coordinatesTo["lng"] + "&fromPlace=" + coordinatesFrom["lat"] + "," + coordinatesFrom["lng"];

            System.Diagnostics.Debug.WriteLine(url);
            */
            //HttpWebRequest resultRequest = (HttpWebRequest)WebRequest.Create(url);
            //resultRequest.Method = "GET";
            //resultRequest.BeginGetResponse(getTripResults, resultRequest);

            //NavigationService.Navigate(new Uri("/TransportResultPage.xaml", UriKind.Relative));
        }

        private void getLocationFromCoordinates(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                string jsonString = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(jsonString);
                JObject jsonObject = JObject.Parse(jsonString);
                JArray jsonArray = (JArray) jsonObject["results"];
                coordinatesFrom["lat"] = (double)jsonArray[0]["geometry"]["location"]["lat"];
                coordinatesFrom["lng"] = (double)jsonArray[0]["geometry"]["location"]["lng"];
                //allDone.Reset();
            }
        }

        private void getLocationToCoordinates(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                string jsonString = streamReader.ReadToEnd();

                JObject jsonObject = JObject.Parse(jsonString);
                JArray jsonArray = (JArray)jsonObject["results"];
                coordinatesTo["lat"] = (double)jsonArray[0]["geometry"]["location"]["lat"];
                coordinatesTo["lng"] = (double)jsonArray[0]["geometry"]["location"]["lng"];
            }
        }

        private void getTripResults(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                string jsonString = streamReader.ReadToEnd();

                System.Diagnostics.Debug.WriteLine(jsonString);

                //NavigationService.Navigate(new Uri("/TransportResultPage.xaml", UriKind.Relative));
            }
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TransportPage1.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string mode = "";

            if (NavigationContext.QueryString.TryGetValue("mode", out mode))

                transportMode = mode;


        }

    }
}