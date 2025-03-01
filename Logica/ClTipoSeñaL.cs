using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SeñaWeb.Datos;

namespace SeñaWeb.Logica
{
    public class ClTipoSeñaL
    {
        public int MtdContarTiposSena()
        {
            ClTipoSeñaD datosTipoSena = new ClTipoSeñaD();
            return datosTipoSena.MtdContarTiposSena();
        }

        // Método para registrar un nuevo tipo de seña
        public int MtdRegistrarTipoSena(ClTipoSeñaE oTipoSena)
        {
            try
            {
                // Validaciones adicionales
                if (string.IsNullOrWhiteSpace(oTipoSena.tipo) ||
                    string.IsNullOrWhiteSpace(oTipoSena.descripcion) ||
                    oTipoSena.idModulo <= 0)
                {
                    return 0; // No se puede registrar con datos faltantes o inválidos
                }

                // Realizar la operación a través de la capa de datos
                ClTipoSeñaD datosTipoSena = new ClTipoSeñaD();
                return datosTipoSena.MtdRegistrarTipoSena(oTipoSena);
            }
            catch (Exception ex)
            {
                // Registrar el error para depuración
                System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarTipoSena (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }

        // Método para listar tipos de seña recientes
        public DataTable MtdListarTiposSenaRecientes(int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    cantidad = 5; // Valor predeterminado
                }

                ClTipoSeñaD datosTipoSena = new ClTipoSeñaD();
                return datosTipoSena.MtdListarTiposSenaRecientes(cantidad);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdListarTiposSenaRecientes (Lógica): " + ex.Message);
                throw;
            }
        }

        // Método para obtener todos los módulos para el dropdown
        public DataTable MtdObtenerModulos()
        {
            try
            {
                ClTipoSeñaD datosTipoSena = new ClTipoSeñaD();
                return datosTipoSena.MtdObtenerModulos();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdObtenerModulos (Lógica): " + ex.Message);
                throw;
            }
        }
    }
}