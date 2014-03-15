﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PintheCloud.Resources;
using System.Threading.Tasks;
using Microsoft.Live;
using Microsoft.WindowsAzure.MobileServices;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Net.NetworkInformation;
using PintheCloud.Models;
using PintheCloud.Managers;
using PintheCloud.Utilities;
using Windows.Storage;
using System.Xml;
using System.IO;
using System.Threading;
using PintheCloud.Helpers;

namespace PintheCloud.Pages
{
    public partial class SplashPage : PtcPage
    {
        // 생성자
        public SplashPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Check main platform at frist login.
            //if (!App.ApplicationSettings.Contains(StorageAccount.ACCOUNT_MAIN_PLATFORM_TYPE_KEY))
            //{
            //    App.ApplicationSettings[StorageAccount.ACCOUNT_MAIN_PLATFORM_TYPE_KEY] = StorageAccount.StorageAccountType.ONE_DRIVE;
            //    App.ApplicationSettings.Save();
            //}

            // Check nick name at frist login.
            if (!App.ApplicationSettings.Contains(StorageAccount.ACCOUNT_DEFAULT_SPOT_NAME_KEY))
            {
                App.ApplicationSettings[StorageAccount.ACCOUNT_DEFAULT_SPOT_NAME_KEY] = AppResources.AtHere;
                App.ApplicationSettings.Save();
            }

            // Check location access consent at frist login.
            if (!App.ApplicationSettings.Contains(StorageAccount.LOCATION_ACCESS_CONSENT_KEY))
            {
                App.ApplicationSettings[StorageAccount.LOCATION_ACCESS_CONSENT_KEY] = false;
                App.ApplicationSettings.Save();
            }

            //NavigationService.Navigate(new Uri("/Utilities/TestDrive.xaml", UriKind.Relative));

            if (App.AccountManager.IsSignIn())
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    App.TaskHelper.AddTask(App.AccountManager.GetPtcId(), App.AccountManager.SignIn());
                }
                NavigationService.Navigate(new Uri(EventHelper.EXPLORER_PAGE, UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri(EventHelper.PROFILE_PAGE, UriKind.Relative));
            }
            
            
        }
    }
}