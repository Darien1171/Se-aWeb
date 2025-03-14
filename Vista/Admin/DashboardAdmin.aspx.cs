using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeñaWeb.Logica;

namespace SeñaWeb.Vista.Admin
{
    public partial class DashboardAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarContadores();
            }
        }

        private void CargarContadores()
        {
            
            ClModuloL logicaModulo = new ClModuloL();
            int modulosCount = logicaModulo.MtdContarModulos();
            lblModulosCount.Text = modulosCount.ToString();

            
            ClTipoSeñaL logicaTipoSena = new ClTipoSeñaL();
            int tiposSenaCount = logicaTipoSena.MtdContarTiposSena();
            lblTiposSenaCount.Text = tiposSenaCount.ToString();

            
            ClSeñaL logicaSena = new ClSeñaL();
            int senasCount = logicaSena.MtdContarSenas();
            lblSenasCount.Text = senasCount.ToString();
        }
    }
}