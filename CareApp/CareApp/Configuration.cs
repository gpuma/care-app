﻿using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using CareApp.Models;

namespace CareApp
{
    public static class Configuration
    {
        //set of emergencies to be detected, tied to specific beacons
        public static IEnumerable<EmergencyConfig> EmergencyConfigs { get; set; }

        public static void LoadEmergencyConfigs()
        {
            var ass = typeof(Configuration).GetTypeInfo().Assembly;
            using (var stream = ass.GetManifestResourceStream("CareApp.Config.emergency-configs.xml"))
            using (StreamReader reader = new StreamReader(stream))
            {
                var serializer = new XmlSerializer(typeof(IEnumerable<EmergencyConfig>));
                EmergencyConfigs = (IEnumerable<EmergencyConfig>)serializer.Deserialize(reader);
            }
        }
    }
}