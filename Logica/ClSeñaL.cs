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

        // Método para registrar una nueva seña
        public int MtdRegistrarSena(ClSeñaE oSena)
        {
            try
            {
                // Validaciones adicionales
                if (string.IsNullOrWhiteSpace(oSena.nombreSeña) ||
                    string.IsNullOrWhiteSpace(oSena.urlVideo) ||
                    oSena.idTipoSeña <= 0)
                {
                    return 0; // No se puede registrar con datos faltantes o inválidos
                }

                // Realizar la operación a través de la capa de datos
                ClSeñaD datosSena = new ClSeñaD();
                return datosSena.MtdRegistrarSena(oSena);
            }
            catch (Exception ex)
            {
                // Registrar el error para depuración
                System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarSena (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }

        // Método para listar señas recientes
        public DataTable MtdListarSenasRecientes(int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    cantidad = 5; // Valor predeterminado
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

        // Método para obtener tipos de seña por módulo
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
                // Validaciones
                if (idSena <= 0)
                {
                    throw new ArgumentException("El ID de la seña no es válido.");
                }

                if (idUsuario <= 0)
                {
                    throw new ArgumentException("El ID del usuario no es válido.");
                }

                // Llamar a la capa de datos
                ClSeñaD datosSena = new ClSeñaD();
                return datosSena.MtdObtenerDetalleSena(idSena, idUsuario);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerDetalleSena (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }
    }
}