using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeñaWeb.Datos;

namespace SeñaWeb.Logica
{
    public class ClTipoSeñaL
    {
        public int MtdContarTiposSena()
        {
            ClTipoSeñaD datosTipoSena = new ClTipoSeñaD();
            return datosTipoSena.MtdContarTiposSena();
        }
    }
}