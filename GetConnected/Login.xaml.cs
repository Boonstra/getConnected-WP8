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
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Loginbutton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Marketplace.xaml", UriKind.Relative));
        }

        private void Registerbutton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Register.xaml", UriKind.Relative));
        }
    }
}