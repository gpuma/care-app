using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var rest = new Data.RESTService();
            var user = await rest.Login(txtUsername.Text, txtPassword.Text);
            if (user == null)
                Notifier.Inform("error logueandose");
            else
                Notifier.Inform("successful login");

            //si es cuidante o paciente
            if(user.Tipo)
                await Navigation.PushAsync(new PatientsView(user));
            else
                await Navigation.PushAsync(new ConfigsView(user));
        }
    }
}
