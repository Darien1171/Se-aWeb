using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeñaWeb.Datos;

namespace SeñaWeb.Logica
{
    public class ClModuloL
    {
        public int MtdContarModulos()
        {
            ClModuloD datosModulo = new ClModuloD();
            return datosModulo.MtdContarModulos();
        }
    }
}