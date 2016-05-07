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

namespace CareApp.Droid
{
    public class NearableActivity : Activity, BeaconManager.IServiceReadyCallback
    {
        BeaconManager beaconManager;
        bool isScanning;
        string scanId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // creamos al administrador de beacons
            beaconManager = new BeaconManager(this);

            beaconManager.Eddystone += (sender, e) =>
             {
                 ActionBar.Subtitle = string.Format("Found {0} eddystones.", e.Eddystones.Count);
             };
            beaconManager.Connect(this);
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (!isScanning)
                return;

            isScanning = false;
            beaconManager.StopEddystoneScanning(scanId);
        }
        public void OnServiceReady()
        {
            isScanning = true;
            scanId = beaconManager.StartEddystoneScanning();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            beaconManager.Disconnect();
        }
    }
}