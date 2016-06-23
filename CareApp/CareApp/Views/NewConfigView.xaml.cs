using CareApp.Models;
using System;
using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class NewConfigView : ContentPage
    {
        //for our binding context
        //public EmergencyConfig CurrentConfig { get; set; }
        //no podemos tener campos que apunten a ref
        public ushort BeaconId1 { get; set; }
        public ushort BeaconId2 { get; set; }
        Usuario paciente;

        public NewConfigView(Usuario paciente)
        {
            InitializeComponent();
            this.paciente = paciente;
        }

        //todo: la solución más chancha que existe
        public void RefreshBindingContext()
        {
            BindingContext = null;
            BindingContext = this;
        }

        private void pckConfigType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnBeaconId2.IsEnabled = (pckConfigType.SelectedIndex == 2 || pckConfigType.SelectedIndex == 3);
        }

        private async void btnAddConfig_Clicked(object sender, EventArgs e)
        {
            var newConfig = new EmergencyConfig()
            {
                Paciente = paciente.Username,
                Tipo = pckConfigType.SelectedIndex,
                BeaconId1 = ushort.Parse(btnBeaconId1.Text),
                BeaconId2 = ushort.Parse(btnBeaconId2.Text),
                Rango = pckRange.SelectedIndex + 1,
                //la bd está en miliseg.
                Tiempo = Int32.Parse(txtTime.Text) * 1000,
                Nombre = txtName.Text
            };
            var rest = new Data.RESTService();
            var success = await rest.SaveConfig(newConfig);
            if (success)
                Notifier.Inform("Configuración creada correctamente.");
            else
                Notifier.Inform("No se puedo crear la configuración.");
        }
        private async void btnBeaconId1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeaconsView(this, 1));
        }
        private async void btnBeaconId2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeaconsView(this, 2));
        }
    }
}
