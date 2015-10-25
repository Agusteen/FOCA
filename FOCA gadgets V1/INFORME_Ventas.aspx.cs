using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FOCA_Negocio;
namespace FOCA_gadgets_V1
{
    public partial class INFORME_Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                cargarComboClientes();
                
            }
        }

        private void cargarComboClientes()
        {
            ddlCliente.DataSource = GestorListadoVenta.ObtenerClientes();
            ddlCliente.DataTextField = "nombreyapellido";
            ddlCliente.DataValueField = "indexBD";
            ddlCliente.Items.Insert(0, new ListItem("Seleccionar un cliente", "-1"));
            ddlCliente.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            //int? IdArticuloFamilia = (ddlFamilia.SelectedValue == "") ? (int?)null : int.Parse(ddlFamilia.SelectedValue);
            string contieneMonto = txtFiltroMonto.Text;
            string contieneFecha = txtFiltroFecha.Text;
            string contieneCliente = ddlCliente.SelectedValue;
            if (contieneCliente == "-1") { contieneCliente = ""}
            
            //string orden = "Nombre, Apellido";
            //if (ViewState["ordenGvArticulos"] != null)
            //{
            //    orden = ViewState["ordenGvArticulos"].ToString();
            //}
            dgvVentas.DataSource = GestorListadoVenta.obtenerVentas(contieneMonto, contieneFecha, contieneCliente);
            dgvVentas.DataBind();
          
        }

        protected void dgvVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvVentas.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void dgvVentass_Sorting(object sender, GridViewSortEventArgs e)
        {
            CargarGrilla();
        }
    }
}