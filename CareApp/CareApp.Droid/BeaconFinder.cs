using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EstimoteSdk;
//using Estimotes;
using CareApp.Droid;
using Model = CareApp.Models;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency (typeof (BeaconFinder))]

namespace CareApp.Droid
{
    class BeaconFinder : Activity, IBeaconFinder, BeaconManager.IServiceReadyCallback
    {
        Region region = new Region("rid", null, null, null);
        BeaconManager beaconMgr;
        IList<Beacon> beacons;

        public BeaconFinder()
        {
            beaconMgr = new BeaconManager(Android.App.Application.Context);
            beaconMgr.Ranging += BeaconMgr_Ranging;
        }

        private void BeaconMgr_Ranging(object sender, BeaconManager.RangingEventArgs e)
        {
            beacons = e.Beacons;
            foreach(var b in e.Beacons)
            {
                System.Diagnostics.Debug.WriteLine("****ENCONTRADO {0}, {1}****", b.Name, b.ProximityUUID);
            }
        }

        public IEnumerable<Model.Beacon> getBeacons()
        {
            //wait NIGGA
            //return beacons.Select(b => new Model.Beacon() {
            //    Uuid = b.ProximityUUID.ToString(),
            //    Major = b.Major,
            //    Minor = b.Minor,
            //    RelativeDistance = b.
            //};
            return null;
        }

        public void startScanning()
        {
            beaconMgr.Connect(this);
            beaconMgr.StartRanging(region);
        }

        public void stopScanning()
        {
            //beaconMgr.StopRanging(region);
            beaconMgr.Disconnect();
        }

        public void OnServiceReady()
        {
            beaconMgr.StartRanging(region);
        }
    }
}