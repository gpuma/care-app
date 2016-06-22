using Xamarin.Forms;
using CareApp.Views;

namespace CareApp
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            //navigation page necesario parar hacer pushasync
            MainPage = new NavigationPage(new LoginView());
            //var rest = new Data.RESTService();
            //var user = rest.Login("vash", "vash").Result;
            //MainPage = new PatientsView(user);
		}

		protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
