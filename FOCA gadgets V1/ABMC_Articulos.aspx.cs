using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FOCA_Negocio;
namespace FOCA_gadgets_V1
{
    public partial class ABMC_Articulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void cargarComboTipoArticulos()
        {
            ddlTipoArticulo.DataSource = GestorArticulos.ObtenerTiposArticulos();
            ddlTipoArticulo.DataTextField = "descripcion";
            ddlTipoArticulo.DataBind();
        }
    }
}