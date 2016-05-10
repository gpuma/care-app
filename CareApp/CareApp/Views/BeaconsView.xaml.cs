using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel

using Xamarin.Forms;
using CareApp.Models;

namespace CareApp.Views
{
    public partial class BeaconsView : ContentPage
    {
        bool isScanning = false;
        
        //este tipo de colección se encarga de actualizar la interfaz
        //según el cambio en los datos, justo lo que queremos
        public ObservableCollection<Beacon> Beacons { get; set; }
        //obtenemos la implementación específica a la plataforma que estemos corriendo
        IBeaconFinder beaconFinder = DependencyService.Get<IBeaconFinder>();

        public BeaconsView()
        {
            InitializeComponent();


            //dummy beacons
            //todo: cambiar a los reales
            Beacons = new ObservableCollection<Beacon> {
                new Beacon { Uuid = "caca", Major = 30, Minor = 10, RelativeDistance = "Lejos", Type = "iBeacon"},
                new Beacon { Uuid = "caca2", Major = 30, Minor = 10, RelativeDistance = "Cerca", Type = "iBeacon"},
                new Beacon { Uuid = "caca3", Major = 30, Minor = 10, RelativeDistance = "Aquisito", Type = "iBeacon"}
            };

            //crucial ponerlo al final para que no hayan problemas de binding
            BindingContext = this;
        }

        //para iniciar o detener el escaneo de beacons
        void OnScan_Clicked(object sender, EventArgs args)
        {
            //simple toggle
            isScanning = !isScanning;
            if(isScanning)
            {
                beaconFinder.startScanning();
                btnScan.Text = "Parar";

                //todo: MAIN THING IS TO USE THE MUTLIPLAT BEACON LIBRARY ARC

                var scannedBeacons = beaconFinder.getBeacons();

                Beacons.Clear();
                //todo: crear un metodo que convierta automáticamente al tipo de beacon
                foreach(var sb in scannedBeacons)
                {
                    //Beacons.Add(new Beacon() { Uuid = sb.u })
                }

            }
            else
            {
                beaconFinder.stopScanning();
                btnScan.Text = "Escanear";
            }
        }

        //cuando se le da click a un beacon
        async void OnList_Tapped(object sender, EventArgs args)
        {
            //todo: implementar
        }
    }
}
