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

        
        public int MtdRegistrarTipoSena(ClTipoSeñaE oTipoSena)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(oTipoSena.tipo) ||
                    string.IsNullOrWhiteSpace(oTipoSena.descripcion) ||
                    oTipoSena.idModulo <= 0)
                {
                    return 0; 
                }

                
                ClTipoSeñaD datosTipoSena = new ClTipoSeñaD();
                return datosTipoSena.MtdRegistrarTipoSena(oTipoSena);
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarTipoSena (Lógica): " + ex.Message);
                throw; 
            }
        }

        
        public DataTable MtdListarTiposSenaRecientes(int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    cantidad = 5; 
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