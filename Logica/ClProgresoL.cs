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
        
        public int MtdRegistrarProgreso(ClProgresoE oProgreso)
        {
            try
            {
                
                if (oProgreso.idUsuario <= 0 || oProgreso.idSeña <= 0)
                {
                    return 0; 
                }

                
                ClProgresoD datosProgreso = new ClProgresoD();
                return datosProgreso.MtdRegistrarProgreso(oProgreso);
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarProgreso (Lógica): " + ex.Message);
                throw; 
            }
        }

        
        public DataTable MtdObtenerProgresoUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return new DataTable(); 
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

        
        public DataTable MtdObtenerProgresoModulo(int idUsuario, int idModulo)
        {
            try
            {
                if (idUsuario <= 0 || idModulo <= 0)
                {
                    return new DataTable(); 
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

        
        public int MtdContarSenasVistas(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return 0; 
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

        
        public DataTable MtdObtenerModulosConProgreso(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return new DataTable(); 
                }

                ClProgresoD datosProgreso = new ClProgresoD();
                DataTable dtModulos = datosProgreso.MtdObtenerModulosConProgreso(idUsuario);

                
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

        
        public DataTable MtdObtenerModulosCompletados(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    return new DataTable(); 
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

                
                ClSeñaL logicaSena = new ClSeñaL();
                int totalSenas = logicaSena.MtdContarSenas();

                
                ClProgresoD datosProgreso = new ClProgresoD();
                int senasVistas = datosProgreso.MtdContarSenasVistas(idUsuario);

                
                int porcentajeSenas = totalSenas > 0 ? (senasVistas * 100) / totalSenas : 0;

                
                ClModuloL logicaModulo = new ClModuloL();
                int totalModulos = logicaModulo.MtdContarModulos();

                
                DataTable dtModulosCompletados = datosProgreso.MtdObtenerModulosCompletados(idUsuario);
                int modulosCompletados = dtModulosCompletados.Rows.Count;

                
                int porcentajeModulos = totalModulos > 0 ? (modulosCompletados * 100) / totalModulos : 0;

                
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