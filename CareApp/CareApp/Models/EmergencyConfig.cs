using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareApp.Models
{
    public class EmergencyConfig
    {
        public int Id { get; set; }
        public int EType { get; set; }
        public ushort BeaconId1 { get; set; }
        //0 es igual a null
        public ushort BeaconId2 { get; set; }
        public string Proximity { get; set; }
        public double Time { get; set; }
    }
}
