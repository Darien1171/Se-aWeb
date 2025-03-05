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
        // Variable para almacenar el ID de la seña actual
        private int idSena = 0;

        // Variable para almacenar el ID del módulo al que pertenece la seña
        private int idModulo = 0;

        // Variable para almacenar el estado actual de la seña
        private bool estaVisto = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si hay una sesión activa
            if (Session["userID"] == null)
            {
                Response.Redirect("~/Vista/Login.aspx");
                return;
            }

            // Obtener el ID de la seña de la URL
            if (Request.QueryString["id"] != null)
            {
                if (int.TryParse(Request.QueryString["id"], out idSena))
                {
                    if (!IsPostBack)
                    {
                        // Cargar los datos de la seña
                        CargarDetalleSena();

                        // Cargar señas relacionadas
                        CargarSenasRelacionadas();

                        // Actualizar información de progreso
                        ActualizarProgreso();
                    }
                }
                else
                {
                    // ID de seña inválido, redirigir a la biblioteca de señas
                    Response.Redirect("~/Vista/Usuario/BibliotecaSeñas.aspx");
                }
            }
            else
            {
                // No se proporcionó ID de seña, redirigir a la biblioteca de señas
                Response.Redirect("~/Vista/Usuario/BibliotecaSeñas.aspx");
            }
        }

        private void CargarDetalleSena()
        {
            try
            {
                // Obtener usuario actual
                int idUsuario = Convert.ToInt32(Session["userID"]);

                // Usar la capa lógica
                ClSeñaL logicaSena = new ClSeñaL();

                // Obtener datos de la seña y su estado para el usuario actual
                DataTable dtSena = logicaSena.MtdObtenerDetalleSena(idSena, idUsuario);

                if (dtSena.Rows.Count > 0)
                {
                    DataRow row = dtSena.Rows[0];

                    // Llenar los controles con los datos de la seña
                    lblNombreSena.InnerText = row["nombreSeña"].ToString();
                    lblTipoSena.InnerText = row["tipoSeña"].ToString();

                    // URL del video
                    string urlVideo = row["urlVideo"].ToString();
                    videoPlayer.Src = ResolveUrl(urlVideo);

                    // Descripción (si existe)
                    if (row["descripcionTipo"] != DBNull.Value)
                    {
                        lblDescripcion.InnerText = row["descripcionTipo"].ToString();
                    }
                    else
                    {
                        lblDescripcion.InnerText = "No hay descripción disponible para esta seña.";
                    }

                    // Obtener ID del módulo para el enlace y las señas relacionadas
                    idModulo = Convert.ToInt32(row["idModulo"]);

                    // Configurar el enlace al módulo
                    lnkModulo.Text = row["nombreModulo"].ToString();
                    lnkModulo.NavigateUrl = $"~/Vista/Usuario/BibliotecaSeñas.aspx?modulo={idModulo}";

                    // Verificar si la seña está marcada como vista
                    estaVisto = row["estado"] != DBNull.Value && ConvertToBoolean(row["estado"]);

                    // Actualizar los botones según el estado
                    ActualizarBotonesEstado();
                }
                else
                {
                    // La seña no existe, redirigir a la biblioteca
                    Response.Redirect("~/Vista/Usuario/BibliotecaSeñas.aspx");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarDetalleSena: " + ex.Message);
                // Opcionalmente mostrar un mensaje de error al usuario
            }
        }

        private void CargarSenasRelacionadas()
        {
            try
            {
                if (idModulo <= 0)
                {
                    return; // No tenemos un módulo válido para buscar señas relacionadas
                }

                int idUsuario = Convert.ToInt32(Session["userID"]);

                // Usar la capa lógica para obtener los datos
                ClProgresoL logicaProgreso = new ClProgresoL();

                // Obtener todas las señas del mismo módulo (excluyendo la seña actual)
                DataTable dtSenasRelacionadas = logicaProgreso.MtdObtenerProgresoModulo(idUsuario, idModulo);

                // Filtrar para excluir la seña actual
                dtSenasRelacionadas.DefaultView.RowFilter = $"idSeña <> {idSena}";
                dtSenasRelacionadas = dtSenasRelacionadas.DefaultView.ToTable();

                // Limitar a máximo 5 señas relacionadas
                if (dtSenasRelacionadas.Rows.Count > 5)
                {
                    DataTable dtLimitado = dtSenasRelacionadas.Clone();
                    for (int i = 0; i < 5; i++)
                    {
                        dtLimitado.ImportRow(dtSenasRelacionadas.Rows[i]);
                    }
                    dtSenasRelacionadas = dtLimitado;
                }

                // Asignar al ListView
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
                    return; // No tenemos un módulo válido
                }

                int idUsuario = Convert.ToInt32(Session["userID"]);

                // Usar la capa lógica
                ClProgresoL logicaProgreso = new ClProgresoL();

                // Obtener todas las señas del mismo módulo
                DataTable dtSenasModulo = logicaProgreso.MtdObtenerProgresoModulo(idUsuario, idModulo);

                // Contar señas vistas y total
                int totalSenas = dtSenasModulo.Rows.Count;
                int senasVistas = 0;

                foreach (DataRow row in dtSenasModulo.Rows)
                {
                    if (row["estado"] != DBNull.Value && ConvertToBoolean(row["estado"]))
                    {
                        senasVistas++;
                    }
                }

                // Calcular porcentaje
                int porcentaje = (totalSenas > 0) ? (senasVistas * 100) / totalSenas : 0;

                // Actualizar controles
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
                // Actualizar estado a "visto"
                ActualizarEstadoSena(true);

                // Mostrar mensaje de éxito
                MostrarMensajeExito("¡Seña marcada como vista correctamente!");

                // Actualizar UI
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
                // Actualizar estado a "pendiente"
                ActualizarEstadoSena(false);

                // Mostrar mensaje de éxito
                MostrarMensajeExito("Seña marcada como pendiente.");

                // Actualizar UI
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

            // Crear objeto de progreso
            ClProgresoE oProgreso = new ClProgresoE
            {
                idUsuario = idUsuario,
                idSeña = idSena,
                estado = estado
            };

            // Actualizar el progreso mediante la capa lógica
            ClProgresoL logicaProgreso = new ClProgresoL();
            logicaProgreso.MtdRegistrarProgreso(oProgreso);
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            // Regresar a la página anterior o a la biblioteca de señas con el módulo seleccionado
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

        // Método auxiliar para convertir diferentes formatos a booleano
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