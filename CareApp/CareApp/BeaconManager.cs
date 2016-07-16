using CareApp.Models;
using Estimotes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace CareApp
{
    public static class BeaconManager
    {
        static BeaconRegion defaultRegion = new BeaconRegion("test-region", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
        static BeaconInitStatus status = BeaconInitStatus.Unknown;

        //diccionario de stopwatches
        //el índice es la id de un beacon config
        static Dictionary<int, Stopwatch> timers;

        static string CROSS = "NO";
        public static bool EnableRanging { get; set; }

        static Usuario User { get; set; }
        static ContentPage Parent { get; set; }

        //static Data.RESTService rest = new Data.RESTService();

        //corre la primera vez q se usa la clase estática
        static BeaconManager()
        {
            EnableRanging = false;
            EstimoteManager.Instance.Ranged += Beacons_Ranged;
        }

        //debe ser llamado para que funcione
        //recién con esto se puede acceder a las configs
        //con el "padre" podemos hacer Push de otros formularios
        public static void SetRequirements(Usuario usr, ContentPage parent)
        {
            EnableRanging = false;
            User = usr;
            Parent = parent;
            EnableRanging = true;
            //initialize stopwatches for each config
            InitializeTimers();
        }

        private static void InitializeTimers()
        {
            //we limit it to the size of our configs
            //might need to change this when we can add or delete configs
            timers = new Dictionary<int, Stopwatch>(User.Configuraciones.Count);
            foreach (var econfig in User.Configuraciones)
                timers[econfig.Id] = new Stopwatch();
        }

        private static async void Beacons_Ranged(object sender, IEnumerable<IBeacon> beacons)
        {
            if (!EnableRanging)
                return;
            //temporales
            Tuple<ushort, int> StartBeacon;
            Tuple<ushort, int> EndBeacon;
            Tuple<ushort, int> TargetBeacon;
            //todo: add enabled check (FIRST NEED TO ADD ENABLED FIElD)
            foreach (var econfig in User.Configuraciones)
            {
                var currentTimer = timers[econfig.Id];
                switch ((EmergencyType)econfig.Tipo)
                {
                    case EmergencyType.ProximityForPeriod:
                        TargetBeacon = new Tuple<ushort, int>(econfig.BeaconId1, econfig.Rango);
                        if (!beacons.Contain(TargetBeacon))
                        {
                            currentTimer.Reset();
                            continue;
                        }
                        currentTimer.Start();
                        if (currentTimer.ElapsedMilliseconds >= econfig.Tiempo)
                        {
                            //just in case
                            EnableRanging = false;

                            currentTimer.Stop();
                            //mostramos una notificación
                            //todo: originalmente mostrada solo x .1 seg
                            Notifier.Inform(String.Format("detectada proximidad de {0} seg.", currentTimer.ElapsedMilliseconds / 1000.0));
                            currentTimer.Reset();
                            await Parent.Navigation.PushAsync(new Views.PatientAlertView(User, econfig));
                            EnableRanging = true;
                        }
                        break;
                    case EmergencyType.ProximityForPeriodAtTime:
                        TargetBeacon = new Tuple<ushort, int>(econfig.BeaconId1, econfig.Rango);
                        if (!beacons.Contain(TargetBeacon))
                        {
                            currentTimer.Reset();
                            continue;
                        }
                        currentTimer.Start();
                        if (currentTimer.ElapsedMilliseconds >= econfig.Tiempo &&
                             DateTime.Now.Hour >= econfig.Hora.Hour)
                        {
                            //just in case
                            EnableRanging = false;

                            currentTimer.Stop();
                            //mostramos una notificación
                            //todo: originalmente mostrada solo x .1 seg
                            Notifier.Inform(String.Format("detectada proximidad de {0} seg.", currentTimer.ElapsedMilliseconds / 1000.0));
                            currentTimer.Reset();
                            await Parent.Navigation.PushAsync(new Views.PatientAlertView(User, econfig));
                            EnableRanging = true;
                        }
                        break;
                    case EmergencyType.FastCross:
                        StartBeacon = new Tuple<ushort, int>(econfig.BeaconId1, econfig.Rango);
                        EndBeacon = new Tuple<ushort, int>(econfig.BeaconId2, econfig.Rango);

                        //comienzo del cruce
                        if (CROSS == "NO" && beacons.Contain(StartBeacon))
                        {
                            currentTimer.Restart();
                            CROSS = "BEGAN";
                            Notifier.Inform("COMIENZO CRUCE");
                            return;
                        }
                        if (CROSS == "BEGAN" && beacons.Contain(EndBeacon))
                        {
                            //cruce satisfactorio (emergencia
                            if (currentTimer.Elapsed.TotalSeconds > econfig.Tiempo / 1000.0)
                            {
                                //se pasó del tiempo límite para completar el cruce
                                CROSS = "NO";
                                continue;
                            }
                            currentTimer.Stop();
                            Notifier.Inform("CRUCE DETECTADO");
                            CROSS = "NO";
                            await Parent.Navigation.PushAsync(new Views.PatientAlertView(User, econfig));
                            EnableRanging = true;
                        }
                        break;

                    case EmergencyType.IncompleteCross:
                        StartBeacon = new Tuple<ushort, int>(econfig.BeaconId1, econfig.Rango);
                        EndBeacon = new Tuple<ushort, int>(econfig.BeaconId2, econfig.Rango);

                        //comienzo del cruce
                        if (CROSS == "NO" && beacons.Contain(StartBeacon))
                        {
                            currentTimer.Restart();
                            CROSS = "BEGAN";
                            Notifier.Inform("COMIENZO CRUCE");
                            return;
                        }
                        if (CROSS == "BEGAN" && !beacons.Contain(EndBeacon))
                        {
                            if (currentTimer.Elapsed.TotalSeconds <= econfig.Tiempo / 1000.0)
                                continue;
                            currentTimer.Stop();
                            Notifier.Inform(String.Format("CRUCE INCOMPLETO en {0} seg", currentTimer.Elapsed.TotalSeconds));
                            CROSS = "NO";
                            await Parent.Navigation.PushAsync(new Views.PatientAlertView(User, econfig));
                            EnableRanging = true;
                        }
                        break;
                }
            }
        }

        //primero avisa al usuario para ver si fue un falso positivo
        //si no es el caso recién se guarda la alarma a la BD
        //private static async void AlertPatient(EmergencyConfig econfig)
        //{
        //    var alertView = new Views.PatientAlertView(User);
        //    await Parent.Navigation.PushAsync(alertView);
        //}

        //static Tuple<ushort, int> TupleFrom

        //comparamos el beacon con nuestra tupla q representa a un beacon
        static bool IsEqual(this IBeacon b, Tuple<ushort,int> bt)
        {
            //<= xq si queremos que detecta cerca, también incluye inmediato
            return (b.Major == bt.Item1 && (int)b.Proximity <= bt.Item2);
        }

        //para ver si la lista de beacons contiene a la tupla q representa a nuestro beacon
        static bool Contain(this IEnumerable<IBeacon> beacons, Tuple<ushort, int> bt)
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

        //static async void Toast(string msg)
        //{
        //    await notificator.Notify(
        //            ToastNotificationType.Success,
        //            "SI", msg, TimeSpan.FromSeconds(1));
        //}
    }
}
