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
                cargarComboClientes();
                cargarComboEstados();
            }
        }

        private void cargarComboEstados()
        {
            
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
            string contieneCliente = ddlCliente.SelectedValue;
            if (contieneCliente == "-1") { contieneCliente = ""; }
            string contieneFecha = txtFiltroFecha.Text;
            string contieneEstado = ddlEstado.SelectedValue;
            if (contieneEstado == "-1") { contieneEstado = ""; }

            dgvListadoReparaciones.DataSource = GestorListadoReparacion.obtenerReparaciones(contieneFecha, contieneCliente, contieneFecha);
            dgvListadoReparaciones.DataBind();

        }
    }
}