namespace CareApp.Models
{
    public class Beacon
    {
        public string Uuid { get; set; }
        public ushort Major { get; set; }
        public ushort Minor { get; set; }
        //Far, Near, Inmediate
        public string RelativeDistance { get; set; }
        //iBeacon or Eddystone
        public string Type { get; set; }
    }
}
