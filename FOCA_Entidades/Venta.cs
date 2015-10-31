using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOCA_Entidades
{
    public class Venta
    {
        public int nroFactura { get; set; }
        public DateTime fecha { get; set; }
        public int cliente { get; set; }
        public float monto { get; set; }
    }
}
