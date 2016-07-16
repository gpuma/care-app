using CareApp.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.Vibrate;

namespace CareApp.Views
{
    public partial class PatientAlertView : ContentPage
    {
        PatientAlertViewModel vm = new PatientAlertViewModel();
        Usuario user;
        EmergencyConfig econfig;
        public bool ShouldCreateEmergency { get; set; }
        bool CancelEmergency = false;
        public PatientAlertView(Usuario user, EmergencyConfig econfig)
        {
            InitializeComponent();
            //vibramos el dispositivo xq se dio una emergencia
            CrossVibrate.Current.Vibration(1000);
            this.user = user;
            //la necesitamos para saber q tipo de emergencia ocurrió
            this.econfig = econfig;
            ShouldCreateEmergency = false;

            //el tiempo de cuenta a atrás con su respectivo mensaje
            vm.Tiempo = 5;
            UpdateMsg();

            BindingContext = vm;
            progBar.ProgressTo(1, vm.Tiempo * 1000, Easing.Linear);
            
            //se activa cada segundo
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                vm.Tiempo -= 1;
                //el usuario presionó q se encuentra bien
                if (CancelEmergency)
                    return false;
                UpdateMsg();
                //true -> se sigue repitiendo el vento
                //false -> se deja de usar
                return (vm.Tiempo != 0);
            });
        }

        private Emergencia PrepareEmergencyObject(EmergencyConfig econfig)
        {
            return new Emergencia
            {
                Paciente = user.Username,
                Cuidante = user.Cuidante,
                //0: unread, 1: read
                Estado = false,
                Timestamp = DateTime.Now,
                Tipo = econfig.Tipo,
                ConfigId = econfig.Id,
                Lugar = econfig.Nombre
            };
        }

        private async void UpdateMsg()
        {
            if (vm.Tiempo != 0)
            {
                vm.MsjTiempo = String.Format("Creando alarma en {0} seg...", vm.Tiempo);
                return;
            }
            //todo: quizas mover esto o cambiarle el nombre al método
            vm.MsjTiempo = String.Format("Creando alarma...", vm.Tiempo);
            //todo: añadir check de validacion si funcionó
            var res = await new Data.RESTService().SaveEmergency(PrepareEmergencyObject(econfig));

            //success
            if (res)
            {
                vm.MsjTiempo = String.Format("Alarma creada", vm.Tiempo);
                await Navigation.PopAsync();
            }
            //todo: try to create alarm again?
        }

        private void btnAyuda_Clicked(object sender, EventArgs e)
        {
            MakeCall();
        }

        //todo: make it so it calls directly, if it's possible
        private void MakeCall()
        {
            //abre la interfaz de telefono nativa al usuario
            Device.OpenUri(new Uri(String.Format("tel:{0}", this.user.TelCuidante)));
            //todo: crashes when back button has already been pressed
            Navigation.PopAsync();
        }

        private void btnOk_Clicked(object sender, EventArgs e)
        {
            CancelEmergency = true;
            Navigation.PopAsync();
        }
    }

    public class PatientAlertViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public uint Tiempo { get; set; }
        string msjTiempo;
        public string MsjTiempo
        {
            get { return msjTiempo; }
            set
            {
                if (value == msjTiempo)
                    return;
                msjTiempo = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MsjTiempo"));
            }
        }
    }
}