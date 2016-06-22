using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using CareApp.Models;

namespace CareApp.Views
{
    public partial class PatientView : ContentPage
    {
        public Usuario User { get; set; }
        public PatientView(Usuario user)
        {
            InitializeComponent();
            User = user;
            BeaconManager.SetUser(User);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BeaconManager.Start();
        }

        //todo: check this shit, if we change to another view
        //it will probably stop scanning; do we want that?
        protected override void OnDisappearing()
        {
            BeaconManager.Stop();
        }
    }
}
