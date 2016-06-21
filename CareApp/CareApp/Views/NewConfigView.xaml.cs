using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using CareApp.Models;

namespace CareApp.Views
{
    public partial class NewConfigView : ContentPage
    {
        //for our binding context
        //public EmergencyConfig CurrentConfig { get; set; }
        //no podemos tener campos que apunten a ref
        public ushort BeaconId1 { get; set; }
        public ushort BeaconId2 { get; set; }

        public NewConfigView()
        {
            InitializeComponent();
            //BindingContext = this;
        }

        //todo: la solución más chancha que existe
        public void RefreshBindingContext()
        {
            BindingContext = null;
            BindingContext = this;
        }

        private void pckRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pckConfigType_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnAddConfig_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private async void btnBeaconId1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeaconsView(this, 1));
        }
        private async void btnBeaconId2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BeaconsView(this, 2));
        }
    }
}
