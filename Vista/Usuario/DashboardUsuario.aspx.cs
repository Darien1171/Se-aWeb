using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeñaWeb.Datos;
using SeñaWeb.Entidad;
using SeñaWeb.Logica;

namespace SeñaWeb.Vista.Usuario
{
    public partial class DashboardUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["userID"] == null)
                {
                    Response.Redirect("~/Vista/Login.aspx");
                    return;
                }

  
                int idUsuario = Convert.ToInt32(Session["userID"]);
                CargarDatosUsuario(idUsuario);


                CargarProgresoGeneral(idUsuario);

                CargarModulosRecientes(idUsuario);


                CargarSenasRecientes(idUsuario);


                CargarInfoProgresoAdicional(idUsuario);
            }
        }

        private void CargarDatosUsuario(int idUsuario)
        {
            try
            {

                ClUsuarioL logicaUsuario = new ClUsuarioL();
                ClUsuarioE oUsuario = new ClUsuarioE { idUsuario = idUsuario };


                lblUserName.Text = "Estudiante";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarDatosUsuario: " + ex.Message);

            }
        }

        private void CargarProgresoGeneral(int idUsuario)
        {
            try
            {

                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtResumen = logicaProgreso.MtdObtenerResumenProgreso(idUsuario);

                if (dtResumen.Rows.Count > 0)
                {
                    DataRow row = dtResumen.Rows[0];


                    int totalSenas = Convert.ToInt32(row["totalSenas"]);
                    int senasVistas = Convert.ToInt32(row["senasVistas"]);
                    int porcentajeSenas = Convert.ToInt32(row["porcentajeSenas"]);
                    int totalModulos = Convert.ToInt32(row["totalModulos"]);
                    int modulosCompletados = Convert.ToInt32(row["modulosCompletados"]);
                    int porcentajeModulos = Convert.ToInt32(row["porcentajeModulos"]);


                    int progresoGeneral = (porcentajeSenas + porcentajeModulos) / 2;


                    hdnProgresoGeneral.Value = progresoGeneral.ToString();
                    lblSenasProgress.Text = porcentajeSenas.ToString() + "%";
                    lblModulosProgress.Text = porcentajeModulos.ToString() + "%";
                    lblPuntajeGeneral.Text = progresoGeneral.ToString() + "%";


                    lblSenasDetail.Text = $"{senasVistas} de {totalSenas} señas aprendidas";
                    lblModulosDetail.Text = $"{modulosCompletados} de {totalModulos} módulos completados";
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarProgresoGeneral: " + ex.Message);
            }
        }

        private void CargarInfoProgresoAdicional(int idUsuario)
        {
            try
            {

                DateTime ultimaSesion = DateTime.Now;
                if (ultimaSesion.Date == DateTime.Now.Date)
                {
                    lblUltimaSesion.Text = "Hoy";
                }
                else
                {
                    lblUltimaSesion.Text = ultimaSesion.ToString("dd/MM/yyyy");
                }


                int rachaActual = 1; 
                lblRachaActual.Text = $"{rachaActual} {(rachaActual == 1 ? "día" : "días")}";


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarInfoProgresoAdicional: " + ex.Message);

            }
        }

        public bool ConvertToBoolean(object value)
        {
            if (value == null || value == DBNull.Value)
                return false;

            if (value is bool)
                return (bool)value;

            if (value is int || value is byte || value is short)
                return Convert.ToInt32(value) != 0;

            string strValue = value.ToString().ToLower();
            return strValue == "true" || strValue == "1" || strValue == "yes" || strValue == "sí";
        }

        private void CargarModulosRecientes(int idUsuario)
        {
            try
            {

                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtModulos = logicaProgreso.MtdObtenerModulosConProgreso(idUsuario);


                if (dtModulos.Rows.Count > 5)
                {
                    DataTable dtLimitado = dtModulos.Clone();
                    for (int i = 0; i < 5; i++)
                    {
                        dtLimitado.ImportRow(dtModulos.Rows[i]);
                    }
                    dtModulos = dtLimitado;
                }

                lvModulosRecientes.DataSource = dtModulos;
                lvModulosRecientes.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarModulosRecientes: " + ex.Message);
            }
        }

        private void CargarSenasRecientes(int idUsuario)
        {
            try
            {

                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtProgreso = logicaProgreso.MtdObtenerProgresoUsuario(idUsuario);


                if (dtProgreso.Rows.Count > 5)
                {
                    DataTable dtLimitado = dtProgreso.Clone();
                    for (int i = 0; i < 5; i++)
                    {
                        dtLimitado.ImportRow(dtProgreso.Rows[i]);
                    }
                    dtProgreso = dtLimitado;
                }

                lvSenasRecientes.DataSource = dtProgreso;
                lvSenasRecientes.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarSenasRecientes: " + ex.Message);
            }
        }
    }
}