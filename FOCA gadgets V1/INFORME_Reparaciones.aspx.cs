using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FOCA_Negocio;
namespace FOCA_gadgets_V1
{
    public partial class INFORME_Reparaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CompareValidatorFechaHasta.ValueToCompare = DateTime.Now.ToShortDateString();
                cargarComboClientes();
                cargarComboEstados();
                ddlInforme.SelectedIndex = 1;
            }
            
            
        }

        private void cargarComboEstados()
        {
            ddlEstados.DataSource = GestorReparaciones.ObtenerEstados();
            ddlEstados.DataTextField = "descripcionEstado";
            ddlEstados.DataValueField = "idEstado";
            ddlEstados.DataBind();
            ddlEstados.Items.Insert(0, new ListItem("Seleccionar un estado", "-1"));
        }

        private void cargarComboClientes()
        {
            ddlClientes.DataSource = GestorClientes.ObtenerClientesParaCombo();
            ddlClientes.DataTextField = "nombreyapellido";
            ddlClientes.DataValueField = "indexBD";
            ddlClientes.DataBind();
            ddlClientes.Items.Insert(0, new ListItem("Seleccionar un cliente", "-1"));
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            { 
            cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            string contieneCliente = ddlClientes.SelectedValue;
            if (contieneCliente == "-1") { contieneCliente = ""; }
            string contieneFechaDesde = "";
            string contieneFechaHasta = "";
            if (txtFiltroFechaDesde.Text != "")
            {
                DateTime fecha = DateTime.Parse(txtFiltroFechaDesde.Text);
                contieneFechaDesde = fecha.ToString("yyyy-MM-dd");
            }
            if (txtFiltroFechaHasta.Text != "")
            {
                DateTime fecha = DateTime.Parse(txtFiltroFechaHasta.Text);
                contieneFechaHasta = fecha.ToString("yyyy-MM-dd");
            }

            string contieneEstado = ddlEstados.SelectedValue;
            if (contieneEstado == "-1") { contieneEstado = ""; }

            dgvListadoReparaciones.DataSource = GestorListadoReparacion.obtenerReparaciones(contieneFechaDesde, contieneFechaHasta, contieneCliente, contieneEstado);
            dgvListadoReparaciones.DataBind();

        }

        protected void ddlInforme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInforme.SelectedItem.Text == "Informe de ventas")
            {
                Response.Redirect("INFORME_Ventas.aspx");
            }
            else
            {
                Response.Redirect("INFORME_Reparaciones.aspx");
            }
        }

        protected void dgvListadoReparaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvListadoReparaciones.PageIndex = e.NewPageIndex;
            cargarGrilla();            
        }
    }
}