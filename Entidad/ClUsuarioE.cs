using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeñaWeb.Entidad
{
    public class ClUsuarioE
    {
        public int idUsuario {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public int idRol {  get; set; }
    }
}