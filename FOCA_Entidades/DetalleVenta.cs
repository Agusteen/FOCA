using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOCA_Entidades
{
   public class DetalleVenta
    {
        public string descripcionArticulo { get; set; }
       public int idDetalleVenta { get; set; }
       public string nroFactura { get; set; }
       public int articulo { get; set; }
        public int cantidad { get; set; }
        public float precioArticulo {get; set;}
        public float subTotal
        { get { return cantidad * precioArticulo; }
        }
        
    }
}
