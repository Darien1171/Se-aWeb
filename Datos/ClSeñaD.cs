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

        // Método para registrar una nueva seña
        public int MtdRegistrarSena(ClSeñaE oSena)
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    // Usar un comando SQL directo en lugar de un procedimiento almacenado
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO seña (nombreSeña, urlVideo, idTipoSeña) VALUES (@nombreSeña, @urlVideo, @idTipoSeña); SELECT SCOPE_IDENTITY();", conex))
                    {
                        cmd.CommandType = CommandType.Text;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@nombreSeña", oSena.nombreSeña);
                        cmd.Parameters.AddWithValue("@urlVideo", oSena.urlVideo);
                        cmd.Parameters.AddWithValue("@idTipoSeña", oSena.idTipoSeña);

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
                    System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarSena: " + ex.Message);
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

        // Método para listar señas recientes

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
                    // Usar consulta SQL directa que incluya información de tipo
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TOP (@cantidad) s.idSeña, s.nombreSeña, s.urlVideo, ts.tipo
                        FROM seña s
                        INNER JOIN TipoSeña ts ON s.idTipoSeña = ts.idTiposeña
                        ORDER BY s.idSeña DESC", conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);

                        // Ejecutar y llenar el DataTable
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

        // Método para obtener tipos de seña por módulo
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

                        // Ejecutar y llenar el DataTable
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