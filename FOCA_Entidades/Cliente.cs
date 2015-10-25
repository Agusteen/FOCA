using System;

namespace FOCA_Entidades
{
    public class Cliente
    {
        public int? indexBD { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreyapellido {
            get
            {
                return nombre + " " + apellido;
            }
            set
            {
                //falta.
            }
           
                }
        public string mail { get; set; }
        public string password { get; set; }
        public int rol { get; set; }
        public string rolString { get; set; }
        public Boolean preferencial { get; set; }
        public String stringPreferencial
        {
            get
            {
                if (preferencial == true) return "Si";
                else
                    return "No";
            }

        }
        public int intPreferencial
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
        public string fechaNac { get; set; }
        public int localidad { get; set; }
        
    }
}
