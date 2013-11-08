using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using System.Device.Location;
using System.Windows.Media;
using Windows.Devices.Geolocation;
using MapLayer = Microsoft.Phone.Maps.Controls.MapLayer;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace GetConnected
{
    public partial class Page2 : PhoneApplicationPage
    {

        private double latitude = 53.241500;
        private double longitude = 6.533199;
        private Map myMap;

        public Page2()
        {
            InitializeComponent();

            addLocationToMap();
            addBusStopsToMap();

        }

        private void addLocationToMap()
        {
            myMap = map;

            GeoCoordinate myGeocoordinate = new GeoCoordinate(latitude, longitude);

            myMap.Center = myGeocoordinate;
            myMap.ZoomLevel = 14;

            Ellipse myCircle = new Ellipse();
            myCircle.Fill =  new SolidColorBrush(Colors.Red);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;

            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0, 1);
            myLocationOverlay.GeoCoordinate = myGeocoordinate;

            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            myMap.Layers.Add(myLocationLayer);

        }

        private void addBusStopsToMap()
        {
            String url = "http://145.37.90.81/yii/sites/BusStops/api/busstop?gps_latitude=" + latitude + "&gps_longitude=" + longitude + "&range=10000";
            HttpWebRequest locationFromRequest = (HttpWebRequest)WebRequest.Create(url);
            locationFromRequest.Method = "GET";
            IAsyncResult result = locationFromRequest.BeginGetResponse(getBusStops, locationFromRequest);

        }

        private void getBusStops(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                string jsonString = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(jsonString);
                JObject jsonObject = JObject.Parse(jsonString);
                JArray jsonArray = (JArray)jsonObject["busstops"];

                
                
                Dispatcher.BeginInvoke(() =>
                {
                    MapLayer myLocationLayer = new MapLayer();
                    Ellipse myCircle;
                    MapOverlay[] busStopsOverlay = new MapOverlay[jsonArray.Count];
                    int i = 0;
                    foreach (JObject busStop in jsonArray)
                    {

                        GeoCoordinate busStopCoordinates = new GeoCoordinate((double)busStop["GPS_Latitude"], (double)busStop["GPS_Longitude"]);
                        myCircle = new Ellipse();
                        myCircle.Fill = new SolidColorBrush(Colors.Blue);
                        myCircle.Height = 20;
                        myCircle.Width = 20;
                        myCircle.Opacity = 50;
                        busStopsOverlay[i] = new MapOverlay();
                        busStopsOverlay[i].Content = myCircle;
                        busStopsOverlay[i].PositionOrigin = new Point(0, 1);
                        busStopsOverlay[i].GeoCoordinate = busStopCoordinates;

                        myLocationLayer.Add(busStopsOverlay[i]);
                        i++;
                    }

                    myMap.Layers.Add(myLocationLayer);
                });
                

            }
        }
          
    }
}