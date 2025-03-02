using SeñaWeb.Entidad;
using SeñaWeb.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeñaWeb.Vista
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ClUsuarioE oSesionUsuario = new ClUsuarioE()
            {
                Email = txtEmail.Text,
                Contraseña = txtPassword.Text
            };

            ClUsuarioL logicaLogin = new ClUsuarioL();
            oSesionUsuario = logicaLogin.login(oSesionUsuario);

            if (oSesionUsuario == null || oSesionUsuario.idRol == 0)
            {
                Response.Write("<script>alert('Error: Usuario no encontrado o credenciales inválidas');</script>");
                return;
            }


            Session["userID"] = oSesionUsuario.idUsuario;

            switch (oSesionUsuario.idRol)
            {
                case 1:
                    Response.Redirect("~/Vista/admin/DashboardAdmin.aspx");
                    break;
                case 2:
                    Response.Redirect("~/Vista/usuario/DashboardUsuario.aspx");
                    break;
                default:
                    Response.Write("<script>alert('Rol no reconocido');</script>");
                    break;
            }
        }



    }
}