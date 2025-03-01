using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using SeñaWeb.Entidad;

namespace SeñaWeb.Datos
{
    public class ClModuloD
    {
        // Método existente para contar módulos
        public int MtdContarModulos()
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ContarModulos", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Método para registrar un nuevo módulo
        public int MtdRegistrarModulo(ClModuloE oModulo)
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    // Usar un comando SQL directo en lugar de un procedimiento almacenado
                    // para evitar depender de la existencia del procedimiento
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO modulo (nombreModulo, descripcion) VALUES (@nombreModulo, @descripcion); SELECT SCOPE_IDENTITY();", conex))
                    {
                        cmd.CommandType = CommandType.Text;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@nombreModulo", oModulo.nombreModulo);
                        cmd.Parameters.AddWithValue("@descripcion", oModulo.descripcion);

                        // Ejecutar y obtener el ID insertado
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Registrar la excepción para depuración
                    System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarModulo: " + ex.Message);
                    throw; // Re-lanzar la excepción para que sea manejada en la capa superior
                }
                finally
                {
                    // Asegurar que la conexión se cierre
                    conexion.MtdCerrarConexion();
                }

                return 0; // Si no se pudo insertar
            }
        }

        // Método para listar módulos recientes
        public DataTable MtdListarModulosRecientes(int cantidad)
        {
            DataTable dtModulos = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    // Usar consulta SQL directa en lugar de procedimiento almacenado
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP (@cantidad) idModulo, nombreModulo, descripcion FROM modulo ORDER BY idModulo DESC", conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);

                        // Ejecutar y llenar el DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtModulos);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdListarModulosRecientes: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtModulos;
        }
    }
}