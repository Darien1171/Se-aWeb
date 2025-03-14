using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SeñaWeb.Datos;

namespace SeñaWeb.Logica
{
    public class ClModuloL
    {
        
        public int MtdContarModulos()
        {
            ClModuloD datosModulo = new ClModuloD();
            return datosModulo.MtdContarModulos();
        }

        
        public int MtdRegistrarModulo(ClModuloE oModulo)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(oModulo.nombreModulo) || string.IsNullOrWhiteSpace(oModulo.descripcion))
                {
                    return 0; 
                }

                
                ClModuloD datosModulo = new ClModuloD();
                return datosModulo.MtdRegistrarModulo(oModulo);
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarModulo (Lógica): " + ex.Message);
                throw; 
            }
        }

        
        public DataTable MtdListarModulosRecientes(int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    cantidad = 5; 
                }

                ClModuloD datosModulo = new ClModuloD();
                return datosModulo.MtdListarModulosRecientes(cantidad);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en MtdListarModulosRecientes (Lógica): " + ex.Message);
                throw;
            }
        }
    }
}