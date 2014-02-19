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
using PintheCloud.Workers;
using PintheCloud.Utilities;
using Windows.Storage;
using System.Xml;
using System.IO;
using System.Threading;

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
            int mainPlatformType = 0;
            if (!App.ApplicationSettings.TryGetValue<int>(Account.ACCOUNT_MAIN_PLATFORM_TYPE_KEY, out mainPlatformType))
            {
                App.ApplicationSettings[Account.ACCOUNT_MAIN_PLATFORM_TYPE_KEY] = App.SKY_DRIVE_KEY_INDEX;
                App.ApplicationSettings.Save();
            }


            // SIgn in
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                for (int i = 0; i < App.IStorageManagers.Length; i++)
                {
                    App.IStorageManager = App.IStorageManagers[i];
                    
                    // If main platform is signed in, process it.
                    // Otherwise, ignore and go to explorer page.
                    bool isSignIn = false;
                    App.ApplicationSettings.TryGetValue<bool>(Account.ACCOUNT_IS_SIGN_IN_KEYS[i], out isSignIn);
                    if (isSignIn)
                        App.TaskManager.AddSignInTask(App.IStorageManager.SignIn(), i);
                }
            }
            NavigationService.Navigate(new Uri(EXPLORER_PAGE, UriKind.Relative));
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
    }
}