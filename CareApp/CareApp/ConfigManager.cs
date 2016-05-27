using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using CareApp.Models;

namespace CareApp
{
    public static class ConfigManager
    {
        //set of emergencies to be detected, tied to specific beacons
        public static List<EmergencyConfig> EmergencyConfigs { get; set; }

        public static void LoadEmergencyConfigs()
        {
            var ass = typeof(ConfigManager).GetTypeInfo().Assembly;
            using (var stream = ass.GetManifestResourceStream("CareApp.Config.emergency-configs.xml"))
            using (StreamReader reader = new StreamReader(stream))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(List<EmergencyConfig>));
                    EmergencyConfigs = (List<EmergencyConfig>)serializer.Deserialize(reader);
                }
                catch (System.Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    throw;
                }
            }
        }
    }
}
