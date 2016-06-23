using CareApp.Models;
using System;
using Xamarin.Forms;

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
			var success = await rest.SaveUser(nuevoUsuario);
            if (success)
                Notifier.Inform("Usuario creado correctamente.");
            else
                Notifier.Inform("No se puedo crear el usuario.");
        }

        private void bitPaciente_Toggled(object sender, ToggledEventArgs e)
        {
            txtCuidante.IsVisible = e.Value;
        }
    }
}
