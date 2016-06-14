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
			//todo: uncomment
			var nuevoUsuario = new Usuario
			{
				Username = txtUsername.Text,
				Password = txtPassword.Text,
				Nombre = txtNombre.Text,
				Apellido = txtApellido.Text,
				//todo: check this shit
				Tipo = bitCuidante.IsToggled,
				//todo: añadir cuidante
				Telefono = txtTelefono.Text
			};
			var rest = new Data.RESTService();
			rest.SaveUser(nuevoUsuario);
		}
	}
}
