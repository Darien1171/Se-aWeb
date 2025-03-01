using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeñaWeb.Entidad;
using SeñaWeb.Logica;
using SeñaWeb.Datos;

namespace SeñaWeb.Vista.Admin
{
    public partial class RegistrarSeña : System.Web.UI.Page
    {
        // Extensiones de archivo permitidas para videos
        private readonly string[] extensionesPermitidas = new string[] { ".mp4", ".avi", ".mov", ".wmv" };
        // Tamaño máximo de archivo en MB
        private const int tamanoMaximoMB = 50;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar datos iniciales
                try
                {
                    CargarTiposSena();
                    CargarSenasRecientes();
                }
                catch (Exception ex)
                {
                    // Solo logueamos el error pero no mostramos nada al usuario en la carga inicial
                    System.Diagnostics.Debug.WriteLine("Error al cargar datos iniciales: " + ex.Message);
                }
            }
        }

        private void CargarTiposSena()
        {
            ClTipoSeñaD datosTipoSena = new ClTipoSeñaD();
            DataTable dtTiposSena = datosTipoSena.MtdObtenerTodosTiposSena();

            ddlTipoSena.Items.Clear();

            if (dtTiposSena != null && dtTiposSena.Rows.Count > 0)
            {
                ddlTipoSena.DataSource = dtTiposSena;
                ddlTipoSena.DataTextField = "tipo";
                ddlTipoSena.DataValueField = "idTiposeña";
                ddlTipoSena.DataBind();

                // Agregar item por defecto
                ddlTipoSena.Items.Insert(0, new ListItem("-- Seleccione un tipo de seña --", "0"));
            }
            else
            {
                ddlTipoSena.Items.Add(new ListItem("No hay tipos de seña disponibles", "0"));
                btnRegistrar.Enabled = false;
                MostrarError("No existen tipos de seña registrados. Debe crear al menos un tipo de seña antes de registrar señas.");
            }
        }

        private void CargarSenasRecientes()
        {
            ClSeñaL logicaSena = new ClSeñaL();
            DataTable dtSenas = logicaSena.MtdListarSenasRecientes(5);

            if (dtSenas != null && dtSenas.Rows.Count > 0)
            {
                gvSenasRecientes.DataSource = dtSenas;
                gvSenasRecientes.DataBind();
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación manual
                if (ddlTipoSena.SelectedValue == "0")
                {
                    MostrarError("Debe seleccionar un tipo de seña.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombreSena.Text))
                {
                    MostrarError("El nombre de la seña es obligatorio.");
                    return;
                }

                if (!fileVideo.HasFile)
                {
                    MostrarError("Debe seleccionar un archivo de video para la seña.");
                    return;
                }

                // Validar la extensión del archivo
                string extension = Path.GetExtension(fileVideo.FileName).ToLower();
                if (!extensionesPermitidas.Contains(extension))
                {
                    MostrarError($"El tipo de archivo no es válido. Tipos permitidos: {string.Join(", ", extensionesPermitidas)}");
                    return;
                }

                // Validar el tamaño del archivo
                if (fileVideo.PostedFile.ContentLength > tamanoMaximoMB * 1024 * 1024)
                {
                    MostrarError($"El archivo excede el tamaño máximo permitido de {tamanoMaximoMB} MB.");
                    return;
                }

                // Generar un nombre único para el archivo
                string nombreArchivo = GenerarNombreArchivoUnico(txtNombreSena.Text, extension);

                // Ruta de la carpeta "Videos" en la raíz del proyecto
                string rutaCarpeta = Server.MapPath("~/Videos/");

                // Verificar si la carpeta existe, si no, crearla
                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }

                // Ruta completa del archivo
                string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                // Guardar el archivo
                fileVideo.SaveAs(rutaCompleta);

                // Ruta relativa para guardar en la base de datos
                string rutaRelativa = "~/Videos/" + nombreArchivo;

                // Crear objeto de entidad con los datos del formulario
                ClSeñaE senaNueva = new ClSeñaE
                {
                    nombreSeña = txtNombreSena.Text.Trim(),
                    urlVideo = rutaRelativa,
                    idTipoSeña = Convert.ToInt32(ddlTipoSena.SelectedValue)
                };

                // Instanciar la capa de lógica
                ClSeñaL logicaSena = new ClSeñaL();

                // Intentar guardar la seña
                int resultado = logicaSena.MtdRegistrarSena(senaNueva);

                if (resultado > 0)
                {
                    // Mostrar mensaje de éxito
                    MostrarExito($"¡La seña '{txtNombreSena.Text.Trim()}' ha sido registrada correctamente! ID: {resultado}");

                    // Mostrar vista previa del video
                    videoPreviewContainer.Visible = true;
                    videoPreview.Src = rutaRelativa;

                    // Limpiar los campos del formulario (excepto la vista previa)
                    txtNombreSena.Text = string.Empty;

                    // Recargar la lista de señas recientes
                    CargarSenasRecientes();
                }
                else
                {
                    MostrarError("No se pudo registrar la seña. Por favor, inténtelo nuevamente.");
                }
            }
            catch (Exception ex)
            {
                // Mostrar detalles del error para ayudar a depurar
                string errorMessage = "Error al registrar la seña: " + ex.Message;

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

        private string GenerarNombreArchivoUnico(string nombreOriginal, string extension)
        {
            // Limpiar el nombre original (quitar caracteres especiales y espacios)
            string nombreLimpio = new string(nombreOriginal.Where(c => char.IsLetterOrDigit(c) || c == '-' || c == '_').ToArray());

            // Agregar timestamp para garantizar unicidad
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Generar un GUID corto para mayor unicidad
            string guid = Guid.NewGuid().ToString("N").Substring(0, 8);

            // Combinar todo
            return $"{nombreLimpio}-{timestamp}-{guid}{extension}";
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