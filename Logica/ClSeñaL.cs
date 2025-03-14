using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SeñaWeb.Datos;

namespace SeñaWeb.Logica
{
    public class ClSeñaL
    {
        public int MtdContarSenas()
        {
            ClSeñaD datosSena = new ClSeñaD();
            return datosSena.MtdContarSenas();
        }

        
        public int MtdRegistrarSena(ClSeñaE oSena)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(oSena.nombreSeña) ||
                    string.IsNullOrWhiteSpace(oSena.urlVideo) ||
                    oSena.idTipoSeña <= 0)
                {
                    return 0; 
                }

                
                ClSeñaD datosSena = new ClSeñaD();
                return datosSena.MtdRegistrarSena(oSena);
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarSena (Lógica): " + ex.Message);
                throw; 
            }
        }

        
        public DataTable MtdListarSenasRecientes(int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    cantidad = 5; 
                }

                ClSeñaD datosSena = new ClSeñaD();
                return datosSena.MtdListarSenasRecientes(cantidad);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdListarSenasRecientes (Lógica): " + ex.Message);
                throw;
            }
        }

        
        public DataTable MtdObtenerTiposSenaPorModulo(int idModulo)
        {
            try
            {
                ClSeñaD datosSena = new ClSeñaD();
                return datosSena.MtdObtenerTiposSenaPorModulo(idModulo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerTiposSenaPorModulo (Lógica): " + ex.Message);
                throw;
            }
        }

        public DataTable MtdObtenerDetalleSena(int idSena, int idUsuario)
        {
            try
            {
                
                if (idSena <= 0)
                {
                    throw new ArgumentException("El ID de la seña no es válido.");
                }

                if (idUsuario <= 0)
                {
                    throw new ArgumentException("El ID del usuario no es válido.");
                }

                
                ClSeñaD datosSena = new ClSeñaD();
                return datosSena.MtdObtenerDetalleSena(idSena, idUsuario);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerDetalleSena (Lógica): " + ex.Message);
                throw; 
            }
        }
    }
}