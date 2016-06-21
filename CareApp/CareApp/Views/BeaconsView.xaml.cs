using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Estimotes;
using Xamarin.Forms;
using CareApp.Models;
using System.Diagnostics;
using Plugin.Toasts;
//using System.Threading.Tasks;
//using System.Threading;

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

        //void HandleReceivedMessages()
        //{
        //    MessagingCenter.Subscribe<TickedMessage>(this, "TickedMessage", message => {
        //        //todo: check this async shit
        //        Device.BeginInvokeOnMainThread(async () => {
        //            //lblText.Text = message.Message;
        //            await notificator.Notify(
        //                ToastNotificationType.Info,
        //                "CHI", message.Message, TimeSpan.FromSeconds(1));
        //            });
        //    });

        //    MessagingCenter.Subscribe<CancelledMessage>(this, "CancelledMessage", message => {
        //        Device.BeginInvokeOnMainThread(async () => {
        //            await notificator.Notify(
        //                ToastNotificationType.Info,
        //                "NO", "Cancelled", TimeSpan.FromSeconds(1));
        //        });
        //    });
        //}

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

        ////para iniciar o detener el escaneo de beacons
        //async void OnScan_Clicked(object sender, EventArgs args)
        //{
        //    //todo: chequear si cada vez que se clickea debe hacerse esto
        //    //inicializamos el plugin
        //    var status = await EstimoteManager.Instance.Initialize();
        //    if(status!=  BeaconInitStatus.Success)
        //    {
        //        throw new Exception("unable to start beacon manager");
        //    }         

        //    //simple toggle
        //    isScanning = !isScanning;
        //    if(isScanning)
        //    {
        //        EstimoteManager.Instance.StartRanging(defaultRegion);
        //        btnScan.Text = "Parar";
        //    }
        //    else
        //    {
        //        EstimoteManager.Instance.StopRanging(defaultRegion);
        //        btnScan.Text = "Escanear";
        //    }
        //}

        ////cuando se le da click a un beacon
        //async void OnList_Tapped(object sender, EventArgs args)
        //{

        //}

        //void OnStart_Clicked(object sender, EventArgs args)
        //{
        //    BeaconManager.Start();
        //    //var msg = new StartRunningTaskMessage();
        //    //MessagingCenter.Send(msg, "StartRunningTaskMessage");
        //}

        //void OnStop_Clicked(object sender, EventArgs args)
        //{
        //    BeaconManager.Stop();
        //    //var msg = new StopRunningTaskMessage();
        //    //MessagingCenter.Send(msg, "StopRunningTaskMessage");
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BeaconManager.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BeaconManager.Stop();
        }
    }
}
