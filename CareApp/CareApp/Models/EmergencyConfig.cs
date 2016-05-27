using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareApp.Models
{
    public class EmergencyConfig
    {
        public string Id { get; set; }

        //public string EType { get; set; }
        //el tipo fue revertido a int en vez de enum
        //xq no funciona muy bien en la serializacion
        int eType;
        public int EType
        {
            get
            {
                return eType;
            }
            set
            {
                this.eType = (int)value;
            }
        }
        public ushort BeaconId1 { get; set; }
        //0 es igual a null
        public ushort BeaconId2 { get; set; }
        public string Proximity { get; set; }
        //public int Proximity { get; set; }
        public double Time { get; set; }
    }

    //todo: load this from db or hardcode it?
    public enum EmergencyType
    {
        ProximityForPeriod,
        ProximityForPeriodAtTime,
        FastCross,
        IncompleteCross
    }
}
