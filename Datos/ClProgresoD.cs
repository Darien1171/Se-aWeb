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
        CAST(p.estado AS BIT) as estado,
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
                    ts.idTipoSeña,
                    ts.tipo as tipoSeña,
                    ts.descripcion as descripcionTipo,
                    CAST(ISNULL(p.estado, 0) AS BIT) as estado
                FROM 
                    TipoSeña ts 
                INNER JOIN 
                    modulo m ON ts.idModulo = m.idModulo
                LEFT JOIN 
                    seña s ON ts.idTiposeña = s.idTipoSeña
                LEFT JOIN 
                    progreso p ON s.idSeña = p.idSeña AND p.idUsuario = @idUsuario
                WHERE 
                    m.idModulo = @idModulo
                    AND s.idSeña IS NOT NULL
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

                    // Agregar depuración
                    System.Diagnostics.Debug.WriteLine($"Se encontraron {dtProgresoModulo.Rows.Count} señas para el módulo {idModulo}");
                    foreach (DataRow row in dtProgresoModulo.Rows)
                    {
                        System.Diagnostics.Debug.WriteLine($"Seña: {row["idSeña"]} - {row["nombreSeña"]} - Tipo: {row["tipoSeña"]}");
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
                    // Consulta modificada para asegurar que se muestren todos los módulos
                    string query = @"
                SELECT 
                    m.idModulo, 
                    m.nombreModulo,
                    ISNULL(
                        (SELECT COUNT(p.idSeña) * 100.0 / NULLIF(COUNT(s.idSeña), 0)
                         FROM TipoSeña ts 
                         LEFT JOIN seña s ON ts.idTiposeña = s.idTipoSeña
                         LEFT JOIN progreso p ON s.idSeña = p.idSeña AND p.idUsuario = @idUsuario AND p.estado = 1
                         WHERE ts.idModulo = m.idModulo
                         GROUP BY ts.idModulo),
                    0) as porcentajeCompletado,
                    (SELECT MAX(p2.idProgreso) 
                     FROM progreso p2 
                     INNER JOIN seña s2 ON p2.idSeña = s2.idSeña
                     INNER JOIN TipoSeña ts2 ON s2.idTipoSeña = ts2.idTiposeña
                     WHERE ts2.idModulo = m.idModulo AND p2.idUsuario = @idUsuario) as ultimoProgreso
                FROM 
                    modulo m
                ORDER BY 
                    CASE WHEN (SELECT MAX(p3.idProgreso) 
                               FROM progreso p3 
                               INNER JOIN seña s3 ON p3.idSeña = s3.idSeña
                               INNER JOIN TipoSeña ts3 ON s3.idTipoSeña = ts3.idTiposeña
                               WHERE ts3.idModulo = m.idModulo AND p3.idUsuario = @idUsuario) IS NULL THEN 2 ELSE 1 END,
                    (SELECT MAX(p3.idProgreso) 
                     FROM progreso p3 
                     INNER JOIN seña s3 ON p3.idSeña = s3.idSeña
                     INNER JOIN TipoSeña ts3 ON s3.idTipoSeña = ts3.idTiposeña
                     WHERE ts3.idModulo = m.idModulo AND p3.idUsuario = @idUsuario) DESC
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