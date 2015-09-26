using System;

namespace FOCA_Entidades
{
    public class Cliente
    {
        public int? indexBD { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string domicilio { get; set; }
        public int dni { get; set; }
        public int telefono { get; set; }
        private Boolean _preferencial;
        public int preferencial
        {
            get
            {
                if (_preferencial == true) return 1;
                else
                    return 0;
            }
            set
            {
                if (value == 1) _preferencial = true;
                else _preferencial = false;
            }
        }
        public DateTime fechaNac { get; set; }
        public int localidad { get; set; }
    }
}
