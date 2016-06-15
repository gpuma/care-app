using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using CareApp.Models;

namespace CareApp.Views
{
    public partial class PatientsView : ContentPage
    {
        //public List<Usuario> Pacientes { get; set; }
        public Usuario Cuidante { get; set; }
        public PatientsView(Usuario cuidante)
        {
            InitializeComponent();
            Cuidante = cuidante;
            BindingContext = Cuidante;
        }
    }
}
