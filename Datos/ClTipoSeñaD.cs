using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using SeñaWeb.Entidad;

namespace SeñaWeb.Datos
{
    public class ClTipoSeñaD
    {
        public int MtdContarTiposSena()
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ContarTiposSeña", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        
        public int MtdRegistrarTipoSena(ClTipoSeñaE oTipoSena)
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO TipoSeña (tipo, descripcion, idModulo) VALUES (@tipo, @descripcion, @idModulo); SELECT SCOPE_IDENTITY();", conex))
                    {
                        cmd.CommandType = CommandType.Text;

                        
                        cmd.Parameters.AddWithValue("@tipo", oTipoSena.tipo);
                        cmd.Parameters.AddWithValue("@descripcion", oTipoSena.descripcion);
                        cmd.Parameters.AddWithValue("@idModulo", oTipoSena.idModulo);

                        
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarTipoSena: " + ex.Message);
                    throw; 
                }
                finally
                {
                    
                    conexion.MtdCerrarConexion();
                }

                return 0; 
            }
        }

        
        public DataTable MtdListarTiposSenaRecientes(int cantidad)
        {
            DataTable dtTiposSena = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TOP (@cantidad) ts.idTiposeña, ts.tipo, ts.descripcion, m.nombreModulo 
                        FROM TipoSeña ts
                        INNER JOIN modulo m ON ts.idModulo = m.idModulo
                        ORDER BY ts.idTiposeña DESC", conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);

                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtTiposSena);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdListarTiposSenaRecientes: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtTiposSena;
        }

        
        public DataTable MtdObtenerModulos()
        {
            DataTable dtModulos = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT idModulo, nombreModulo FROM modulo ORDER BY nombreModulo", conex))
                    {
                        cmd.CommandType = CommandType.Text;

                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtModulos);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdObtenerModulos: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtModulos;
        }

        
        public DataTable MtdObtenerTodosTiposSena()
        {
            DataTable dtTiposSena = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT ts.idTiposeña, ts.tipo, ts.descripcion, m.nombreModulo 
                        FROM TipoSeña ts
                        INNER JOIN modulo m ON ts.idModulo = m.idModulo
                        ORDER BY ts.tipo", conex))
                    {
                        cmd.CommandType = CommandType.Text;

                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtTiposSena);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdObtenerTodosTiposSena: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtTiposSena;
        }
    }
}