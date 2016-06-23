using Plugin.Toasts;
using System;
using Xamarin.Forms;

namespace CareApp
{
    public static class Notifier
    {
        static IToastNotificator notifier = DependencyService.Get<IToastNotificator>();
        public static async void Inform(string msg)
        {
            await notifier.Notify(ToastNotificationType.Info, "Info", msg, TimeSpan.FromSeconds(1.5));
        }
    }
}
