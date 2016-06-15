using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using CareApp.Models;

namespace CareApp.Views
{
	public partial class NewUserView : ContentPage
	{
		public NewUserView()
		{
			InitializeComponent();
		}

		async void CrearUsuario(object sender, EventArgs args)
		{
            //todo: añadir check y feedback de inserción
            var nuevoUsuario = new Usuario
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                //si es paciente no es cuidante y viceversa
                Tipo = !bitPaciente.IsToggled,
                Telefono = txtTelefono.Text,
                Cuidante = txtCuidante.Text
			};
			var rest = new Data.RESTService();
			rest.SaveUser(nuevoUsuario);
		}

        private void bitPaciente_Toggled(object sender, ToggledEventArgs e)
        {
            txtCuidante.IsVisible = e.Value;
        }
    }
}
