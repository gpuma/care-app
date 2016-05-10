using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr;
using Estimotes;
using System.Diagnostics;

namespace CareApp
{
    class BeaconTest : ContentPage
    {
        Button btnStart = new Button { Text = "Comenzar Beacons" };
        Button btnStop = new Button { Text = "Detener Beacons" };
        BeaconRegion region = new BeaconRegion("pinga", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
        public BeaconTest()
        {
            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    btnStart,
                    btnStop
                }
            };
            btnStart.Clicked += (sender, e) =>
             {
                 EstimoteManager.Instance.StartRanging(region);
             };
            btnStop.Clicked += (sender, e) =>
            {
                EstimoteManager.Instance.StopRanging(region);
            };
            test();
        }

        public async void log(string msg)
        {
            await DisplayAlert("", msg, "ok...");
        }

        public async void test()
        {
            var status = await EstimoteManager.Instance.Initialize();
            if (status != BeaconInitStatus.Success)
            {
                await DisplayAlert("las cagaste", "error iniciando el gestor de beacons", "Ok...");
                return;
            }
            EstimoteManager.Instance.Ranged += (sender, beacons) =>
            {
                foreach (var b in beacons)
                {
                    Debug.WriteLine((String.Format("detectado ibeacon con uid {0} y proximidad {1}", b.Uuid, b.Proximity)));
                }
            };

        }
    }
}
