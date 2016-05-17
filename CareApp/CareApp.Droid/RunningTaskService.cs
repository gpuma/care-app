using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareApp.Droid
{
    [Service]
    public class RunningTaskService : Service
    {
        CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            //why?
            return null;
        }

        //[return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, /*[GeneratedEnum] */StartCommandFlags flags, int startId)
        {
            //return base.OnStartCommand(intent, flags, startId);
            _cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                try
                {
                    //invocamos el código compartido!!!!
                    var mensajero = new TaskMensaje();
                    mensajero.MostrarMensajePendejo(_cts.Token).Wait();
                }
                catch (Android.OS.OperationCanceledException)
                {
                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        var msg = new CancelledMessage();
                        Device.BeginInvokeOnMainThread(
                            () => MessagingCenter.Send(msg, "CancelledMessage")
                        );
                    }
                }
            }, _cts.Token);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            if(_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();
                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}