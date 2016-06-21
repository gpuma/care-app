using CareApp.Models;
using System;
using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class ConfigsView : ContentPage
    {
        public Usuario Paciente { get; set; }
        public ConfigsView(Usuario paciente)
        {
            InitializeComponent();
            Paciente = paciente;
            BindingContext = Paciente;

            //set title
            Title = String.Format("Alertas de {0}", paciente.Nombre);
        }
    }
}
