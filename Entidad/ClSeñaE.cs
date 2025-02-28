using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeñaWeb.Entidad
{
    public class ClSeñaE
    {
        public int idSeña { get; set; }      // ID de la seña
        public string nombreSeña { get; set; } // Nombre de la seña
        public string urlVideo { get; set; }  // URL del video de la seña
        public int idTipoSeña { get; set; }   // ID del tipo de seña al que pertenece
    }
}