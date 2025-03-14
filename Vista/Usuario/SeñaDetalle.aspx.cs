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
    public partial class SeñaDetalle : System.Web.UI.Page
    {
        
        private int idSena = 0;

        
        private int idModulo = 0;

        
        private bool estaVisto = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["userID"] == null)
            {
                Response.Redirect("~/Vista/Login.aspx");
                return;
            }

            
            if (Request.QueryString["id"] != null)
            {
                if (int.TryParse(Request.QueryString["id"], out idSena))
                {
                    if (!IsPostBack)
                    {
                        
                        CargarDetalleSena();

                        
                        CargarSenasRelacionadas();

                        
                        ActualizarProgreso();
                    }
                }
                else
                {
                    
                    Response.Redirect("~/Vista/Usuario/BibliotecaSeñas.aspx");
                }
            }
            else
            {
                
                Response.Redirect("~/Vista/Usuario/BibliotecaSeñas.aspx");
            }
        }

        private void CargarDetalleSena()
        {
            try
            {
                
                int idUsuario = Convert.ToInt32(Session["userID"]);

                
                ClSeñaL logicaSena = new ClSeñaL();

                
                DataTable dtSena = logicaSena.MtdObtenerDetalleSena(idSena, idUsuario);

                if (dtSena.Rows.Count > 0)
                {
                    DataRow row = dtSena.Rows[0];

                    
                    lblNombreSena.InnerText = row["nombreSeña"].ToString();
                    lblTipoSena.InnerText = row["tipoSeña"].ToString();

                    
                    string urlVideo = row["urlVideo"].ToString();
                    videoPlayer.Src = ResolveUrl(urlVideo);

                    
                    if (row["descripcionTipo"] != DBNull.Value)
                    {
                        lblDescripcion.InnerText = row["descripcionTipo"].ToString();
                    }
                    else
                    {
                        lblDescripcion.InnerText = "No hay descripción disponible para esta seña.";
                    }

                    
                    idModulo = Convert.ToInt32(row["idModulo"]);

                    
                    lnkModulo.Text = row["nombreModulo"].ToString();
                    lnkModulo.NavigateUrl = $"~/Vista/Usuario/BibliotecaSeñas.aspx?modulo={idModulo}";

                    
                    estaVisto = row["estado"] != DBNull.Value && ConvertToBoolean(row["estado"]);

                    
                    ActualizarBotonesEstado();
                }
                else
                {
                    
                    Response.Redirect("~/Vista/Usuario/BibliotecaSeñas.aspx");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarDetalleSena: " + ex.Message);
                
            }
        }

        private void CargarSenasRelacionadas()
        {
            try
            {
                if (idModulo <= 0)
                {
                    return; 
                }

                int idUsuario = Convert.ToInt32(Session["userID"]);

                
                ClProgresoL logicaProgreso = new ClProgresoL();

                
                DataTable dtSenasRelacionadas = logicaProgreso.MtdObtenerProgresoModulo(idUsuario, idModulo);

                
                dtSenasRelacionadas.DefaultView.RowFilter = $"idSeña <> {idSena}";
                dtSenasRelacionadas = dtSenasRelacionadas.DefaultView.ToTable();

                
                if (dtSenasRelacionadas.Rows.Count > 5)
                {
                    DataTable dtLimitado = dtSenasRelacionadas.Clone();
                    for (int i = 0; i < 5; i++)
                    {
                        dtLimitado.ImportRow(dtSenasRelacionadas.Rows[i]);
                    }
                    dtSenasRelacionadas = dtLimitado;
                }

                
                lvSenasRelacionadas.DataSource = dtSenasRelacionadas;
                lvSenasRelacionadas.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarSenasRelacionadas: " + ex.Message);
            }
        }

        private void ActualizarProgreso()
        {
            try
            {
                if (idModulo <= 0)
                {
                    return; 
                }

                int idUsuario = Convert.ToInt32(Session["userID"]);

                
                ClProgresoL logicaProgreso = new ClProgresoL();

                
                DataTable dtSenasModulo = logicaProgreso.MtdObtenerProgresoModulo(idUsuario, idModulo);

                
                int totalSenas = dtSenasModulo.Rows.Count;
                int senasVistas = 0;

                foreach (DataRow row in dtSenasModulo.Rows)
                {
                    if (row["estado"] != DBNull.Value && ConvertToBoolean(row["estado"]))
                    {
                        senasVistas++;
                    }
                }

                
                int porcentaje = (totalSenas > 0) ? (senasVistas * 100) / totalSenas : 0;

                
                lblPorcentajeProgreso.InnerText = porcentaje.ToString() + "%";
                progressBar.Style["width"] = porcentaje.ToString() + "%";
                progressBar.Attributes["aria-valuenow"] = porcentaje.ToString();
                lblProgresoDetalle.InnerText = $"Has visto {senasVistas} de {totalSenas} señas";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en ActualizarProgreso: " + ex.Message);
            }
        }

        private void ActualizarBotonesEstado()
        {
            btnMarcarVisto.Visible = !estaVisto;
            btnMarcarPendiente.Visible = estaVisto;
        }

        protected void btnMarcarVisto_Click(object sender, EventArgs e)
        {
            try
            {
                
                ActualizarEstadoSena(true);

                
                MostrarMensajeExito("¡Seña marcada como vista correctamente!");

                
                estaVisto = true;
                ActualizarBotonesEstado();
                ActualizarProgreso();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en btnMarcarVisto_Click: " + ex.Message);
            }
        }

        protected void btnMarcarPendiente_Click(object sender, EventArgs e)
        {
            try
            {
                
                ActualizarEstadoSena(false);

                
                MostrarMensajeExito("Seña marcada como pendiente.");

                
                estaVisto = false;
                ActualizarBotonesEstado();
                ActualizarProgreso();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en btnMarcarPendiente_Click: " + ex.Message);
            }
        }

        private void ActualizarEstadoSena(bool estado)
        {
            int idUsuario = Convert.ToInt32(Session["userID"]);

            
            ClProgresoE oProgreso = new ClProgresoE
            {
                idUsuario = idUsuario,
                idSeña = idSena,
                estado = estado
            };

            
            ClProgresoL logicaProgreso = new ClProgresoL();
            logicaProgreso.MtdRegistrarProgreso(oProgreso);
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            
            if (idModulo > 0)
            {
                Response.Redirect($"~/Vista/Usuario/BibliotecaSeñas.aspx?modulo={idModulo}");
            }
            else
            {
                Response.Redirect("~/Vista/Usuario/BibliotecaSeñas.aspx");
            }
        }

        protected void lvSenasRelacionadas_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "VerSena")
            {
                int idSenaSeleccionada = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"~/Vista/Usuario/SeñaDetalle.aspx?id={idSenaSeleccionada}");
            }
        }

        private void MostrarMensajeExito(string mensaje)
        {
            alertSuccess.Visible = true;
            successMessage.InnerText = mensaje;
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
    }
}