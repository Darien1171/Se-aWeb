using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeñaWeb.Entidad
{
    public class ClTipoSeñaE
    {
        public int idTiposeña { get; set; } // ID del tipo de seña
        public string tipo { get; set; }     // Nombre del tipo de seña
        public string descripcion { get; set; } // Descripción del tipo de seña
        public int idModulo { get; set; }    // ID del módulo al que pertenece
    }
}