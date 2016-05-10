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
using CareApp.Droid;
using EstimoteSdk;

[assembly: Xamarin.Forms.Dependency(typeof(Mierda))]

namespace CareApp.Droid
{
    class Mierda : Activity, IMierda
    {
        BeaconManager mgr;
        public Mierda()
        {
            mgr = new BeaconManager(Android.App.Application.Context);
            mgr.Ranging += Mgr_Ranging;
            Region region = new Region("rid", null, null, null);
            int pene = 0;
        }

        private void Mgr_Ranging(object sender, BeaconManager.RangingEventArgs e)
        {
            var beacons = e.Beacons;
        }

        public void Puta()
        {
            int a = 666;
        }

    }
}