using System;
using System.ComponentModel;
using Xamarin.Forms;
using CareApp.Models;

namespace CareApp.Views
{
    public partial class PatientAlertView : ContentPage
    {
        PatientAlertViewModel vm = new PatientAlertViewModel();
        Usuario user;
        public PatientAlertView(Usuario user)
        {
            InitializeComponent();
            this.user = user;

            //el tiempo de cuenta a atrás con su respectivo mensaje
            vm.Tiempo = 5;
            UpdateMsg();

            BindingContext = vm;
            progBar.ProgressTo(1, vm.Tiempo * 1000, Easing.Linear);
            
            //se activa cada segundo
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                vm.Tiempo -= 1;
                UpdateMsg();
                //true -> se sigue repitiendo el vento
                //false -> se deja de usar
                return (vm.Tiempo != 0);
            });
        }

        private void UpdateMsg()
        {
            if (vm.Tiempo != 0)
                vm.MsjTiempo = String.Format("Contactando a cuidante en {0} seg...", vm.Tiempo);
            else
                vm.MsjTiempo = String.Format("Contactando a cuidante...", vm.Tiempo);
        }

        private void btnAyuda_Clicked(object sender, EventArgs e)
        {
            MakeCall();
        }

        private void MakeCall()
        {
            //abre la interfaz de telefono nativa al usuario
            Device.OpenUri(new Uri(String.Format("tel:{0}", this.user.TelCuidante)));
            Navigation.PopAsync();
        }

        private void btnOk_Clicked(object sender, EventArgs e)
        {
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