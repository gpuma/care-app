using CareApp.Models;
using System;
using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class CarerPatientsView : ContentPage
    {
        public Usuario Cuidante { get; set; }
        public bool ShouldCheckForEmergencies { get; set; }
        //en segundos
        int Frecuencia = 5;

        Data.RESTService rest = new Data.RESTService();
        public CarerPatientsView(Usuario cuidante)
        {
            InitializeComponent();
            Cuidante = cuidante;
            Title = "Pacientes de " + Cuidante.Nombre;
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

        async void btnNewPatient_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewUserView(this, false));
        }

        //usado cuando crea un paciente desde su interfaz
        public async void RefrescarPacientes()
        {
            Cuidante = await rest.Login(Cuidante.Username, Cuidante.Password);
            BindingContext = null;
            BindingContext = Cuidante;
        }
    }
}
