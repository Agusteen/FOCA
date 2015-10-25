using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FOCA_Negocio;
using System.Data;
using FOCA_Entidades;


namespace FOCA_gadgets_V1
{
    public partial class TRANSACCION_Reparacion : System.Web.UI.Page
    {
        
        //List<Problema> listaProblemas;

        protected void Page_Load(object sender, EventArgs e)
        {
            //listaProblemas = GestorReparaciones.ObtenerProblemas();
            if (!Page.IsPostBack)
            {
                txtFechaRepracion.Text = DateTime.Now.ToString("dd/MM/yyyy");
                cargarComboClientes();
                cargarComboEstados();
                FirstGridViewRow();
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));            
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = string.Empty;
            dr["Col2"] = string.Empty;            
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            gvDetalleReparacion.DataSource = dt;
            gvDetalleReparacion.DataBind();
        }
        
        

        private void cargarComboClientes()
        {
            ddlClientes.DataSource = GestorClientes.ObtenerClientesParaCombo();
            ddlClientes.DataTextField = "nombreyapellido";
            ddlClientes.DataValueField = "indexBD";
            ddlClientes.DataBind();
        }

        private void cargarComboEstados()
        {
            ddlEstados.DataSource = GestorReparaciones.ObtenerEstados();
            ddlEstados.DataTextField = "descripcionEstado";
            ddlEstados.DataValueField = "idEstado";
            ddlEstados.DataBind();
        }

        protected void cmdAgregarFila_Click(object sender, EventArgs e)
        {
            if (null == ViewState["CurrentTable"])
            {
                return;
            }
            int rowIndex = 0;

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList cboProblemas = (DropDownList)gvDetalleReparacion.Rows[rowIndex].Cells[0].FindControl("cboProblemas");
                    Label lblDuracion = (Label)gvDetalleReparacion.Rows[rowIndex].Cells[1].FindControl("lblDuracion");
                     
                    
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = cboProblemas.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = lblDuracion.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                gvDetalleReparacion.DataSource = dtCurrentTable;
                gvDetalleReparacion.DataBind();
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList cboProblemas = (DropDownList)gvDetalleReparacion.Rows[rowIndex].Cells[0].FindControl("cboProblemas");
                        TextBox lblDuracion = (TextBox)gvDetalleReparacion.Rows[rowIndex].Cells[1].FindControl("lblDuracion");


                        cboProblemas.SelectedValue = dt.Rows[i]["Col1"].ToString();

                        lblDuracion.Text = dt.Rows[i]["Col2"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void gvDetalleReparacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList cboProblemas = (e.Row.FindControl("cboProblemas") as DropDownList);
                cboProblemas.DataSource = GestorReparaciones.ObtenerProblemas();
                //int count = listaProblemas.Count;
                cboProblemas.DataTextField = "descripcionProblema";
                cboProblemas.DataValueField = "idProblema";
                cboProblemas.DataBind();
            }
        }
    }
}