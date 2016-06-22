using System;

namespace CareApp.Models
{
    public class Emergencia
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Estado { get; set; }
        public string Paciente { get; set; }
        public string Cuidante { get; set; }
    }
}
