using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeñaWeb.Entidad
{
    public class ClProgresoE
    {
        public int idProgreso { get; set; }   
        public int idUsuario { get; set; }    
        public int idSeña { get; set; }       
        public bool estado { get; set; }      
    }
}