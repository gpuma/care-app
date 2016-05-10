using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using CareApp.Models;

namespace CareApp.Views
{
    public partial class BeaconsView : ContentPage
    {
        bool isScanning = false;
        
        public IList<Beacon> Beacons { get; set; }

        public BeaconsView()
        {
            InitializeComponent();


            //dummy beacons
            //todo: cambiar a los reales
            Beacons = new Beacon[] {
                new Beacon { Uuid = "caca", Major = 30, Minor = 10, RelativeDistance = "Lejos", Type = "iBeacon"},
                new Beacon { Uuid = "caca2", Major = 30, Minor = 10, RelativeDistance = "Cerca", Type = "iBeacon"},
                new Beacon { Uuid = "caca3", Major = 30, Minor = 10, RelativeDistance = "Aquisito", Type = "iBeacon"}
            };
            BindingContext = this;
        }

        //para iniciar o detener el escaneo de beacons
        void OnScan_Clicked(object sender, EventArgs args)
        {
            isScanning = !isScanning;
            //todo: toggle scan
            btnScan.Text = isScanning ? "Parar" : "Escanear";
        }

        //cuando se le da click a un beacon
        async void OnList_Tapped(object sender, EventArgs args)
        {
            //todo: implementar
        }
    }
}
