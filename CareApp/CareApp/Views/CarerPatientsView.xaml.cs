using CareApp.Models;
using System;
using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class CarerPatientsView : ContentPage
    {
        //public List<Usuario> Pacientes { get; set; }
        public Usuario Cuidante { get; set; }
        public bool ShouldCheckForEmergencies { get; set; }
        //en segundos
        int Frecuencia = 5;

        Data.RESTService rest = new Data.RESTService();
        public CarerPatientsView(Usuario cuidante)
        {
            InitializeComponent();
            Cuidante = cuidante;
            BindingContext = Cuidante;
            ShouldCheckForEmergencies = true;

            Device.StartTimer(TimeSpan.FromSeconds(Frecuencia), () =>
            {
                if (ShouldCheckForEmergencies)
                    CheckForEmergencies();
                //runs indefinitely
                return true;
            });
        }

        private async void CheckForEmergencies()
        {
            var emergencies = await rest.GetPendingEmergenciesFromCarerPatients(Cuidante.Username);
            //si no hay nada
            if (emergencies.Count == 0)
                return;
            //todo: por ahora solo la primera emergencia
            var e = emergencies[0];
            await Navigation.PushAsync(new CarerAlertView(this, Cuidante, e));
        }
    }
}
