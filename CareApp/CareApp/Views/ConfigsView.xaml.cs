using CareApp.Models;
using System;
using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class ConfigsView : ContentPage
    {
        public Usuario Paciente { get; set; }
        public ConfigsView(Usuario paciente)
        {
            InitializeComponent();
            Paciente = paciente;
            //LoadValues();
        }

        public void LoadValues()
        {
            BindingContext = null;
            BindingContext = Paciente;

            BeaconManager.SetRequirements(Paciente, this);

            //set title
            Title = String.Format("Configuraciones de {0} {1}", Paciente.Nombre, Paciente.Apellido);
        }

        private async void btnNewConfig_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewConfigView(Paciente));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadValues();
            BeaconManager.Start();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BeaconManager.Stop();
        }
    }
}
