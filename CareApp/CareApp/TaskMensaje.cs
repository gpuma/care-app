using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CareApp
{
    //todo: mover de aquí
    public class TaskMensaje
    {
        //nuestro mensaje q corre en el background supuestamente
        public async Task MostrarMensajePendejo(CancellationToken token)
        {
            await Task.Run(async () =>
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();

                    await Task.Delay(3000);

                    var message = new TickedMessage { Message = "hola " + i.ToString() };
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
                    });
                }
            }, token);
        }
    }
}
