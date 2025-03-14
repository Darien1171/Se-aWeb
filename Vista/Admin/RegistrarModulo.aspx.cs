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
                
                try
                {
                    CargarModulosRecientes();
                }
                catch (Exception ex)
                {
                    
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

                
                ClModuloE moduloNuevo = new ClModuloE
                {
                    nombreModulo = txtNombreModulo.Text.Trim(),
                    descripcion = txtDescripcion.Text.Trim()
                };

                
                ClModuloL logicaModulo = new ClModuloL();

                
                int resultado = logicaModulo.MtdRegistrarModulo(moduloNuevo);

                if (resultado > 0)
                {
                    
                    MostrarExito("¡El módulo ha sido registrado correctamente! ID: " + resultado);

                    
                    LimpiarFormulario();

                    
                    CargarModulosRecientes();
                }
                else
                {
                    MostrarError("No se pudo registrar el módulo. Por favor, inténtelo nuevamente.");
                }
            }
            catch (Exception ex)
            {
                
                string errorMessage = "Error al registrar el módulo: " + ex.Message;

                if (ex.InnerException != null)
                {
                    errorMessage += " | Detalle: " + ex.InnerException.Message;
                }

                
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