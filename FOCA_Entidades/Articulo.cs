using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOCA_Entidades
{
   public class Articulo
    {
        public int? indexBD { get; set; }
     
        public string descripcion { get; set; }
        public float precio{ get; set; }
        public int stock { get; set; }
        public Boolean disponible { get; set; }
        public int intDisponible
        {
            get
            {
                if (disponible == true) return 1;
                else
                    return 0;
            }
            set
            {
                if (value == 1) disponible = true;
                else disponible = false;
            }
        }
        public int tipoArticulo { get; set; }

    }
}
