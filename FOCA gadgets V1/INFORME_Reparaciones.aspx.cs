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
            cargarGrilla();
        }

        private void cargarGrilla()
        {
            string contieneCliente = ddlClientes.SelectedValue;
            if (contieneCliente == "-1") { contieneCliente = ""; }
            string contieneFecha = "";
            if (txtFiltroFecha.Text != "")
            {
                DateTime fecha = DateTime.Parse(txtFiltroFecha.Text);
                contieneFecha = fecha.ToString("yyyy-MM-dd");
            }
            string contieneEstado = ddlEstados.SelectedValue;
            if (contieneEstado == "-1") { contieneEstado = ""; }

            dgvListadoReparaciones.DataSource = GestorListadoReparacion.obtenerReparaciones(contieneFecha, contieneCliente, contieneEstado);
            dgvListadoReparaciones.DataBind();

        }
    }
}