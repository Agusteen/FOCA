﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOCA_Entidades
{
    public class ListadoReparacion
    {
        public int idReparacion { get; set; }
        public int indexCliente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public String nombreCliente { get { return nombre + " " + apellido; } }
        public Boolean preferencial { get; set; }
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
        public String stringPreferencial
        {
            get
            {
                if (preferencial) return "Si";
                else return "No";
            }
        }
        public string estado { get; set; }
        public DateTime fechareparacion { get; set; }
        public DateTime fechadevolucion { get; set; }
      
        

    }
}

