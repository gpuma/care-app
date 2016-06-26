using System;
using Xamarin.Forms;

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
            var rest = new Data.RESTService();
            user = await rest.Login(txtUsername.Text, txtPassword.Text);
            if (user == null)
                Notifier.Inform("error logueandose");
            else
                Notifier.Inform("successful login");

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
