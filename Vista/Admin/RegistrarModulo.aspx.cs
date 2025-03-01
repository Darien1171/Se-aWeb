using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeñaWeb.Entidad;
using SeñaWeb.Logica;

namespace SeñaWeb.Vista.Admin
{
    public partial class RegistrarModulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar datos iniciales
                try
                {
                    CargarModulosRecientes();
                }
                catch (Exception ex)
                {
                    // Solo logueamos el error pero no mostramos nada al usuario en la carga inicial
                    System.Diagnostics.Debug.WriteLine("Error al cargar módulos: " + ex.Message);
                }
            }
        }

        private void CargarModulosRecientes()
        {
            ClModuloL logicaModulo = new ClModuloL();
            DataTable dtModulos = logicaModulo.MtdListarModulosRecientes(5);

            if (dtModulos != null && dtModulos.Rows.Count > 0)
            {
                gvModulosRecientes.DataSource = dtModulos;
                gvModulosRecientes.DataBind();
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación manual
                if (string.IsNullOrWhiteSpace(txtNombreModulo.Text))
                {
                    MostrarError("El nombre del módulo es obligatorio.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MostrarError("La descripción del módulo es obligatoria.");
                    return;
                }

                // Crear objeto de entidad con los datos del formulario
                ClModuloE moduloNuevo = new ClModuloE
                {
                    nombreModulo = txtNombreModulo.Text.Trim(),
                    descripcion = txtDescripcion.Text.Trim()
                };

                // Instanciar la capa de lógica
                ClModuloL logicaModulo = new ClModuloL();

                // Intentar guardar el módulo
                int resultado = logicaModulo.MtdRegistrarModulo(moduloNuevo);

                if (resultado > 0)
                {
                    // Mostrar mensaje de éxito
                    MostrarExito("¡El módulo ha sido registrado correctamente! ID: " + resultado);

                    // Limpiar los campos del formulario
                    LimpiarFormulario();

                    // Recargar la lista de módulos recientes
                    CargarModulosRecientes();
                }
                else
                {
                    MostrarError("No se pudo registrar el módulo. Por favor, inténtelo nuevamente.");
                }
            }
            catch (Exception ex)
            {
                // Mostrar detalles del error para ayudar a depurar
                string errorMessage = "Error al registrar el módulo: " + ex.Message;

                if (ex.InnerException != null)
                {
                    errorMessage += " | Detalle: " + ex.InnerException.Message;
                }

                // Registrar el error completo incluyendo el stack trace
                System.Diagnostics.Debug.WriteLine(errorMessage);
                System.Diagnostics.Debug.WriteLine("Stack Trace: " + ex.StackTrace);

                MostrarError(errorMessage);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombreModulo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
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