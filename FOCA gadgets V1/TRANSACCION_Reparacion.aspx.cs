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
        
        List<Problema> listaProblemas;

        protected void Page_Load(object sender, EventArgs e)
        {
            listaProblemas = GestorReparaciones.ObtenerProblemas();
            if (!Page.IsPostBack)
            {
                compareValidatorFechaDesde.ValueToCompare = DateTime.Now.ToShortDateString();
                txtFechaRepracion.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //agregado hoy
                txtFechaRepracion.ReadOnly = true;
                //
                cargarComboClientes();
                cargarComboEstados();
                FirstGridViewRow();
            }

            //actualizarPreciosYSubtotales();
            txtFechaRepracion.Text = DateTime.Now.ToString("dd/MM/yyyy");
            actualizarTotal();
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
                    TextBox txtPrecio = (TextBox)gvDetalleReparacion.Rows[rowIndex].Cells[1].FindControl("txtPrecio");
                                   
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = cboProblemas.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtPrecio.Text;
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
                        TextBox txtPrecio = (TextBox)gvDetalleReparacion.Rows[rowIndex].Cells[1].FindControl("txtPrecio");
                        
                        cboProblemas.SelectedValue = dt.Rows[i]["Col1"].ToString();

                        txtPrecio.Text = dt.Rows[i]["Col2"].ToString();
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
                
                cboProblemas.DataSource = listaProblemas;
                int count = listaProblemas.Count;
                cboProblemas.DataTextField = "descripcionProblema";
                cboProblemas.DataValueField = "idProblema";
                cboProblemas.DataBind();
                TextBox text = (e.Row.FindControl("txtPrecio") as TextBox);
                
                
            }
        }

        protected void gvDetalleReparacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    gvDetalleReparacion.DataSource = dt;
                    gvDetalleReparacion.DataBind();

                    SetPreviousData();
                    actualizarTotal();
                }
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (null == ViewState["CurrentTable"])
            {
                return;
            }
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList cboProblemas = (DropDownList)gvDetalleReparacion.Rows[rowIndex].Cells[0].FindControl("cboProblemas");
                    TextBox txtPrecio = (TextBox)gvDetalleReparacion.Rows[rowIndex].Cells[1].FindControl("txtPrecio");
                    
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = cboProblemas.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtPrecio.Text;
                    rowIndex++;
                }

                ViewState["CurrentTable"] = dtCurrentTable;          
            }            
        }

        private void actualizarTotal()
        {
            int rowIndex = 0;
            float total = 0;
            if (null == ViewState["CurrentTable"])
            {
                return;
            }
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    TextBox txtPrecio = (TextBox)gvDetalleReparacion.Rows[rowIndex].Cells[1].FindControl("txtPrecio");
                    if (txtPrecio.Text == "") txtPrecio.Text = "0";
                    total += float.Parse(txtPrecio.Text);
                    rowIndex++;
                }

                lblTotal.Text = total.ToString();            
            }
        }

        protected void Unnamed5_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    //MAESTRO
                    Reparacion rep = new Reparacion();                 
                                        
                    string contieneEstado = ddlEstados.SelectedValue;
                    DateTime fecha = DateTime.Parse(txtFechaRepracion.Text);
                    rep.fechaReparacion = fecha.ToString("yyyy-MM-dd");
                    fecha = DateTime.Parse(txtFechaRepracion.Text);
                    rep.fechaDevolucion = fecha.ToString("yyyy-MM-dd");
                    rep.descripcionReparacion = txtDescripcion.Text;
                    rep.equipo = txtEquipo.Text;
                    rep.estado = int.Parse(ddlEstados.SelectedValue);
                    rep.cliente = int.Parse(ddlClientes.SelectedValue);
                    rep.total = float.Parse(lblTotal.Text);

                    //DETALLE
                    List<DetalleReparacion> listaDetalles = new List<DetalleReparacion>();
                    int rowIndex = 0;

                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            DropDownList cboProblemas = (DropDownList)gvDetalleReparacion.Rows[rowIndex].Cells[0].FindControl("cboProblemas");
                            TextBox txtPrecio = (TextBox)gvDetalleReparacion.Rows[rowIndex].Cells[1].FindControl("txtPrecio");
                            DetalleReparacion detalle = new DetalleReparacion();
                            detalle.problema = int.Parse(cboProblemas.SelectedValue);
                            detalle.subTotal = float.Parse(txtPrecio.Text);

                            listaDetalles.Add(detalle);
                            rowIndex++;
                        }
                    }

                    GestorReparaciones.InsertarReparacion(rep, listaDetalles);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('La reparación se registró correctamente')", true);
                    limpiarCampos();
                }
                catch(Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('La reparación no se puedo registrar')", true);
                }

            }
        }

        private void limpiarCampos()
        {
            txtFechaDevolucion.Text = "";
            txtDescripcion.Text = "";
            txtEquipo.Text = "";            
        }



    }
}