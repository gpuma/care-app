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
            foreach(var b in e.Beacons)
            {
                System.Diagnostics.Debug.WriteLine("****ENCONTRADO {0}, {1}****", b.Name, b.ProximityUUID);
            }
        }

        public IEnumerable<String> getBeacons()
        {
            //return beacons.Select(b => String.Format("{0}, {1}", b.ProximityUUID, b.Name));
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