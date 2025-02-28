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

                // Ejecutar el SP
                using (SqlCommand cmd = new SqlCommand("sp_Login", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", oSesionUsuario.Email);
                    cmd.Parameters.AddWithValue("@Password", oSesionUsuario.Contraseña);

                    // Ejecutar el SP y leer los resultados
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Verificar si se obtuvo un usuario válido
                            if (reader["idUsuario"] != DBNull.Value && reader["idRol"] != DBNull.Value)
                            {
                                oDatosUsuario.idUsuario = Convert.ToInt32(reader["idUsuario"]);
                                oDatosUsuario.idRol = Convert.ToInt32(reader["idRol"]);
                            }
                            else
                            {
                                // En caso de error, se establecen valores predeterminados
                                oDatosUsuario.idUsuario = 0;
                                oDatosUsuario.idRol = 0;
                            }
                        }
                        else
                        {
                            // Si no hay resultados, se establece un valor de error
                            oDatosUsuario.idUsuario = 0;
                            oDatosUsuario.idRol = 0;
                        }
                    }
                }

                conexion.MtdCerrarConexion();
                return oDatosUsuario;
            }
        }

    }
}