using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr;
using Estimotes;

namespace CareApp
{
    class BeaconTest : ContentPage
    {
        Button btnStart = new Button { Text = "Comenzar Beacons" };
        Button btnStop = new Button { Text = "Detener Beacons" };
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
                 EstimoteManager.Instance.StartRanging(new BeaconRegion("blueberry", "B9407F30-F5F8-466E-AFF9-25556B57FE6D"));
             };
            btnStop.Clicked += (sender, e) =>
            {
                EstimoteManager.Instance.StopRanging(new BeaconRegion("blueberry", "B9407F30-F5F8-466E-AFF9-25556B57FE6D"));
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
                log(String.Format("Evento Ranged fue activado con proximidad {0}", beacons.First().Proximity));
            };

        }
    }
}
