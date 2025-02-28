using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeñaWeb.Datos;

namespace SeñaWeb.Logica
{
    public class ClSeñaL
    {
        public int MtdContarSenas()
        {
            ClSeñaD datosSena = new ClSeñaD();
            return datosSena.MtdContarSenas();
        }
    }
}