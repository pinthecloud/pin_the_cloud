﻿using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using PintheCloud.Models;
using PintheCloud.Pages;
using PintheCloud.Resources;
using PintheCloud.ViewModels;
using PintheCloud.Workers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace PintheCloud.Managers
{
    public class SpaceManagerImplement : SpaceManager
    {
        /*** Instance ***/

        private SpaceWorker CurrentSpaceWorker = null;
        public void SetAccountWorker(SpaceWorker CurrentSpaceWorker)
        {
            this.CurrentSpaceWorker = CurrentSpaceWorker;
        }

        /*** Implementation ***/

        // Get space view item from space list.
        public async Task<bool> SetNearSpaceViewItemsToSpaceViewModelAsync(Geoposition currentGeoposition)
        {
            // Get current coordinate
            double currentLatitude = currentGeoposition.Coordinate.Latitude;
            double currentLongtitude = currentGeoposition.Coordinate.Longitude;

            // Get spaces formed JArray
            JArray spaces = await this.CurrentSpaceWorker.GetNearSpacesAsync(currentLatitude, currentLongtitude);

            // Convert jarray spaces to space view items
            if (spaces != null)
            {
                foreach (JObject space in spaces)
                {
                    string space_name = (string)space["space_name"];
                    double space_latitude = (double)space["space_latitude"];
                    double space_longtitude = (double)space["space_longtitude"];
                    string account_id = (string)space["account_id"];
                    int space_like_number = (int)space["space_like_number"];
                    ExplorerPage.NearSpaceViewModel.Items.Add(this.MakeSpaceViewItemFromSpace
                        (new Space(space_name, space_latitude, space_longtitude, account_id, space_like_number), currentLatitude, currentLongtitude));
                }
            }
            if (ExplorerPage.NearSpaceViewModel.Items.Count > 0)
                return true;
            else
                return false;
        }


        // Get space view item from space list.
        public async Task<bool> SetMySpaceViewItemsToSpaceViewModelAsync()
        {
            // Get spaces
            MobileServiceCollection<Space, Space> spaces = await this.CurrentSpaceWorker
                .GetMySpacesAsync(App.CurrentAccountManager.GetCurrentAcccount().account_platform_id);

            // Convert spaces to space view items
            if (spaces != null)
            {
                foreach (Space space in spaces)
                    ExplorerPage.MySpaceViewModel.Items.Add(this.MakeSpaceViewItemFromSpace(space));
            }
            if (ExplorerPage.MySpaceViewModel.Items.Count > 0)
                return true;
            else
                return false;
        }



        /*** Self Method ***/

        // Make new space view item from space model object.
        private SpaceViewItem MakeSpaceViewItemFromSpace(Space space, double currentLatitude = -1, double currentLongtitude = -1)
        {
            // Set new space view item
            SpaceViewItem spaceViewItem = new SpaceViewItem();
            spaceViewItem.SpaceName = space.space_name;
            spaceViewItem.SpaceLikeDescription = space.space_like_number + " " + AppResources.LikeDescription;

            // If it requires distance, set distance description
            // Otherwise, Set blank.
            if (currentLatitude != -1)
            {
                double distance = App.CurrentGeoCalculateManager.GetDistanceBetweenTwoCoordiantes
                    (currentLatitude, space.space_latitude, currentLongtitude, space.space_longtitude);
                spaceViewItem.SpaceDescription = Math.Round(distance) + " " + AppResources.DistanceDescription;
            }
            else
            {
                spaceViewItem.SpaceDescription = "";
            }
            return spaceViewItem;
        }
    }
}