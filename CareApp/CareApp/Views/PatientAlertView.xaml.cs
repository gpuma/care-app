using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class PatientAlertView : ContentPage
    {
        PatientAlertViewModel vm = new PatientAlertViewModel();
        public PatientAlertView()
        {
            InitializeComponent();

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
            throw new NotImplementedException();
        }

        private void btnOk_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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