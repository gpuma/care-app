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
            var success = await rest.Login(txtUsername.Text, txtPassword.Text);
            if (success == null)
                Notifier.Inform("error logueandose");
            else
                Notifier.Inform("successful login");
        }
    }
}
