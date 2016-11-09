using System;
using Xamarin.Forms;
using Acr.UserDialogs;
using Plugin.TextToSpeech;
//using Plugin.TextToSpeech.Abstractions;

namespace CareApp.Views
{
    public partial class LoginView : ContentPage
    {
        public Models.Usuario user { get; set; }
        public LoginView()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            //para mostrar el circulito de progreso
            var progConfig = new ProgressDialogConfig { Title = "Iniciando sesión...", AutoShow = true };
            using (var progDialog = UserDialogs.Instance.Progress(progConfig))
            {
                var rest = new Data.RESTService();
                user = await rest.Login(txtUsername.Text, txtPassword.Text);
            }

            if (user == null)
            {
                Notifier.Inform("Error al iniciar sesión.");
            }
            else
            {
                Notifier.Inform(string.Format("Bienvenido, {0}.", user.Nombre));
                CrossTextToSpeech.Current.Speak(string.Format("Bienvenido, {0}.", user.Nombre));
            }
            ProceedWithLogin();
        }

        public async void ProceedWithLogin()
        {
            //si es cuidante o paciente
            if (user.Tipo)
                await Navigation.PushAsync(new CarerPatientsView(user));
            else
                await Navigation.PushAsync(new ConfigsView(user));
        }

        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewUserView(this));
        }
    }
}
