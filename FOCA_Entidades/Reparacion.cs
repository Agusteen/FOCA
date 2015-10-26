using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOCA_Entidades
{
    public class Reparacion
    {
        public int? idReparacion { get; set; }
        public string fechaReparacion { get; set; }
        public string fechaDevolucion { get; set; }
        public string descripcionReparacion { get; set; }
        public string equipo { get; set; }
        public int estado { get; set; }
        public int cliente { get; set; }
        public float total { get; set; }
    }
}
