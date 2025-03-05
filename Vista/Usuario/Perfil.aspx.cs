using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeñaWeb.Entidad;
using SeñaWeb.Logica;

namespace SeñaWeb.Vista.Usuario
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si hay una sesión activa
            if (Session["userID"] == null)
            {
                Response.Redirect("~/Vista/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Cargar datos del usuario
                CargarDatosUsuario();

                // Cargar estadísticas
                CargarEstadisticas();
            }
        }

        private void CargarDatosUsuario()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["userID"]);

                // Usar la capa lógica para obtener datos del usuario
                ClUsuarioL logicaUsuario = new ClUsuarioL();
                ClUsuarioE oUsuario = logicaUsuario.MtdObtenerUsuarioPorId(idUsuario);

                if (oUsuario != null)
                {
                    // Llenar los controles con los datos del usuario
                    txtNombre.Text = oUsuario.Nombre;
                    txtApellido.Text = oUsuario.Apellido;
                    txtEmail.Text = oUsuario.Email;

                    // Mostrar en etiquetas
                    lblNombreCompleto.Text = $"{oUsuario.Nombre} {oUsuario.Apellido}";
                    lblEmail.Text = oUsuario.Email;
                    lblIdUsuario.Text = oUsuario.idUsuario.ToString();

                    // Determinar el rol
                    string rol = "Estudiante";
                    if (oUsuario.idRol == 1)
                    {
                        rol = "Administrador";
                    }
                    lblRolUsuario.Text = rol;
                }
                else
                {
                    // Usuario no encontrado, redirigir al login
                    Session.Clear();
                    Response.Redirect("~/Vista/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarDatosUsuario: " + ex.Message);
                MostrarError("No se pudieron cargar los datos del usuario. Por favor, inténtelo más tarde.");
            }
        }

        private void CargarEstadisticas()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["userID"]);

                // Obtener el resumen de progreso usando la capa lógica
                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtResumen = logicaProgreso.MtdObtenerResumenProgreso(idUsuario);

                if (dtResumen.Rows.Count > 0)
                {
                    DataRow row = dtResumen.Rows[0];

                    // Extraer los datos
                    int totalSenas = Convert.ToInt32(row["totalSenas"]);
                    int senasVistas = Convert.ToInt32(row["senasVistas"]);
                    int porcentajeSenas = Convert.ToInt32(row["porcentajeSenas"]);
                    int totalModulos = Convert.ToInt32(row["totalModulos"]);
                    int modulosCompletados = Convert.ToInt32(row["modulosCompletados"]);
                    int porcentajeModulos = Convert.ToInt32(row["porcentajeModulos"]);

                    // Calcular progreso general (promedio de ambos)
                    int progresoGeneral = (porcentajeSenas + porcentajeModulos) / 2;

                    // Actualizar controles
                    lblSenasAprendidas.Text = senasVistas.ToString();
                    lblModulosCompletados.Text = modulosCompletados.ToString();
                    lblProgresoGeneral.Text = progresoGeneral.ToString() + "%";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarEstadisticas: " + ex.Message);
                // No mostrar error al usuario, simplemente no mostrar estadísticas
            }
        }

        protected void btnGuardarInfo_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener ID de usuario de la sesión
                int idUsuario = Convert.ToInt32(Session["userID"]);

                // Crear objeto con los nuevos datos
                ClUsuarioE oUsuario = new ClUsuarioE
                {
                    idUsuario = idUsuario,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                // Usar la capa lógica para actualizar
                ClUsuarioL logicaUsuario = new ClUsuarioL();
                bool resultado = logicaUsuario.MtdActualizarUsuario(oUsuario);

                if (resultado)
                {
                    // Actualizar la información en la interfaz
                    lblNombreCompleto.Text = $"{oUsuario.Nombre} {oUsuario.Apellido}";
                    lblEmail.Text = oUsuario.Email;

                    // Mostrar mensaje de éxito
                    MostrarExito("¡La información se ha actualizado correctamente!");
                }
                else
                {
                    MostrarError("No se pudo actualizar la información. Por favor, inténtelo nuevamente.");
                }
            }
            catch (ArgumentException ex)
            {
                // Errores de validación
                MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en btnGuardarInfo_Click: " + ex.Message);
                MostrarError("Ocurrió un error al actualizar la información. Por favor, inténtelo nuevamente.");
            }
        }

        protected void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener ID de usuario de la sesión
                int idUsuario = Convert.ToInt32(Session["userID"]);

                // Obtener contraseñas de los campos
                string contrasenaActual = txtContrasenaActual.Text;
                string nuevaContrasena = txtNuevaContrasena.Text;
                string confirmarContrasena = txtConfirmarContrasena.Text;

                // Usar la capa lógica para cambiar la contraseña
                ClUsuarioL logicaUsuario = new ClUsuarioL();
                bool resultado = logicaUsuario.MtdCambiarContrasena(idUsuario, contrasenaActual, nuevaContrasena, confirmarContrasena);

                if (resultado)
                {
                    // Limpiar los campos de contraseña
                    txtContrasenaActual.Text = string.Empty;
                    txtNuevaContrasena.Text = string.Empty;
                    txtConfirmarContrasena.Text = string.Empty;

                    // Mostrar mensaje de éxito
                    MostrarExito("¡La contraseña se ha cambiado correctamente!");
                }
                else
                {
                    MostrarError("La contraseña actual es incorrecta. Por favor, verifique e intente nuevamente.");
                }
            }
            catch (ArgumentException ex)
            {
                // Errores de validación
                MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en btnCambiarContrasena_Click: " + ex.Message);
                MostrarError("Ocurrió un error al cambiar la contraseña. Por favor, inténtelo nuevamente.");
            }
        }

        private void MostrarExito(string mensaje)
        {
            alertSuccess.Visible = true;
            alertError.Visible = false;
            successMessage.InnerText = mensaje;
        }

        private void MostrarError(string mensaje)
        {
            alertSuccess.Visible = false;
            alertError.Visible = true;
            errorMessage.InnerText = mensaje;
        }
    }
}