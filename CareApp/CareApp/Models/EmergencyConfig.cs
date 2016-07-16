using Newtonsoft.Json;
using System;

namespace CareApp.Models
{
    public class EmergencyConfig
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        //public string EType { get; set; }
        //el tipo fue revertido a int en vez de enum
        //xq no funciona muy bien en la serializacion
        [JsonIgnore]
        int eType;

        public int Tipo
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
        public int Rango { get; set; }
        //public int Proximity { get; set; }
        public int Tiempo { get; set; }
        public string Paciente { get; set; }
        public DateTime Hora { get; set; }
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
