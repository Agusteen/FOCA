using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOCA_Entidades
{
    class Articulo
    {
        public int? indexBD { get; set; }
        public string descripcion { get; set; }
        public float precio { get; set; }
        public int stock { get; set; }
        private Boolean _disponible;
        public int disponible
        {
            get
            {
                if (_disponible == true) return 1;
                else
                    return 0;
            }
            set
            {
                if (value == 1) _disponible = true;
                else _disponible = false;
            }
        }
        public int tipoArticulo { get; set; }

    }
}
