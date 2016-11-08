using CareApp.Models;
using System;
using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class NewUserView : ContentPage
	{
        ContentPage parent;
        //si es verdadero el formulario fue llamado desde LoginView
        //si es falso fue llamado desde CarerPatientsView
        bool isLogin;
		public NewUserView(ContentPage parent, bool isLogin = true)
		{
			InitializeComponent();
            this.parent = parent;
            this.isLogin = isLogin;

            //cuando fue llamado desde CarerPatientsView le asignamos
            //de frente el Paciente al usuario actual, ya que se supone
            //que es él mismo el que lo creo
            if (!isLogin)
            {
                this.bitPaciente.IsEnabled = false;
                this.bitPaciente.IsToggled = true;
                //lo oculta completamente
                //this.txtCuidante.readon = false;
                this.txtCuidante.Text = ((CarerPatientsView)parent).Cuidante.Username;
            }
		}

        void btnNuevoUsuario_Clicked(object sender, EventArgs args)
        {
            CrearUsuario();
        }

        async void CrearUsuario()
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
            {
                Notifier.Inform("Usuario creado correctamente.");
                if (isLogin)
                {
                    //ya que fue creado exitosamente lo pasamos
                    //directamente para que se loguee de frente
                    ((LoginView)parent).user = nuevoUsuario;
                    await Navigation.PopAsync();
                    ((LoginView)parent).ProceedWithLogin();
                }
                else
                {
                    ((CarerPatientsView)parent).RefrescarPacientes();
                    await Navigation.PopAsync();
                }
            }
            else
                Notifier.Inform("No se puedo crear el usuario.");
        }

        private void bitPaciente_Toggled(object sender, ToggledEventArgs e)
        {
            txtCuidante.IsVisible = e.Value;
        }
    }
}
