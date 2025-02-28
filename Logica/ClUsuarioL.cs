using SeñaWeb.Datos;
using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeñaWeb.Logica
{
    public class ClUsuarioL
    {
        public ClUsuarioE login(ClUsuarioE oSesionUsuario)
        {
            ClUsuarioD loginDatos = new ClUsuarioD();
            return loginDatos.loginUsuario(oSesionUsuario);
        }
    }
}