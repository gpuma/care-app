using System;
using System.Linq;
using System.Globalization;
using CareApp.Models;
using Xamarin.Forms;
using Humanizer;

namespace CareApp.Views
{
    public partial class CarerAlertView : ContentPage
    {
        Usuario User;
        Emergencia Emergencia;
        Usuario Paciente;
        CarerPatientsView parent;

        //todo: no creo, manejar varias emergencias al mismo tiempo
        public CarerAlertView(CarerPatientsView parent, Usuario user, Emergencia emergencia)
        {
            InitializeComponent();
            User = user;
            Emergencia = emergencia;
            this.parent = parent;

            //para obtener el primer nombre y teléfono del paciente
            Paciente = (from p in User.Pacientes
                            where p.Username == Emergencia.Paciente
                            select p).FirstOrDefault();
            txtMensaje.Text = ConstruirMensaje();
        }

        private string ConstruirMensaje()
        {
            //para obtener un mensaje en español
            var ci = new CultureInfo("es-pe");
            return String.Format("{0} ha sufrido una posible emergencia cerca de {1} {2}.",
                Paciente.Nombre, Emergencia.Lugar, Emergencia.Timestamp.Humanize(culture: ci));
        }

        private async void btnLlamar_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(String.Format("tel:{0}", Paciente.Telefono)));
            await Navigation.PopAsync();
            //todo: actualizar a 1 la emergencia
        }

        private async void btnAyuda_Clicked(object sender, EventArgs e)
        {
            //emergencias?
            //parent.ShouldCheckForEmergencies = true;
            //Device.OpenUri(new Uri("tel:117"));
            //Navigation.PopAsync();
            //todo: actualizar a 1 la emergencia
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            parent.ShouldCheckForEmergencies = true;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //mientras tratamos con esta emergencia no se 
            //busca si hay otras
            parent.ShouldCheckForEmergencies = false;

            //actualizar emergencia
            Emergencia.Estado = true;
            await new Data.RESTService().SaveEmergency(Emergencia, update: true);
        }
    }
}
