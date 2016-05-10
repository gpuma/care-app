using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareApp.Models
{
    class Beacon
    {
        string Uuid { get; set; }
        short Major { get; set; }
        short Minor { get; set; }
        //Far, Near, Inmediate
        string RelativeDistance { get; set; }
        //iBeacon or Eddystone
        string Type { get; set; }
    }
}
