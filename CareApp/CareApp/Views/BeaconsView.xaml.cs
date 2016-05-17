using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Estimotes;
using Xamarin.Forms;
using CareApp.Models;
using System.Diagnostics;

namespace CareApp.Views
{
    public partial class BeaconsView : ContentPage
    {
        bool isScanning = false;
        
        //este tipo de colección se encarga de actualizar la interfaz
        //según el cambio en los datos, justo lo que queremos
        public ObservableCollection<Beacon> Beacons { get; set; }

        //usando la uid default de estimote
        //todo: poner esta info en un txt
        BeaconRegion defaultRegion = new BeaconRegion("test-region", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");

        public BeaconsView()
        {
            InitializeComponent();

            this.Beacons = new ObservableCollection<Beacon>();

            //crucial ponerlo al final para que no hayan problemas de binding
            BindingContext = this;

            //añadimos el gestor de eventos para beacons
            EstimoteManager.Instance.Ranged += Beacons_Ranged;
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

        //para iniciar o detener el escaneo de beacons
        async void OnScan_Clicked(object sender, EventArgs args)
        {
            //todo: chequear si cada vez que se clickea debe hacerse esto
            //inicializamos el plugin
            var status = await EstimoteManager.Instance.Initialize();
            if(status!=  BeaconInitStatus.Success)
            {
                throw new Exception("unable to start beacon manager");
            }         

            //simple toggle
            isScanning = !isScanning;
            if(isScanning)
            {
                EstimoteManager.Instance.StartRanging(defaultRegion);
                btnScan.Text = "Parar";
            }
            else
            {
                EstimoteManager.Instance.StopRanging(defaultRegion);
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
