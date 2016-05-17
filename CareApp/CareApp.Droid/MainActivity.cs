using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.Toasts;
using Xamarin.Forms;

namespace CareApp.Droid
{
    [Activity(Label = "CareApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //registramos la implementación Android de nuestro Toast plugin
            Xamarin.Forms.DependencyService.Register<ToastNotificatorImplementation>();
            ToastNotificatorImplementation.Init(this);

            //para nuestro mensaje background
            MessagingCenter.Subscribe<StartRunningTaskMessage>(this,
                "StartRunningTaskMessage", message =>
                {
                    var intent = new Intent(this, typeof(RunningTaskService));
                    StartService(intent);
                }
            );

            MessagingCenter.Subscribe<StopRunningTaskMessage>(this,
               "StopRunningTaskMessage", message =>
               {
                   var intent = new Intent(this, typeof(RunningTaskService));
                   StopService(intent);
               }
            );

            LoadApplication(new App());
        }
    }
}

