using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FOCA_Negocio;
using FOCA_Entidades;

namespace FOCA_gadgets_V1
{
    public partial class TRANSACCION_Venta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtFechaVenta.Text = DateTime.Today.ToShortDateString();
                //Page.DataBind();
                cargarComboClientes();
                cargarComboArticulos();
            }
        }

        private void cargarComboArticulos()
        {
            ddlTipoArticulo.DataSource = GestorArticulos.obtenerTiposArticulos();
            ddlTipoArticulo.DataTextField = "descripcion";
            ddlTipoArticulo.DataValueField = "indexBD";
            ddlTipoArticulo.DataBind();
            ddlTipoArticulo.Items.Insert(0, new ListItem("Todas las familias", "-1"));
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
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            {
                string contieneDescripcion = "%" + txtFiltroDescripcion.Text + "%";
                string orden = "Descripcion";
                if (ViewState["ordenGvArticulos"] != null)
                {
                    orden = ViewState["ordenGvArticulos"].ToString();
                }

                string contieneFamilia = ddlTipoArticulo.SelectedValue;
                if (contieneFamilia == "-1") { contieneFamilia = ""; }
                string disponible = "1"; 
                List<Articulo> listaArticulos = GestorArticulos.obtenerArticulos(contieneDescripcion, contieneFamilia, orden, disponible);
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.DataKeyNames = new string[] { "indexBD" };
                 
                dgvArticulos.DataBind();
                calcularTotalesyUnidadArticulo();

                


            }
        }

      

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void dgvArticulos_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void cargarArticulos_Click(object sender, EventArgs e)
        {
            panelArticulos.Visible = true ;
            txtFactura.ReadOnly = true;
            ddlClientes.Visible = false;
            lblCliente.Visible = false;
            btnCargarArticulos.Visible = false;
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int unidades = int.Parse(lblTotalUnidades.Text) + 1 ;
            float precio = float.Parse(lblTotalPesos.Text);
            int idArticulo = (int)dgvArticulos.SelectedDataKey.Value;
            Articulo a = GestorArticulos.obtenerArticulo(idArticulo);
            DetalleVenta dv = new DetalleVenta();
            if (Session["MiCarrito"] == null)
            {
                Session["MiCarrito"] = new List<DetalleVenta>();
            }

            List<DetalleVenta> listadetallesventa = (List<DetalleVenta>)Session["MiCarrito"];
            Boolean encontro = false;
            foreach (DetalleVenta item in listadetallesventa)
            {
                if (idArticulo == item.articulo)
                {
                    item.cantidad++;
                    encontro = true;
                    //precio += item.precioArticulo;
                    break;
                }

            }
            if (encontro == false)
            {
                //dv.nroFactura = int.Parse(txtFactura.Text);
                //el numero de factura se carga al guardar en la base
                dv.nroFactura = -1;
                dv.cantidad = 1;
                dv.articulo = a.indexBD;
                dv.precioArticulo = a.precio;
                dv.descripcionArticulo = a.descripcion;
                listadetallesventa.Add(dv);
                //precio += dv.precioArticulo;
            }

            //lblTotalUnidades.Text = unidades.ToString();
            //lblTotalPesos.Text = precio.ToString();
            calcularTotalesyUnidadArticulo();


        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (Session["MiCarrito"] != null)
            {
                List<DetalleVenta> listadetallesventa = (List<DetalleVenta>)Session["MiCarrito"];
                if (listadetallesventa.Count != 0)
                {
                    panelArticulos.Visible = false;
                    panelCarrito.Visible = true;
                    CargarGrillaCarrito();
                }
            }
        }

        private void CargarGrillaCarrito()
        {
            if (Session["MiCarrito"] == null) return;
            List<DetalleVenta> listadetallesventa = (List<DetalleVenta>)Session["MiCarrito"];

            dgvCarrito.DataSource = listadetallesventa;
            dgvCarrito.DataBind();

            calcularTotalesyUnidadCarrito();

        }

        private void calcularTotalesyUnidadArticulo()
        {
            if (Session["MiCarrito"] == null) return;
            List<DetalleVenta> listadetallesventa = (List<DetalleVenta>)Session["MiCarrito"];
            float total = 0;
            int unidades = 0;
            foreach (DetalleVenta item in listadetallesventa)
            {
                total += item.subTotal;
                unidades += item.cantidad;
            }
            lblTotalPesos.Text = total.ToString();
            lblTotalUnidades.Text = unidades.ToString();
            txtMonto.Text = total.ToString();
        }

        private void calcularTotalesyUnidadCarrito()
        {
            if (Session["MiCarrito"] == null) return;
            List<DetalleVenta> listadetallesventa = (List<DetalleVenta>)Session["MiCarrito"];
            float total = 0;
            int unidades = 0;
            foreach (DetalleVenta item in listadetallesventa)
            {
                
                total += item.subTotal;
                unidades += item.cantidad;
            }
            lbltotalPesosCarrito.Text = total.ToString();
            lbltotalUnidadesCarrito.Text = unidades.ToString();
            txtMonto.Text = total.ToString();
        }

        protected void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            if (Session["MiCarrito"] == null) return;

            List<DetalleVenta> listadetallesventa = (List<DetalleVenta>)Session["MiCarrito"];
            if (listadetallesventa.Count == 0) return;
            
            
                try
                {


                Venta v = new Venta();
                v.cliente = int.Parse(ddlClientes.SelectedValue);
                v.fecha = DateTime.Parse(txtFechaVenta.Text);
                v.monto = float.Parse(txtMonto.Text);
                //v.nroFactura = int.Parse(txtFactura.Text);
                //la factura se busca en el momento de la transaccion
                v.nroFactura = -1;

                GestorVentas.guardarVenta(v, listadetallesventa);




                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('La venta se registró correctamente')", true);

                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se pudo registrar la venta')", true);

                }
                finally
                {
                   
                    limpiarcampos();
                }
               

        }

        private void limpiarcampos()
        {
            panelArticulos.Visible = false;
            panelCarrito.Visible = false;
            btnCargarArticulos.Visible = true;
            lblTotalPesos.Text = "0,0";
            lbltotalPesosCarrito.Text = "0,0";
            lblTotalUnidades.Text = "0";
            lbltotalUnidadesCarrito.Text = "0";
            txtMonto.Text = "";
            ddlClientes.SelectedIndex = -1;
            ddlClientes.Visible = true;
            lblCliente.Visible = true;
            txtFactura.Text = "";
            txtFactura.ReadOnly = true;
            Session["MiCarrito"] = null;
            ddlTipoArticulo.SelectedIndex = -1;
            dgvArticulos.DataSource = new List<Articulo>();
            dgvArticulos.DataBind();
            dgvCarrito.DataSource = new List<DetalleVenta>();
            dgvCarrito.DataBind();




        }

        protected void dgvCarrito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           


        }

        protected void dgvCarrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int idArticulo = (int)dgvCarrito.SelectedDataKey.Value;
            if (Session["MiCarrito"] == null) return;
            List<DetalleVenta> listadetallesventa = (List<DetalleVenta>)Session["MiCarrito"];
            Boolean encontro = false;
            foreach (DetalleVenta item in listadetallesventa)
            {
                
                if (idArticulo == item.articulo)
                {
                    listadetallesventa.Remove(item);
                    calcularTotalesyUnidadCarrito();
                   
                    break;
                }

            }

            CargarGrillaCarrito();
        }

        protected void dgvCarrito_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnVolverAlCarrito_Click(object sender, EventArgs e)
        {
            panelCarrito.Visible = false;
            panelArticulos.Visible = true;
            CargarGrilla();
        }

        protected void ModificarDatos_Click(object sender, EventArgs e)
        {
            panelArticulos.Visible = false;
            panelCarrito.Visible = false;
            btnCargarArticulos.Visible = true;
            txtFactura.ReadOnly = true;
            ddlClientes.Visible = true;
            lblCliente.Visible = true;
            txtFechaVenta.Text = DateTime.Today.ToShortDateString();
            
        }
    }
}