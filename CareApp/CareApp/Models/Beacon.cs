using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareApp.Models
{
    public class Beacon
    {
        public string Uuid { get; set; }
        public short Major { get; set; }
        public short Minor { get; set; }
        //Far, Near, Inmediate
        public string RelativeDistance { get; set; }
        //iBeacon or Eddystone
        public string Type { get; set; }
    }
}
