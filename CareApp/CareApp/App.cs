using Xamarin.Forms;
using CareApp.Views;

namespace CareApp
{
    public class App : Application
    {
        public App()
        {
			// The root page of your application
			//MainPage = new MainTabbedView();
			MainPage = new NewUserView();
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
