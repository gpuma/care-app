using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CareApp
{
    public partial class Page1 : ContentPage
    {
        IBeaconFinder beaconFinder;
        public Page1()
        {
            InitializeComponent();
            lista.ItemsSource = new string[] { "pinga", "ano", "chucha", "teta" };
            beaconFinder = DependencyService.Get<IBeaconFinder>();
        }
        void btnIniciar_Clicked(object sender, EventArgs args)
        {
            beaconFinder.startScanning();
        }
        void btnParar_Clicked(object sender, EventArgs args)
        {
            beaconFinder.stopScanning();
        }
        void btnLeer_Clicked(object sender, EventArgs args)
        {
            var pinga = beaconFinder.getBeacons();
        }
    }
}
