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
    public partial class Page3 : PhoneApplicationPage
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void mode_Copy1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private String getTransportMode()
        {
            bool bus = (bool) mode_bus.IsChecked,
                  train = (bool)mode_train.IsChecked,
                  taxiOther = (bool)mode_taxiother.IsChecked;

            if (!bus && !train && taxiOther)
            {
                return "WALK";
            }
            else
            {
                return "TRANSIT,WALK";
            }
    
        }

        private void nextPage(object sender, RoutedEventArgs e)
        {
            String mode = getTransportMode();
            NavigationService.Navigate(new Uri("/TransportPage2.xaml?mode=" + mode, UriKind.Relative));
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}