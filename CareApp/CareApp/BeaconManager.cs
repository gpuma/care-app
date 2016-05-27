using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estimotes;
using System.Diagnostics;
using Plugin.Toasts;
using Xamarin.Forms;
using CareApp.Models;

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

        //diccionario de stopwatches
        //el índice es la id de un beacon config
        static Dictionary<string, Stopwatch> timers;

        //corre la primera vez q se usa la clase estática
        static BeaconManager()
        {
            EstimoteManager.Instance.Ranged += Beacons_Ranged;
            //we load the emergency configurations 
            //todo: uncomment
            ConfigManager.LoadEmergencyConfigs();
        }

        private static async void Beacons_Ranged(object sender, IEnumerable<IBeacon> beacons)
        {
            //todo: add enabled check
            foreach(var econfig in ConfigManager.EmergencyConfigs)
            {
                switch ((EmergencyType)econfig.EType)
                {
                    case EmergencyType.ProximityForPeriod:
                        var targetBeacon = new Tuple<ushort, string>(econfig.BeaconId1, econfig.Proximity);
                        if (!beacons.Contain(targetBeacon))
                        {
                            sw.Reset();
                            continue;
                        }
                        sw.Start();
                        if (sw.ElapsedMilliseconds >= econfig.Time)
                        {
                            sw.Stop();
                            //mostramos una notificación
                            await notificator.Notify(
                                    ToastNotificationType.Info,
                                    "SI", String.Format("detectada proximidad de {0} seg.", sw.ElapsedMilliseconds / 1000), TimeSpan.FromSeconds(.1));
                            sw.Reset();
                        }
                        break;

                    case EmergencyType.FastCross:
                        var StartBeacon = new Tuple<ushort, string>(econfig.BeaconId1, econfig.Proximity);
                        var EndBeacon = new Tuple<ushort, string>(econfig.BeaconId2, econfig.Proximity);

                        //comienzo del cruce
                        if (CROSS == "NO" && beacons.Contain(StartBeacon))
                        {
                            sw.Restart();
                            CROSS = "BEGAN";
                            Toast("COMIENZO CRUCE");
                            return;
                        }
                        if (CROSS == "BEGAN" && beacons.Contain(EndBeacon))
                        {
                            sw.Stop();
                            Toast(String.Format("FIN CRUCE en {0} seg", sw.Elapsed.TotalSeconds));

                            //todo: division is returning 0, should be decimal
                            //cruce satisfactorio (emergencia
                            if (sw.Elapsed.TotalSeconds < econfig.Time / 1000)
                                Toast("CRUCE DETECTADO");

                            //todo: revisar esto
                            CROSS = "NO";
                        }
                        break;
                }
            }
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

            ////CRUCE
            //var StartBeacon = new Tuple<ushort, string>(53847, Proximity.Immediate.ToString());
            //var EndBeacon = new Tuple<ushort, string>(40796, Proximity.Immediate.ToString());

            ////comienzo del cruce
            //if(CROSS == "NO" && beacons.Contain(StartBeacon))
            //{
            //    sw.Restart();
            //    CROSS = "BEGAN";
            //    Toast("COMIENZO CRUCE");
            //    return;
            //}
            //if(CROSS == "BEGAN" && beacons.Contain(EndBeacon))
            //{
            //    sw.Stop();
            //    Toast(String.Format("FIN CRUCE en {0} seg", sw.Elapsed.TotalSeconds));

            //    //cruce satisfactorio (emergencia
            //    if(sw.Elapsed.TotalSeconds < 10)
            //        Toast("CRUCE DETECTADO");

            //    //todo: revisar esto
            //    CROSS = "NO";
            //}
        }

        //todo: move this somewhere else
        static string CROSS = "NO";

        //static Tuple<ushort, string> TupleFrom

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
