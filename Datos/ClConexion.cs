using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SeñaWeb.Datos
{
    public class ClConexion
    {
        SqlConnection conex = null;

        public ClConexion()
        {
            conex = new SqlConnection("Data Source=.;Initial Catalog=dbSeñas11;Integrated Security=True;");
        }

        public SqlConnection MtdAbrirConexion()
        {
            conex.Open();
            return conex;
        }

        public SqlConnection MtdCerrarConexion()
        {
            conex.Close();
            return conex;
        }
    }
}