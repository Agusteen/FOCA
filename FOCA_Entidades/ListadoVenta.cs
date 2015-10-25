using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOCA_Entidades
{
    public class ListadoVenta
    {
        int indexCliente { get; set; }
        String nombreCliente { get; set; }
        Boolean preferencial { get; set; }
        int intPreferencial
        {
            get
            {
                if (preferencial == true) return 1;
                else
                    return 0;
            }
            set
            {
                if (value == 1) preferencial = true;
                else preferencial = false;
            }
        }
        String stringPreferencial
        {
            get
            {
                if (preferencial) return "Si";
                else return "No";
            }
        }
        DateTime fecha { get; set; }
        Decimal monto { get; set; }

    }
}
