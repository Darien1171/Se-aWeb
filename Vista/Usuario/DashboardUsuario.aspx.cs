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
                // Verificar si hay una sesión activa
                if (Session["userID"] == null)
                {
                    Response.Redirect("~/Vista/Login.aspx");
                    return;
                }

                // Cargar datos del usuario y establecer nombre
                int idUsuario = Convert.ToInt32(Session["userID"]);
                CargarDatosUsuario(idUsuario);

                // Cargar progreso general del usuario
                CargarProgresoGeneral(idUsuario);

                // Cargar módulos recientes con progreso
                CargarModulosRecientes(idUsuario);

                // Cargar señas recientes vistas
                CargarSenasRecientes(idUsuario);
            }
        }

        private void CargarDatosUsuario(int idUsuario)
        {
            try
            {
                // Obtener el nombre del usuario a través de un método en la capa de lógica
                ClUsuarioL logicaUsuario = new ClUsuarioL();
                ClUsuarioE oUsuario = new ClUsuarioE { idUsuario = idUsuario };

                // Para obtener el usuario necesitaríamos un nuevo método en la clase ClUsuarioL,
                // similar a "ObtenerUsuarioPorId". Por ahora usamos un nombre genérico
                lblUserName.Text = "Estudiante";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarDatosUsuario: " + ex.Message);
                // Mantener el valor predeterminado
            }
        }

        private void CargarProgresoGeneral(int idUsuario)
        {
            try
            {
                // Obtener el resumen de progreso del usuario usando la capa de lógica
                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtResumen = logicaProgreso.MtdObtenerResumenProgreso(idUsuario);

                if (dtResumen.Rows.Count > 0)
                {
                    DataRow row = dtResumen.Rows[0];

                    // Extraer los datos del resumen
                    int totalSenas = Convert.ToInt32(row["totalSenas"]);
                    int senasVistas = Convert.ToInt32(row["senasVistas"]);
                    int porcentajeSenas = Convert.ToInt32(row["porcentajeSenas"]);
                    int totalModulos = Convert.ToInt32(row["totalModulos"]);
                    int modulosCompletados = Convert.ToInt32(row["modulosCompletados"]);
                    int porcentajeModulos = Convert.ToInt32(row["porcentajeModulos"]);

                    // Calcular el promedio para el progreso general (promedio de ambos porcentajes)
                    int progresoGeneral = (porcentajeSenas + porcentajeModulos) / 2;

                    // Actualizar controles en la interfaz
                    hdnProgresoGeneral.Value = progresoGeneral.ToString();
                    lblSenasProgress.Text = porcentajeSenas.ToString() + "%";
                    lblModulosProgress.Text = porcentajeModulos.ToString() + "%";
                    lblPuntajeGeneral.Text = progresoGeneral.ToString() + "%";

                    // Actualizar etiquetas de detalles
                    lblSenasDetail.Text = $"{senasVistas} de {totalSenas} señas aprendidas";
                    lblModulosDetail.Text = $"{modulosCompletados} de {totalModulos} módulos completados";
                }

                // Obtener última evaluación
                DataTable dtUltimaEvaluacion = ObtenerUltimaEvaluacion(idUsuario);
                if (dtUltimaEvaluacion.Rows.Count > 0)
                {
                    DateTime fechaEval = Convert.ToDateTime(dtUltimaEvaluacion.Rows[0]["fechaEvaluacion"]);
                    int puntaje = Convert.ToInt32(dtUltimaEvaluacion.Rows[0]["puntaje"]);
                    lblUltimaEvaluacion.Text = $"Última evaluación: {fechaEval.ToString("dd/MM/yyyy")} - Puntaje: {puntaje}/100";
                }
                else
                {
                    lblUltimaEvaluacion.Text = "Sin evaluaciones completadas";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarProgresoGeneral: " + ex.Message);
            }
        }

        private DataTable ObtenerUltimaEvaluacion(int idUsuario)
        {
            // Esta función obtiene la última evaluación del usuario
            // En una implementación real, esto debería estar en una capa de lógica separada
            DataTable dtUltimaEvaluacion = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM evaluacion WHERE IdUsuario = @idUsuario ORDER BY fechaEvaluacion DESC", conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtUltimaEvaluacion);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en ObtenerUltimaEvaluacion: " + ex.Message);
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtUltimaEvaluacion;
        }

        private void CargarModulosRecientes(int idUsuario)
        {
            try
            {
                // Obtener módulos con progreso de la capa lógica
                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtModulos = logicaProgreso.MtdObtenerModulosConProgreso(idUsuario);

                // Limitar a los 5 más recientes
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
                // Obtener progreso de señas del usuario (limitado a 5)
                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtProgreso = logicaProgreso.MtdObtenerProgresoUsuario(idUsuario);

                // Limitar a las 5 más recientes
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