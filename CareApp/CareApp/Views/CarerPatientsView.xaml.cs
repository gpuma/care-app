using Xamarin.Forms;
using CareApp.Models;

namespace CareApp.Views
{
    public partial class CarerPatientsView : ContentPage
    {
        //public List<Usuario> Pacientes { get; set; }
        public Usuario Cuidante { get; set; }

        public CarerPatientsView(Usuario cuidante)
        {
            InitializeComponent();
            Cuidante = cuidante;
            BindingContext = Cuidante;
        }
    }
}
