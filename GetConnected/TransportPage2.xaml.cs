using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace GetConnected
{
    public partial class Page1 : PhoneApplicationPage
    {

        String transportMode;

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

            RadioButton radioButtonArrival = (RadioButton) arrival;
            bool arriveBy = (bool) arrival.IsChecked;

            String url = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + textFrom + "&region=nl&sensor=true";

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            request.BeginGetResponse(r =>
            {
                HttpWebResponse response = (HttpWebResponse) request.EndGetResponse(r);

                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string xml = streamReader.ReadToEnd();
                    System.Diagnostics.Debug.WriteLine(xml);
                }
            }, null);
            System.Diagnostics.Debug.WriteLine(url);


            //System.Diagnostics.Debug.WriteLine(url);


            //NavigationService.Navigate(new Uri("/TransportResultPage.xaml", UriKind.Relative));
        }

        /*private string getResponse(HttpWebRequest request)
        {
            request.Method = "POST";
            request.BeginGetResponse(r =>
            {
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(r);

                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    return 
                }
            }, null);

            return;
        }*/

        private void Response_Completed(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                string xml = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(xml);
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