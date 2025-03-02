using SeñaWeb.Datos;
using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SeñaWeb.Logica
{
    public class ClProgresoL
    {
        // Método para registrar o actualizar el progreso de un usuario
        public int MtdRegistrarProgreso(ClProgresoE oProgreso)
        {
            try
            {
                // Validaciones adicionales
                if (oProgreso.idUsuario <= 0 || oProgreso.idSeña <= 0)
                {
                    return 0; // No se puede registrar con datos inválidos
                }

                // Realizar la operación a través de la capa de datos
                ClProgresoD datosProgreso = new ClProgresoD();
                return datosProgreso.MtdRegistrarProgreso(oProgreso);
            }
            catch (Exception ex)
            {
                // Registrar el error para depuración
                System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarProgreso (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }

        // Método para obtener el progreso de un usuario
        public DataTable MtdObtenerProgresoUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return new DataTable(); // Retornar tabla vacía si el ID es inválido
                }

                ClProgresoD datosProgreso = new ClProgresoD();
                return datosProgreso.MtdObtenerProgresoUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerProgresoUsuario (Lógica): " + ex.Message);
                throw;
            }
        }

        // Método para obtener el progreso de un usuario en un módulo específico
        public DataTable MtdObtenerProgresoModulo(int idUsuario, int idModulo)
        {
            try
            {
                if (idUsuario <= 0 || idModulo <= 0)
                {
                    return new DataTable(); // Retornar tabla vacía si los IDs son inválidos
                }

                ClProgresoD datosProgreso = new ClProgresoD();
                return datosProgreso.MtdObtenerProgresoModulo(idUsuario, idModulo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerProgresoModulo (Lógica): " + ex.Message);
                throw;
            }
        }

        // Método para contar señas vistas por el usuario
        public int MtdContarSenasVistas(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return 0; // Retornar 0 si el ID es inválido
                }

                ClProgresoD datosProgreso = new ClProgresoD();
                return datosProgreso.MtdContarSenasVistas(idUsuario);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdContarSenasVistas (Lógica): " + ex.Message);
                throw;
            }
        }

        // Método para obtener módulos con porcentaje de completado
        public DataTable MtdObtenerModulosConProgreso(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return new DataTable(); // Retornar tabla vacía si el ID es inválido
                }

                ClProgresoD datosProgreso = new ClProgresoD();
                DataTable dtModulos = datosProgreso.MtdObtenerModulosConProgreso(idUsuario);

                // Procesar resultados para asegurar formato correcto
                foreach (DataRow row in dtModulos.Rows)
                {
                    if (row["porcentajeCompletado"] != DBNull.Value)
                    {
                        double porcentaje = Convert.ToDouble(row["porcentajeCompletado"]);
                        row["porcentajeCompletado"] = Math.Round(porcentaje, 0);
                    }
                    else
                    {
                        row["porcentajeCompletado"] = 0;
                    }
                }

                return dtModulos;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerModulosConProgreso (Lógica): " + ex.Message);
                throw;
            }
        }

        // Método para obtener módulos completados
        public DataTable MtdObtenerModulosCompletados(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return new DataTable(); // Retornar tabla vacía si el ID es inválido
                }

                ClProgresoD datosProgreso = new ClProgresoD();
                return datosProgreso.MtdObtenerModulosCompletados(idUsuario);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerModulosCompletados (Lógica): " + ex.Message);
                throw;
            }
        }

        // Método para obtener el resumen de progreso
        public DataTable MtdObtenerResumenProgreso(int idUsuario)
        {
            try
            {
                DataTable dtResumen = new DataTable();
                dtResumen.Columns.Add("totalSenas", typeof(int));
                dtResumen.Columns.Add("senasVistas", typeof(int));
                dtResumen.Columns.Add("porcentajeSenas", typeof(int));
                dtResumen.Columns.Add("totalModulos", typeof(int));
                dtResumen.Columns.Add("modulosCompletados", typeof(int));
                dtResumen.Columns.Add("porcentajeModulos", typeof(int));

                // Obtener el total de señas
                ClSeñaL logicaSena = new ClSeñaL();
                int totalSenas = logicaSena.MtdContarSenas();

                // Obtener señas vistas por el usuario
                ClProgresoD datosProgreso = new ClProgresoD();
                int senasVistas = datosProgreso.MtdContarSenasVistas(idUsuario);

                // Calcular porcentaje de señas vistas
                int porcentajeSenas = totalSenas > 0 ? (senasVistas * 100) / totalSenas : 0;

                // Obtener el total de módulos
                ClModuloL logicaModulo = new ClModuloL();
                int totalModulos = logicaModulo.MtdContarModulos();

                // Obtener módulos completados
                DataTable dtModulosCompletados = datosProgreso.MtdObtenerModulosCompletados(idUsuario);
                int modulosCompletados = dtModulosCompletados.Rows.Count;

                // Calcular porcentaje de módulos completados
                int porcentajeModulos = totalModulos > 0 ? (modulosCompletados * 100) / totalModulos : 0;

                // Añadir fila con los datos
                DataRow row = dtResumen.NewRow();
                row["totalSenas"] = totalSenas;
                row["senasVistas"] = senasVistas;
                row["porcentajeSenas"] = porcentajeSenas;
                row["totalModulos"] = totalModulos;
                row["modulosCompletados"] = modulosCompletados;
                row["porcentajeModulos"] = porcentajeModulos;
                dtResumen.Rows.Add(row);

                return dtResumen;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerResumenProgreso (Lógica): " + ex.Message);
                throw;
            }
        }
    }
}