using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeñaWeb.Entidad
{
    public class ClProgresoE
    {
        public int idProgreso { get; set; }   // ID del registro de progreso
        public int idUsuario { get; set; }    // ID del usuario
        public int idSeña { get; set; }       // ID de la seña
        public bool estado { get; set; }      // Estado (true = visto, false = no visto)
    }
}