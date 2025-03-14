using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeñaWeb.Entidad
{
    public class ClTipoSeñaE
    {
        public int idTiposeña { get; set; } 
        public string tipo { get; set; }     
        public string descripcion { get; set; } 
        public int idModulo { get; set; }    
    }
}