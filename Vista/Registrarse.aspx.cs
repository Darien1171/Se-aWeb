using SeñaWeb.Entidad;
using SeñaWeb.Logica;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeñaWeb.Vista
{
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtConfirmarPassword.Text))
                {
                    MostrarMensaje("Todos los campos son obligatorios. Por favor, complete el formulario.");
                    return;
                }

                
                if (txtPassword.Text != txtConfirmarPassword.Text)
                {
                    MostrarMensaje("Las contraseñas no coinciden. Por favor, inténtelo de nuevo.");
                    return;
                }

                
                if (txtPassword.Text.Length < 6)
                {
                    MostrarMensaje("La contraseña debe tener al menos 6 caracteres.");
                    return;
                }

                
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
                {
                    MostrarMensaje("El formato del correo electrónico no es válido.");
                    return;
                }

                
                ClUsuarioE nuevoUsuario = new ClUsuarioE
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Contraseña = txtPassword.Text,
                    idRol = 2 
                };

                
                ClUsuarioL usuarioLogica = new ClUsuarioL();

                
                int resultado = RegistrarNuevoUsuario(nuevoUsuario);

                if (resultado > 0)
                {
                    
                    Session["userID"] = resultado;

                    
                    string script = "alert('¡Registro exitoso! Serás redirigido al dashboard.'); " +
                                   "window.location.href = 'usuario/DashboardUsuario.aspx';";
                    ScriptManager.RegisterStartupScript(this, GetType(), "redirectScript", script, true);
                }
                else if (resultado == -1)
                {
                    MostrarMensaje("El correo electrónico ya está registrado. Por favor, utilice otro o inicie sesión.");
                }
                else
                {
                    MostrarMensaje("No se pudo completar el registro. Por favor, inténtelo de nuevo más tarde.");
                }
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine("Error en registro: " + ex.Message);
                MostrarMensaje("Ocurrió un error durante el registro. Por favor, inténtelo de nuevo más tarde.");
            }
        }

        private int RegistrarNuevoUsuario(ClUsuarioE usuario)
        {
            
            try
            {
                
                ClUsuarioL usuarioLogica = new ClUsuarioL();
                return usuarioLogica.MtdRegistrarUsuario(usuario);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en RegistrarNuevoUsuario: " + ex.Message);
                throw;
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            string script = $"alert('{mensaje}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", script, true);
        }
    }
}