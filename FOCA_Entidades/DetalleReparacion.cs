using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOCA_Entidades
{
    public class DetalleReparacion
    {
        public int? idDetalleReparacion { get; set; }
        public int idReparacion { get; set; }
        public int problema { get; set; }
        public float subTotal { get; set; }
    }
}
