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
            UpdateBeaconDisplay();
        }

        //todo: la solución más chancha que existe
        public void UpdateBeaconDisplay()
        {
            if (BeaconId1 == 0)
                btnBeaconId1.Text = "Seleccionar beacon 1...";
            else
                btnBeaconId1.Text = String.Format("Beacon 1 ID: {0}", BeaconId1);
            if (BeaconId2 == 0)
                btnBeaconId2.Text = "Seleccionar beacon 2...";
            else
                btnBeaconId2.Text = String.Format("Beacon 2 ID: {0}", BeaconId2);
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
                BeaconId1 = BeaconId1,
                BeaconId2 = BeaconId2,
                Rango = pckRange.SelectedIndex + 1,
                //la bd está en miliseg.
                Tiempo = Int32.Parse(txtTime.Text) * 1000,
                Nombre = txtName.Text
            };
            var rest = new Data.RESTService();
            var success = await rest.SaveConfig(newConfig);
            if (success)
            {
                Notifier.Inform("Configuración creada correctamente.");
                //todo: quizás esto falle cuando se haga un upsert
                paciente.Configuraciones.Add(newConfig);
                await Navigation.PopAsync();
            }
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
