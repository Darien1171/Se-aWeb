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

        
        public int MtdRegistrarModulo(ClModuloE oModulo)
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    
                    
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO modulo (nombreModulo, descripcion) VALUES (@nombreModulo, @descripcion); SELECT SCOPE_IDENTITY();", conex))
                    {
                        cmd.CommandType = CommandType.Text;

                        
                        cmd.Parameters.AddWithValue("@nombreModulo", oModulo.nombreModulo);
                        cmd.Parameters.AddWithValue("@descripcion", oModulo.descripcion);

                        
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarModulo: " + ex.Message);
                    throw; 
                }
                finally
                {
                    
                    conexion.MtdCerrarConexion();
                }

                return 0; 
            }
        }

        
        public DataTable MtdListarModulosRecientes(int cantidad)
        {
            DataTable dtModulos = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP (@cantidad) idModulo, nombreModulo, descripcion FROM modulo ORDER BY idModulo DESC", conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);

                        
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