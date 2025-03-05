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
    public partial class BibliotecaSeñas : System.Web.UI.Page
    {
        // Propiedad para almacenar el ID del módulo seleccionado
        public int SelectedModuloId
        {
            get
            {
                if (ViewState["SelectedModuloId"] == null)
                {
                    return 0;
                }
                return (int)ViewState["SelectedModuloId"];
            }
            set
            {
                ViewState["SelectedModuloId"] = value;
            }
        }

        // Almacenar datos de tipos de señas por módulo
        private Dictionary<int, int> tiposSenaPorModulo = new Dictionary<int, int>();

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
                // Cargar datos iniciales
                CargarEstadisticasGlobales();
                CargarModulos();

                // Verificar si hay un módulo seleccionado en la URL
                if (Request.QueryString["modulo"] != null)
                {
                    int idModulo;
                    if (int.TryParse(Request.QueryString["modulo"], out idModulo))
                    {
                        SeleccionarModulo(idModulo);
                    }
                }
            }
        }

        private void CargarEstadisticasGlobales()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["userID"]);

                // Obtener el resumen de progreso
                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtResumen = logicaProgreso.MtdObtenerResumenProgreso(idUsuario);

                if (dtResumen.Rows.Count > 0)
                {
                    DataRow row = dtResumen.Rows[0];

                    // Extraer datos
                    int totalSenas = Convert.ToInt32(row["totalSenas"]);
                    int senasVistas = Convert.ToInt32(row["senasVistas"]);
                    int porcentajeSenas = Convert.ToInt32(row["porcentajeSenas"]);
                    int totalModulos = Convert.ToInt32(row["totalModulos"]);
                    int modulosCompletados = Convert.ToInt32(row["modulosCompletados"]);
                    int porcentajeModulos = Convert.ToInt32(row["porcentajeModulos"]);

                    // Calcular porcentaje global (promedio de ambos)
                    int porcentajeGlobal = (porcentajeSenas + porcentajeModulos) / 2;

                    // Actualizar etiquetas
                    lblTotalSenas.Text = totalSenas.ToString();
                    lblSenasVistas.Text = senasVistas.ToString();
                    lblTotalModulos.Text = totalModulos.ToString();
                    lblModulosCompletados.Text = modulosCompletados.ToString();
                    lblPorcentajeGlobal.Text = porcentajeGlobal.ToString() + "%";

                    // Crear barra de progreso
                    litProgressBar.Text = string.Format(
                        "<div class='progress-bar bg-success' role='progressbar' style='width: {0}%' " +
                        "aria-valuenow='{0}' aria-valuemin='0' aria-valuemax='100'></div>",
                        porcentajeGlobal);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar estadísticas globales: " + ex.Message);
            }
        }

        private void CargarModulos()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["userID"]);
                string busqueda = txtBuscarModulo.Text.Trim();

                // Obtener módulos con progreso
                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtModulos = logicaProgreso.MtdObtenerModulosConProgreso(idUsuario);

                // Agregar depuración
                System.Diagnostics.Debug.WriteLine($"Se cargaron {dtModulos.Rows.Count} módulos de la base de datos");

                foreach (DataRow row in dtModulos.Rows)
                {
                    System.Diagnostics.Debug.WriteLine($"Módulo: {row["idModulo"]} - {row["nombreModulo"]}");
                }

                // Aplicar filtro de búsqueda si existe
                if (!string.IsNullOrEmpty(busqueda))
                {
                    string filtro = string.Format("nombreModulo LIKE '%{0}%'",
                        busqueda.Replace("'", "''"));

                    DataView dv = new DataView(dtModulos);
                    dv.RowFilter = filtro;
                    dtModulos = dv.ToTable();

                    // Depuración del filtro
                    System.Diagnostics.Debug.WriteLine($"Después del filtro: {dtModulos.Rows.Count} módulos");
                }

                // Precalcular la cantidad de tipos de señas por módulo
                CargarTiposSenaPorModulo(dtModulos);

                // Asignar al repeater
                rptModulos.DataSource = dtModulos;
                rptModulos.DataBind();

                // Depuración después del databind
                System.Diagnostics.Debug.WriteLine($"Repeater tiene {rptModulos.Items.Count} items");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar módulos: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("StackTrace: " + ex.StackTrace);
            }
        }

        private void CargarTiposSenaPorModulo(DataTable dtModulos)
        {
            // Limpiar el diccionario existente
            tiposSenaPorModulo.Clear();

            try
            {
                ClSeñaL logicaSena = new ClSeñaL();

                foreach (DataRow row in dtModulos.Rows)
                {
                    int idModulo = Convert.ToInt32(row["idModulo"]);

                    // Obtener los tipos de seña para este módulo
                    DataTable dtTipos = logicaSena.MtdObtenerTiposSenaPorModulo(idModulo);

                    // Guardar la cantidad en el diccionario
                    tiposSenaPorModulo[idModulo] = dtTipos != null ? dtTipos.Rows.Count : 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar tipos de seña por módulo: " + ex.Message);
                // No lanzar excepción para evitar interrumpir el flujo
            }
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

        // Método que será llamado desde el archivo ASPX para obtener la cantidad de tipos de seña
        public int GetCantidadTiposSena(int idModulo)
        {
            if (tiposSenaPorModulo.ContainsKey(idModulo))
            {
                return tiposSenaPorModulo[idModulo];
            }
            return 0;
        }

        private void SeleccionarModulo(int idModulo)
        {
            try
            {
                // Actualizar el ID del módulo seleccionado
                SelectedModuloId = idModulo;

                // Obtener el nombre del módulo
                ClTipoSeñaL logicaTipoSena = new ClTipoSeñaL();
                DataTable dtModulos = logicaTipoSena.MtdObtenerModulos();
                DataRow[] filas = dtModulos.Select("idModulo = " + idModulo);

                string nombreModulo = "Módulo seleccionado";
                if (filas.Length > 0)
                {
                    nombreModulo = filas[0]["nombreModulo"].ToString();
                }

                // Actualizar el título
                litTituloSenas.Text = "Señas del módulo: " + nombreModulo;

                // Ocultar panel de selección
                pnlSeleccionaModulo.Visible = false;

                // Cargar los tipos de seña para este módulo
                CargarTiposSena(idModulo);

                // Cargar las señas del módulo
                CargarSenas();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al seleccionar módulo: " + ex.Message);
            }
        }

        private void CargarTiposSena(int idModulo)
        {
            try
            {
                // Limpiar el dropdown actual
                ddlTipoSena.Items.Clear();

                // Obtener tipos de seña para el módulo seleccionado
                ClSeñaL logicaSena = new ClSeñaL();
                DataTable dtTiposSena = logicaSena.MtdObtenerTiposSenaPorModulo(idModulo);

                // Asignar al dropdown
                ddlTipoSena.DataSource = dtTiposSena;
                ddlTipoSena.DataTextField = "tipo";
                ddlTipoSena.DataValueField = "idTiposeña";
                ddlTipoSena.DataBind();

                // Agregar la opción "Todos los tipos" al inicio
                ddlTipoSena.Items.Insert(0, new ListItem("Todos los tipos", "0"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar tipos de seña: " + ex.Message);
            }
        }

        private void CargarSenas()
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["userID"]);
                int idModulo = SelectedModuloId;
                int idTipoSena = Convert.ToInt32(ddlTipoSena.SelectedValue);
                string estado = ddlEstado.SelectedValue;
                string busqueda = txtBuscarSena.Text.Trim();

                if (idModulo <= 0)
                {
                    // No hay módulo seleccionado, no mostramos señas
                    rptSenas.DataSource = null;
                    rptSenas.DataBind();
                    return;
                }

                // Obtener señas del módulo
                ClProgresoL logicaProgreso = new ClProgresoL();
                DataTable dtSenas = logicaProgreso.MtdObtenerProgresoModulo(idUsuario, idModulo);

                // Aplicar filtro por tipo de seña
                if (idTipoSena > 0)
                {
                    dtSenas.DefaultView.RowFilter = "idTipoSeña = " + idTipoSena;
                    dtSenas = dtSenas.DefaultView.ToTable();
                }

                // Aplicar filtro por estado
                if (estado != "todos")
                {
                    bool estadoFiltro = (estado == "vistas");
                    dtSenas.DefaultView.RowFilter = "estado = " + (estadoFiltro ? "true" : "false");
                    dtSenas = dtSenas.DefaultView.ToTable();
                }

                // Aplicar filtro por búsqueda
                if (!string.IsNullOrEmpty(busqueda))
                {
                    string filtro = string.Format("nombreSeña LIKE '%{0}%'",
                        busqueda.Replace("'", "''"));

                    dtSenas.DefaultView.RowFilter = filtro;
                    dtSenas = dtSenas.DefaultView.ToTable();
                }

                // Asignar al repeater
                rptSenas.DataSource = dtSenas;
                rptSenas.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar señas: " + ex.Message);
            }
        }

        protected void txtBuscarModulo_TextChanged(object sender, EventArgs e)
        {
            // Recargar módulos con filtro
            CargarModulos();
        }

        protected void rptModulos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectModulo")
            {
                int idModulo = Convert.ToInt32(e.CommandArgument);
                SeleccionarModulo(idModulo);
            }
        }

        protected void ddlTipoSena_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recargar señas con el nuevo filtro
            CargarSenas();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recargar señas con el nuevo filtro
            CargarSenas();
        }

        protected void btnBuscarSena_Click(object sender, EventArgs e)
        {
            // Recargar señas con el nuevo filtro
            CargarSenas();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            // Resetear filtros
            ddlTipoSena.SelectedValue = "0";
            ddlEstado.SelectedValue = "todos";
            txtBuscarSena.Text = string.Empty;

            // Recargar señas
            CargarSenas();
        }

        protected void rptSenas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                // Obtener el ID de la seña y el comando
                int idSena = Convert.ToInt32(e.CommandArgument);
                string comando = e.CommandName;
                int idUsuario = Convert.ToInt32(Session["userID"]);

                // Crear objeto de progreso
                ClProgresoE oProgreso = new ClProgresoE
                {
                    idUsuario = idUsuario,
                    idSeña = idSena,
                    estado = (comando == "MarcarVisto") // true si se marca como visto, false si se marca como pendiente
                };

                // Actualizar el progreso
                ClProgresoL logicaProgreso = new ClProgresoL();
                int resultado = logicaProgreso.MtdRegistrarProgreso(oProgreso);

                if (resultado > 0)
                {
                    // Recargar datos
                    CargarEstadisticasGlobales();
                    CargarSenas();

                    // Actualizar el progreso en la lista de módulos
                    CargarModulos();
                }
                else
                {
                    // Mostrar mensaje de error
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                        "alert('No se pudo actualizar el estado de la seña.');", true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en ItemCommand de señas: " + ex.Message);
            }
        }
    }
}