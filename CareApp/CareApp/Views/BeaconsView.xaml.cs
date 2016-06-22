using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Estimotes;
using Xamarin.Forms;
using CareApp.Models;
using System.Diagnostics;
using Plugin.Toasts;

namespace CareApp.Views
{
    public partial class BeaconsView : ContentPage
    {
        bool isScanning = false;
        //para los Toasts
        IToastNotificator notificator = DependencyService.Get<IToastNotificator>();        
        
        //este tipo de colección se encarga de actualizar la interfaz
        //según el cambio en los datos, justo lo que queremos
        public ObservableCollection<Beacon> Beacons { get; set; }

        //usando la uid default de estimote
        //todo: poner esta info en un txt
        BeaconRegion defaultRegion = new BeaconRegion("test-region", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");

        NewConfigView parent;

        short beaconNumber;

        //beaconNumber = 1 o 2
        public BeaconsView(NewConfigView parent, short beaconNumber)
        {
            InitializeComponent();

            this.Beacons = new ObservableCollection<Beacon>();
            this.parent = parent;
            this.beaconNumber = beaconNumber;

            //crucial ponerlo al final para que no hayan problemas de binding
            BindingContext = this;

            //añadimos el gestor de eventos para beacons
            //todo: creo q hay q desuscribir este evento cada vez q se
            //pare el gestor para no intervenir con el otro servicio
            EstimoteManager.Instance.Ranged += Beacons_Ranged;

            //desactivamos la detección normal de nuestro gestor de beacons
            //mientras seleccionamos
            BeaconManager.EnableRanging = false;
        }

        private async void lstBeacons_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var beacon = e.Item as Beacon;
            if (this.beaconNumber == 1)
                this.parent.BeaconId1 = beacon.Major;
            else
                this.parent.BeaconId2 = beacon.Major;

            //jajajajajajajajaja
            this.parent.RefreshBindingContext();
            await Navigation.PopAsync();
        }

        private void Beacons_Ranged(object s, IEnumerable<IBeacon> beacons)
        {
            //reiniciamos la lista cada en cada evento de detección
            this.Beacons.Clear();
            //convert this type of beacon to our beacon model
            foreach (var b in beacons)
            {
                this.Beacons.Add(new Beacon
                {
                    Uuid = b.Uuid,
                    Major = b.Major,
                    Minor = b.Minor,
                    RelativeDistance = b.Proximity.ToString()
                });
            }
            Debug.WriteLine(String.Format("found {0}", Beacons.Count));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BeaconManager.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BeaconManager.EnableRanging = true;
            BeaconManager.Stop();
        }
    }
}
