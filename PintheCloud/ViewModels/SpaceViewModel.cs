﻿using Microsoft.WindowsAzure.MobileServices;
using PintheCloud.Models;
using PintheCloud.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PintheCloud.ViewModels
{
    public class SpaceViewModel : INotifyPropertyChanged
    {
        public static string LIKE_NOT_PRESS_IMAGE_PATH = "/Assets/pajeon/png/general_like.png";
        public static string LIKE_PRESS_IMAGE_PATH = "/Assets/pajeon/png/general_like_p.png";

        public ObservableCollection<SpaceViewItem> Items { get; private set; }
        
        // Mutex
        public bool IsDataLoaded { get; set; }


        public SpaceViewModel()
        {
            this.Items = new ObservableCollection<SpaceViewItem>();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
