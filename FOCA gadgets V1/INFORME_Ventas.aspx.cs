﻿using System;
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
            //int? IdArticuloFamilia = (ddlFamilia.SelectedValue == "") ? (int?)null : int.Parse(ddlFamilia.SelectedValue);
            string contieneMonto = txtFiltroMonto.Text;
            string contieneFecha = txtFiltroFecha.Text;
            string contieneCliente = ddlCliente.SelectedValue;
            if (contieneCliente == "-1") { contieneCliente = ""; }
            
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
            cargarGrilla();
        }

        protected void dgvVentass_Sorting(object sender, GridViewSortEventArgs e)
        {
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
    }
}