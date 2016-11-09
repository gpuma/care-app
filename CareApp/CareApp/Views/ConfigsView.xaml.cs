using CareApp.Models;
using System;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace CareApp.Views
{
    public partial class ConfigsView : ContentPage
    {
        public Usuario Paciente { get; set; }
        public ConfigsView(Usuario paciente)
        {
            InitializeComponent();
            Paciente = paciente;
        }

        //al darle clic a una configuración se le presenta al usuario la opción de borrarla
        async void lstConfigs_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var deseaEliminar = await DisplayAlert("Confirmación", "¿Desea eliminar esta configuración?", "Sí", "No");

            if (!deseaEliminar)
                return;

            var configSeleccionada = (EmergencyConfig)lstConfigs.SelectedItem;
            bool success = false;
            var progConfig = new  ProgressDialogConfig { Title = "Iniciando sesión...", AutoShow = true };
            using (var progDialog = UserDialogs.Instance.Progress(progConfig))
            {
                var rest = new Data.RESTService();
                success = await rest.DeleteConfig(configSeleccionada.Id);
            }
            if (success)
            {
                Notifier.Inform("Configuración borrada exitosamente.");
                Paciente.Configuraciones.Remove(configSeleccionada);
                LoadValues();
            }
            else
            {
                Notifier.Inform("No se pudo borrar la configuración.");
            }
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
