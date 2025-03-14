using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using SeñaWeb.Entidad;

namespace SeñaWeb.Datos
{
    public class ClSeñaD
    {
        public int MtdContarSenas()
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_ContarSeñas", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        
        public int MtdRegistrarSena(ClSeñaE oSena)
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO seña (nombreSeña, urlVideo, idTipoSeña) VALUES (@nombreSeña, @urlVideo, @idTipoSeña); SELECT SCOPE_IDENTITY();", conex))
                    {
                        cmd.CommandType = CommandType.Text;

                        
                        cmd.Parameters.AddWithValue("@nombreSeña", oSena.nombreSeña);
                        cmd.Parameters.AddWithValue("@urlVideo", oSena.urlVideo);
                        cmd.Parameters.AddWithValue("@idTipoSeña", oSena.idTipoSeña);

                        
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarSena: " + ex.Message);
                    throw; 
                }
                finally
                {
                    
                    conexion.MtdCerrarConexion();
                }

                return 0; 
            }
        }

        

        public DataTable MtdObtenerDetalleSena(int idSena, int idUsuario)
        {
            DataTable dtSena = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    string query = @"
                SELECT 
                    s.idSeña,
                    s.nombreSeña,
                    s.urlVideo,
                    ts.idTipoSeña,
                    ts.tipo as tipoSeña,
                    ts.descripcion as descripcionTipo,
                    m.idModulo,
                    m.nombreModulo,
                    m.descripcion as descripcionModulo,
                    CAST(ISNULL(p.estado, 0) AS BIT) as estado
                FROM 
                    seña s
                INNER JOIN 
                    TipoSeña ts ON s.idTipoSeña = ts.idTiposeña
                INNER JOIN 
                    modulo m ON ts.idModulo = m.idModulo
                LEFT JOIN 
                    progreso p ON s.idSeña = p.idSeña AND p.idUsuario = @idUsuario
                WHERE 
                    s.idSeña = @idSena";

                    using (SqlCommand cmd = new SqlCommand(query, conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idSena", idSena);
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtSena);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdObtenerDetalleSena: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtSena;
        }
        public DataTable MtdListarSenasRecientes(int cantidad)
        {
            DataTable dtSenas = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TOP (@cantidad) s.idSeña, s.nombreSeña, s.urlVideo, ts.tipo
                        FROM seña s
                        INNER JOIN TipoSeña ts ON s.idTipoSeña = ts.idTiposeña
                        ORDER BY s.idSeña DESC", conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);

                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtSenas);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdListarSenasRecientes: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtSenas;
        }

        
        public DataTable MtdObtenerTiposSenaPorModulo(int idModulo)
        {
            DataTable dtTiposSena = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT idTiposeña, tipo FROM TipoSeña WHERE idModulo = @idModulo ORDER BY tipo", conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idModulo", idModulo);

                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtTiposSena);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdObtenerTiposSenaPorModulo: " + ex.Message);
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