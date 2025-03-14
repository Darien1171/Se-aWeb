using SeñaWeb.Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SeñaWeb.Datos
{
    public class ClUsuarioD
    {
        public ClUsuarioE loginUsuario(ClUsuarioE oSesionUsuario)
        {
            ClConexion conexion = new ClConexion();
            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                ClUsuarioE oDatosUsuario = new ClUsuarioE();

                
                using (SqlCommand cmd = new SqlCommand("sp_Login", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", oSesionUsuario.Email);
                    cmd.Parameters.AddWithValue("@Password", oSesionUsuario.Contraseña);

                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            if (reader["idUsuario"] != DBNull.Value && reader["idRol"] != DBNull.Value)
                            {
                                oDatosUsuario.idUsuario = Convert.ToInt32(reader["idUsuario"]);
                                oDatosUsuario.idRol = Convert.ToInt32(reader["idRol"]);
                            }
                            else
                            {
                                
                                oDatosUsuario.idUsuario = 0;
                                oDatosUsuario.idRol = 0;
                            }
                        }
                        else
                        {
                            
                            oDatosUsuario.idUsuario = 0;
                            oDatosUsuario.idRol = 0;
                        }
                    }
                }

                conexion.MtdCerrarConexion();
                return oDatosUsuario;
            }
        }


        
        public ClUsuarioE MtdObtenerUsuarioPorId(int idUsuario)
        {
            ClUsuarioE oUsuario = null;
            ClConexion conexion = new ClConexion();

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    string query = @"
                SELECT 
                    u.idUsuario,
                    u.Nombre,
                    u.Apellido,
                    u.Email,
                    u.idRol,
                    r.descripcion as RolDescripcion
                FROM 
                    usuario u
                INNER JOIN 
                    rol r ON u.idRol = r.idRol
                WHERE 
                    u.idUsuario = @idUsuario";

                    using (SqlCommand cmd = new SqlCommand(query, conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                oUsuario = new ClUsuarioE
                                {
                                    idUsuario = Convert.ToInt32(reader["idUsuario"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    idRol = Convert.ToInt32(reader["idRol"])
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdObtenerUsuarioPorId: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return oUsuario;
        }

        
        public bool MtdActualizarUsuario(ClUsuarioE oUsuario)
        {
            ClConexion conexion = new ClConexion();
            bool resultado = false;

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    string query = @"
                UPDATE usuario 
                SET 
                    Nombre = @Nombre,
                    Apellido = @Apellido,
                    Email = @Email
                WHERE 
                    idUsuario = @idUsuario";

                    using (SqlCommand cmd = new SqlCommand(query, conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Nombre", oUsuario.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", oUsuario.Apellido);
                        cmd.Parameters.AddWithValue("@Email", oUsuario.Email);
                        cmd.Parameters.AddWithValue("@idUsuario", oUsuario.idUsuario);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        resultado = (filasAfectadas > 0);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdActualizarUsuario: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return resultado;
        }

        
        public bool MtdCambiarContrasena(int idUsuario, string contrasenaActual, string nuevaContrasena)
        {
            ClConexion conexion = new ClConexion();
            bool resultado = false;

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    
                    string queryVerificar = "SELECT COUNT(*) FROM usuario WHERE idUsuario = @idUsuario AND Contraseña = @contrasenaActual";
                    using (SqlCommand cmdVerificar = new SqlCommand(queryVerificar, conex))
                    {
                        cmdVerificar.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmdVerificar.Parameters.AddWithValue("@contrasenaActual", contrasenaActual);

                        int count = Convert.ToInt32(cmdVerificar.ExecuteScalar());
                        if (count == 0)
                        {
                            return false; 
                        }
                    }

                    
                    string queryActualizar = "UPDATE usuario SET Contraseña = @nuevaContrasena WHERE idUsuario = @idUsuario";
                    using (SqlCommand cmdActualizar = new SqlCommand(queryActualizar, conex))
                    {
                        cmdActualizar.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmdActualizar.Parameters.AddWithValue("@nuevaContrasena", nuevaContrasena);

                        int filasAfectadas = cmdActualizar.ExecuteNonQuery();
                        resultado = (filasAfectadas > 0);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdCambiarContrasena: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return resultado;
        }


        

        
        public bool MtdVerificarEmailExistente(string email)
        {
            ClConexion conexion = new ClConexion();
            bool existe = false;

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM usuario WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", email);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        existe = (count > 0);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdVerificarEmailExistente: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return existe;
        }

        
        public int MtdRegistrarUsuario(ClUsuarioE oUsuario)
        {
            ClConexion conexion = new ClConexion();
            int idUsuarioRegistrado = 0;

            using (SqlConnection conex = conexion.MtdAbrirConexion())
            {
                try
                {
                    
                    string query = @"INSERT INTO usuario (Nombre, Apellido, Email, Password, idRol) 
                            VALUES (@Nombre, @Apellido, @Email, @Password, @idRol); 
                            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, conex))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Nombre", oUsuario.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", oUsuario.Apellido);
                        cmd.Parameters.AddWithValue("@Email", oUsuario.Email);
                        cmd.Parameters.AddWithValue("@Password", oUsuario.Contraseña); 
                        cmd.Parameters.AddWithValue("@idRol", oUsuario.idRol);

                        
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            idUsuarioRegistrado = Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error en MtdRegistrarUsuario: " + ex.Message);
                    throw;
                }
                finally
                {
                    conexion.MtdCerrarConexion();
                }
            }

            return idUsuarioRegistrado;
        }




    }
}