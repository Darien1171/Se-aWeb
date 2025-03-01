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
        // Método para contar módulos
        public int MtdContarModulos()
        {
            ClModuloD datosModulo = new ClModuloD();
            return datosModulo.MtdContarModulos();
        }

        // Método para registrar un nuevo módulo
        public int MtdRegistrarModulo(ClModuloE oModulo)
        {
            try
            {
                // Validaciones adicionales si son necesarias
                if (string.IsNullOrWhiteSpace(oModulo.nombreModulo) || string.IsNullOrWhiteSpace(oModulo.descripcion))
                {
                    return 0; // No se puede registrar con datos faltantes
                }

                // Realizar la operación a través de la capa de datos
                ClModuloD datosModulo = new ClModuloD();
                return datosModulo.MtdRegistrarModulo(oModulo);
            }
            catch (Exception ex)
            {
                // Registrar el error para depuración
                System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarModulo (Lógica): " + ex.Message);
                throw; // Re-lanzar para manejo en capa superior
            }
        }

        // Método para listar módulos recientes
        public DataTable MtdListarModulosRecientes(int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    cantidad = 5; // Valor predeterminado
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