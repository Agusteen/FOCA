using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FOCA_gadgets_V1
{
    public partial class FOCAMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            { 
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                btnLogin.Visible = false;
                lblUser.Visible = true;
                btnCerrar.Visible = true;
                lblUser.Text = Page.User.Identity.Name;
                
                
            }
            else
            {
                btnLogin.Visible = true;
                lblUser.Visible = false;
                btnCerrar.Visible = false;
            }
            mnuArticulos.Visible = Page.User.IsInRole("Administrador") || Page.User.IsInRole("Vendedor");
            mnuClientes.Visible = Page.User.IsInRole("Administrador") || Page.User.IsInRole("Vendedor");
            mnuReparacion.Visible = Page.User.IsInRole("Administrador") || Page.User.IsInRole("Técnico");
            mnuVenta.Visible = Page.User.IsInRole("Administrador") || Page.User.IsInRole("Vendedor");
            mnuInformes.Visible = Page.User.IsInRole("Administrador") || Page.User.IsInRole("Vendedor") || Page.User.IsInRole("Tecnico");
            
                
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("Inicio.aspx");
        }

    }
}