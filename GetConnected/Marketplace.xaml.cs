using System;
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
    public partial class MarketplacePage : PhoneApplicationPage
    {
        public MarketplacePage()
        {
            InitializeComponent();
        }
		
		private void goBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        private void CreateRide(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateRide.xaml", UriKind.Relative));
        }
        private void OfferedRides(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/OfferedRides.xaml", UriKind.Relative));
        }
        private void UnratedRides(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/UnratedRides.xaml", UriKind.Relative));
        }
    }
}