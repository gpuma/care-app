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
            ////todo: temporal, usamos Major como id
            //ushort targetMajor = 40796;
            //string targetProximity = Proximity.Immediate.ToString();
            ////obtenemos el beacon y proximidad q queremos
            //var targetBeacon = beacons.Where(b => b.Major == targetMajor && b.Proximity.ToString() == targetProximity).FirstOrDefault();
            //if (targetBeacon == null)
            //{
            //    sw.Reset();
            //    return;
            //}
            //sw.Start();
            ////mostramos una notificación
            //await notificator.Notify(
            //        ToastNotificationType.Info,
            //        "SI", String.Format("pasaron {0} ms", sw.ElapsedMilliseconds), TimeSpan.FromSeconds(.5));

            //CRUCE
            var StartBeacon = new Tuple<ushort, string>(53847, Proximity.Immediate.ToString());
            var EndBeacon = new Tuple<ushort, string>(40796, Proximity.Immediate.ToString());

            //comienzo del cruce
            if(CROSS == "NO" && beacons.Contain(StartBeacon))
            {
                sw.Restart();
                CROSS = "BEGAN";
                Toast("COMIENZO CRUCE");
                return;
            }
            if(CROSS == "BEGAN" && beacons.Contain(EndBeacon))
            {
                sw.Stop();
                Toast(String.Format("FIN CRUCE en {0} seg", sw.Elapsed.TotalSeconds));

                //cruce satisfactorio (emergencia
                if(sw.Elapsed.TotalSeconds < 10)
                    Toast("CRUCE DETECTADO");

                //todo: revisar esto
                CROSS = "NO";
            }
        }
        static string CROSS = "NO";

        //comparamos el beacon con nuestra tupla q representa a un beacon
        static bool IsEqual(this IBeacon b, Tuple<ushort,string> bt)
        {
            return (b.Major == bt.Item1 && b.Proximity.ToString() == bt.Item2);
        }

        //para ver si la lista de beacons contiene a la tupla q representa a nuestro beacon
        static bool Contain(this IEnumerable<IBeacon> beacons, Tuple<ushort, string> bt)
        {
            foreach(var b in beacons)
            {
                if (b.IsEqual(bt))
                    return true;
            }
            return false;
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

        static async void Toast(string msg)
        {
            await notificator.Notify(
                    ToastNotificationType.Success,
                    "SI", msg, TimeSpan.FromSeconds(1));
        }
    }
}
