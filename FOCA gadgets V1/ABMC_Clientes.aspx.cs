using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FOCA_Entidades;
using FOCA_Negocio;


namespace FOCA_gadgets_V1
{
    public partial class ABMC_Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                cargarComboLocalidades();
        }

        private void cargarComboLocalidades()
        {
            ddlLocalidades.DataSource = GestorLocalidades.ObtenerTodas();
            ddlLocalidades.DataTextField = "Nombre";
            ddlLocalidades.DataBind();
        }

        protected void enviar(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {                    
                
            }

        }

        
    }
}