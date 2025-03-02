using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using SeñaWeb.Entidad;

namespace SeñaWeb.Datos
{
    public class ClProgresoD
    {
        // Método para registrar o actualizar el progreso de un usuario en una seña
        public int MtdRegistrarProgreso(ClProgresoE oProgreso)
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    // Verificar si ya existe un registro de progreso para este usuario y seña
                    using (SqlCommand cmdCheck = new SqlCommand("SELECT idProgreso FROM progreso WHERE idUsuario = @idUsuario AND idSeña = @idSeña", conex))
                    {
                        cmdCheck.CommandType = CommandType.Text;
                        cmdCheck.Parameters.AddWithValue("@idUsuario", oProgreso.idUsuario);
                        cmdCheck.Parameters.AddWithValue("@idSeña", oProgreso.idSeña);

                        object existingId = cmdCheck.ExecuteScalar();

                        // Si ya existe, actualizar el registro
                        if (existingId != null && existingId != DBNull.Value)
                        {
                            int idProgreso = Convert.ToInt32(existingId);
                            using (SqlCommand cmdUpdate = new SqlCommand("UPDATE progreso SET estado = @estado WHERE idProgreso = @idProgreso", conex))
                            {
                                cmdUpdate.CommandType = CommandType.Text;
                                cmdUpdate.Parameters.AddWithValue("@estado", oProgreso.estado);
                                cmdUpdate.Parameters.AddWithValue("@idProgreso", idProgreso);

                                cmdUpdate.ExecuteNonQuery();
                                return idProgreso;
                            }
                        }
                        // Si no existe, crear un nuevo registro
                        else
                        {
                            using (SqlCommand cmdInsert = new SqlCommand("INSERT INTO progreso (idUsuario, idSeña, estado) VALUES (@idUsuario, @idSeña, @estado); SELECT SCOPE_IDENTITY();", conex))
                            {
                                cmdInsert.CommandType = CommandType.Text;

                                cmdInsert.Parameters.AddWithValue("@idUsuario", oProgreso.idUsuario);
                                cmdInsert.Parameters.AddWithValue("@idSeña", oProgreso.idSeña);
                                cmdInsert.Parameters.AddWithValue("@estado", oProgreso.estado);

                                // Ejecutar y obtener el ID insertado
                                object result = cmdInsert.ExecuteScalar();
                                if (result != null && result != DBNull.Value)
                                {
                                    return Convert.ToInt32(result);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarProgreso: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }

                return 0; // Si no se pudo insertar/actualizar
            }
        }

        // Método para obtener el progreso de un usuario
        public DataTable MtdObtenerProgresoUsuario(int idUsuario)
        {
            DataTable dtProgreso = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    string query = @"
                        SELECT 
                            p.idProgreso,
                            p.idUsuario,
                            p.idSeña,
                            p.estado,
                            s.nombreSeña,
                            ts.tipo as tipoSeña,
                            m.nombreModulo,
                            GETDATE() as fechaVisto
                        FROM 
                            progreso p
                        INNER JOIN 
                            seña s ON p.idSeña = s.idSeña
                        INNER JOIN 
                            TipoSeña ts ON s.idTipoSeña = ts.idTiposeña
                        INNER JOIN 
                            modulo m ON ts.idModulo = m.idModulo
                        WHERE 
                            p.idUsuario = @idUsuario
                        ORDER BY 
                            p.idProgreso DESC
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtProgreso);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdObtenerProgresoUsuario: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtProgreso;
        }

        // Método para obtener el progreso de un usuario en un módulo específico
        public DataTable MtdObtenerProgresoModulo(int idUsuario, int idModulo)
        {
            DataTable dtProgresoModulo = new DataTable();
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
                            ts.tipo as tipoSeña,
                            ts.descripcion as descripcionTipo,
                            ISNULL(p.estado, 0) as estado
                        FROM 
                            seña s
                        INNER JOIN 
                            TipoSeña ts ON s.idTipoSeña = ts.idTiposeña
                        LEFT JOIN 
                            progreso p ON s.idSeña = p.idSeña AND p.idUsuario = @idUsuario
                        WHERE 
                            ts.idModulo = @idModulo
                        ORDER BY 
                            ts.tipo, s.nombreSeña
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@idModulo", idModulo);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtProgresoModulo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdObtenerProgresoModulo: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtProgresoModulo;
        }

        // Método para obtener el número de señas vistas por el usuario
        public int MtdContarSenasVistas(int idUsuario)
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM progreso WHERE idUsuario = @idUsuario AND estado = 1", conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdContarSenasVistas: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }

                return 0;
            }
        }

        // Método para obtener módulos con porcentaje de completado
        public DataTable MtdObtenerModulosConProgreso(int idUsuario)
        {
            DataTable dtModulos = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    string query = @"
                        SELECT DISTINCT 
                            m.idModulo, 
                            m.nombreModulo,
                            (
                                SELECT COUNT(p.idSeña) * 100.0 / NULLIF(COUNT(s.idSeña), 0)
                                FROM seña s
                                LEFT JOIN progreso p ON s.idSeña = p.idSeña AND p.idUsuario = @idUsuario AND p.estado = 1
                                INNER JOIN TipoSeña ts ON s.idTipoSeña = ts.idTiposeña
                                WHERE ts.idModulo = m.idModulo
                            ) as porcentajeCompletado,
                            MAX(p.idProgreso) as ultimoProgreso
                        FROM 
                            modulo m
                        INNER JOIN 
                            TipoSeña ts ON m.idModulo = ts.idModulo
                        INNER JOIN 
                            seña s ON ts.idTiposeña = s.idTipoSeña
                        LEFT JOIN 
                            progreso p ON s.idSeña = p.idSeña AND p.idUsuario = @idUsuario
                        GROUP BY 
                            m.idModulo, m.nombreModulo
                        ORDER BY 
                            ultimoProgreso DESC
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtModulos);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdObtenerModulosConProgreso: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtModulos;
        }

        // Método para obtener módulos completados
        public DataTable MtdObtenerModulosCompletados(int idUsuario)
        {
            DataTable dtModulosCompletados = new DataTable();
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    string query = @"
                        SELECT DISTINCT m.idModulo, m.nombreModulo 
                        FROM modulo m
                        WHERE (
                            SELECT COUNT(s.idSeña) 
                            FROM seña s
                            INNER JOIN TipoSeña ts ON s.idTipoSeña = ts.idTiposeña
                            WHERE ts.idModulo = m.idModulo
                        ) > 0
                        AND (
                            SELECT COUNT(s.idSeña) 
                            FROM seña s
                            INNER JOIN TipoSeña ts ON s.idTipoSeña = ts.idTiposeña
                            WHERE ts.idModulo = m.idModulo
                        ) = (
                            SELECT COUNT(p.idSeña) 
                            FROM progreso p
                            INNER JOIN seña s ON p.idSeña = s.idSeña
                            INNER JOIN TipoSeña ts ON s.idTipoSeña = ts.idTiposeña
                            WHERE ts.idModulo = m.idModulo
                            AND p.idUsuario = @idUsuario
                            AND p.estado = 1
                        )
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtModulosCompletados);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdObtenerModulosCompletados: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return dtModulosCompletados;
        }
    }
}