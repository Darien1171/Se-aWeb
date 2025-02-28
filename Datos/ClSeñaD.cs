using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

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
    }
}