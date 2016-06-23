
using CareApp.Models;
using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class PatientView : ContentPage
    {
        public Usuario User { get; set; }
        public PatientView(Usuario user)
        {
            InitializeComponent();
            User = user;
            BeaconManager.SetRequirements(User, this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BeaconManager.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BeaconManager.Stop();
        }
    }
}
