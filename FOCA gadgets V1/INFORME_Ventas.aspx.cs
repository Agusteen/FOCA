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
                ViewState["OrdenGvVentas"] = "Apellido";
                compareValidatorFechaHasta.ValueToCompare = DateTime.Now.ToShortDateString();
                compareValidatorFechaDesde.ValueToCompare = DateTime.Now.ToShortDateString();
            }
            
        }

        private void cargarComboClientes()
        {
            ddlCliente.DataSource = GestorClientes.ObtenerClientesParaCombo();
            ddlCliente.DataTextField = "nombreyapellido";
            ddlCliente.DataValueField = "indexBD";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem("Seleccionar un cliente", "-1"));
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        private void cargarGrilla()
        {
            string orden = "Apellido";
            if (ViewState["OrdenGvVentas"] != null)
            {
                orden = ViewState["OrdenGvVentas"].ToString();
            }


            //int? IdArticuloFamilia = (ddlFamilia.SelectedValue == "") ? (int?)null : int.Parse(ddlFamilia.SelectedValue);
            string contieneMonto = txtFiltroMonto.Text;            

            string contieneFechaDesde = "";
            string contieneFechaHasta = "";
            if (txtFiltroFecha.Text != "")
            {
                DateTime fecha = DateTime.Parse(txtFiltroFecha.Text);
                contieneFechaDesde = fecha.ToString("yyyy-MM-dd");
            }
            if (txtFechaHasta.Text != "")
            {
                DateTime fecha = DateTime.Parse(txtFechaHasta.Text);
                contieneFechaHasta = fecha.ToString("yyyy-MM-dd");
            }

            if (txtFiltroFecha.Text != "" & txtFechaHasta.Text == "")
            {
                contieneFechaHasta = DateTime.Now.ToString("yyyy-MM-dd"); ;
            }

            if (txtFiltroFecha.Text =="" & txtFechaHasta.Text != "")
            {
                contieneFechaDesde = "2015-01-01";
            }

            string contieneCliente = ddlCliente.SelectedValue;
            if (contieneCliente == "-1") { contieneCliente = ""; }
            
            //string orden = "Nombre, Apellido";
            //if (ViewState["ordenGvArticulos"] != null)
            //{
            //    orden = ViewState["ordenGvArticulos"].ToString();
            //}
            dgvVentas.DataSource = GestorListadoVenta.obtenerVentas(contieneMonto, contieneFechaDesde, contieneFechaHasta, contieneCliente, orden);
            dgvVentas.DataBind();
          
        }

        protected void dgvVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvVentas.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

        protected void dgvVentass_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["OrdenGvVentas"] = e.SortExpression;
            cargarGrilla();
        }

        protected void ddlInforme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInforme.SelectedItem.Value == "Informe de ventas")
            {
                Response.Redirect("INFORME_Ventas.aspx");
            }
            else
            {
                Response.Redirect("INFORME_Reparaciones.aspx");
            }
                
        }

        protected void dgvVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idVentas = (int)dgvVentas.SelectedDataKey.Value;
            dgvDetalleVentas.DataSource = GestorListadoVenta.obtenerDetalles(idVentas);
            dgvDetalleVentas.DataBind();
        }
    }
}