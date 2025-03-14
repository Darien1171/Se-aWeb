using SeñaWeb.Entidad;
using SeñaWeb.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeñaWeb.Vista.Admin
{
    public partial class RegistrarTipoSeña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                try
                {
                    CargarModulos();
                    CargarTiposSenaRecientes();
                }
                catch (Exception ex)
                {
                    
                    System.Diagnostics.Debug.WriteLine("Error al cargar datos iniciales: " + ex.Message);
                }
            }
        }

        private void CargarModulos()
        {
            ClTipoSeñaL logicaTipoSena = new ClTipoSeñaL();
            DataTable dtModulos = logicaTipoSena.MtdObtenerModulos();

            if (dtModulos != null && dtModulos.Rows.Count > 0)
            {
                ddlModulo.DataSource = dtModulos;
                ddlModulo.DataTextField = "nombreModulo";
                ddlModulo.DataValueField = "idModulo";
                ddlModulo.DataBind();

                
                ddlModulo.Items.Insert(0, new ListItem("-- Seleccione un módulo --", "0"));
            }
            else
            {
                
                ddlModulo.Items.Clear();
                ddlModulo.Items.Add(new ListItem("No hay módulos disponibles", "0"));
                btnRegistrar.Enabled = false;
                MostrarError("No existen módulos registrados. Debe crear al menos un módulo antes de registrar tipos de seña.");
            }
        }

        private void CargarTiposSenaRecientes()
        {
            ClTipoSeñaL logicaTipoSena = new ClTipoSeñaL();
            DataTable dtTiposSena = logicaTipoSena.MtdListarTiposSenaRecientes(5);

            if (dtTiposSena != null && dtTiposSena.Rows.Count > 0)
            {
                gvTiposSenaRecientes.DataSource = dtTiposSena;
                gvTiposSenaRecientes.DataBind();
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (ddlModulo.SelectedValue == "0")
                {
                    MostrarError("Debe seleccionar un módulo.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTipo.Text))
                {
                    MostrarError("El nombre del tipo de seña es obligatorio.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MostrarError("La descripción del tipo de seña es obligatoria.");
                    return;
                }

                
                ClTipoSeñaE tipoSenaNuevo = new ClTipoSeñaE
                {
                    tipo = txtTipo.Text.Trim(),
                    descripcion = txtDescripcion.Text.Trim(),
                    idModulo = Convert.ToInt32(ddlModulo.SelectedValue)
                };

                
                ClTipoSeñaL logicaTipoSena = new ClTipoSeñaL();

                
                int resultado = logicaTipoSena.MtdRegistrarTipoSena(tipoSenaNuevo);

                if (resultado > 0)
                {
                    
                    MostrarExito($"¡El tipo de seña '{txtTipo.Text.Trim()}' ha sido registrado correctamente! ID: {resultado}");

                    
                    LimpiarFormulario();

                    
                    CargarTiposSenaRecientes();
                }
                else
                {
                    MostrarError("No se pudo registrar el tipo de seña. Por favor, inténtelo nuevamente.");
                }
            }
            catch (Exception ex)
            {
                
                string errorMessage = "Error al registrar el tipo de seña: " + ex.Message;

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
            ddlModulo.SelectedValue = "0"; 
            txtTipo.Text = string.Empty;
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