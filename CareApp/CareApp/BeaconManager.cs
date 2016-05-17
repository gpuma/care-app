using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estimotes;
using System.Diagnostics;
using Plugin.Toasts;
using Xamarin.Forms;

namespace CareApp
{
    public static class BeaconManager
    {
        static BeaconRegion defaultRegion = new BeaconRegion("test-region", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
        //todo: check this shit
        static BeaconInitStatus status = BeaconInitStatus.Unknown;

        //representación de un beacon
        static Tuple<ushort, string> previous;// = new Tuple<ushort, string> (0000,"null");

        //todo: meter esto a una clase estática
        static IToastNotificator notificator = DependencyService.Get<IToastNotificator>();

        //cronometro
        static Stopwatch sw = new Stopwatch();

        //corre la primera vez q se usa la clase estática
        static BeaconManager()
        {
            EstimoteManager.Instance.Ranged += Beacons_Ranged;
        }

        private static async void Beacons_Ranged(object sender, IEnumerable<IBeacon> beacons)
        {
            //todo: temporal, usamos Major como id
            ushort targetMajor = 40796;
            string targetProximity = Proximity.Immediate.ToString();
            //obtenemos el beacon y proximidad q queremos
            var targetBeacon = beacons.Where(b => b.Major == targetMajor && b.Proximity.ToString() == targetProximity).FirstOrDefault();
            if (targetBeacon == null)
            {
                sw.Reset();
                //todo: no estoy seguro
                previous = null;
                return;
            }
            //primera vez escaneado
            if (previous == null)
            {
                previous = new Tuple<ushort, string>(targetBeacon.Major,
                    targetBeacon.Proximity.ToString());
                sw.Start();
            }
            else if (targetBeacon.IsEqual(previous))
            {
                //mostramos una notificación
                await notificator.Notify(
                        ToastNotificationType.Info,
                        "SI", String.Format("pasaron {0} ms", sw.ElapsedMilliseconds), TimeSpan.FromSeconds(.5));
            }
            //no se detecto el mismo beacon (o a la misma proximidad)
            else
            {
                sw.Reset();
            }

            //TODO: PREVIOUS IS USELESS BECAUSE THERE CAN BE MANY BEACONS
        }

        //comparamos el beacon con nuestra tupla q representa a un beacon
        static bool IsEqual(this IBeacon b, Tuple<ushort,string> bt)
        {
            return (b.Major == bt.Item1 && b.Proximity.ToString() == bt.Item2);
        }

        public static async void Start()
        {
            status = await EstimoteManager.Instance.Initialize();
            if (status != BeaconInitStatus.Success)
            {
                throw new Exception("unable to start beacon manager");
            }
            EstimoteManager.Instance.StartRanging(defaultRegion);
        }
        public static void Stop()
        {
            EstimoteManager.Instance.StopRanging(defaultRegion);
        }
    }
}
