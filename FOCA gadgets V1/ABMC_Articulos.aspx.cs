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
    public partial class ABMC_Articulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!Page.IsPostBack)
            {
                cargarComboTipoArticulos();
                CargarGrilla();
            }
        }
        private void CargarGrilla()
        {
             dgvArticulos.DataSource = GestorArticulos.obtenerArticulos();
            dgvArticulos.DataBind();
           
        }


        private void cargarComboTipoArticulos()
        {
            ddlTipoArticulo.DataSource = GestorArticulos.obtenerTiposArticulos();
            ddlTipoArticulo.DataTextField = "descripcion";
            ddlTipoArticulo.DataValueField = "indexBD";
            ddlTipoArticulo.DataBind();
        }

        protected void guardarArticulo(object sender, EventArgs e)
        {
           
                if (Page.IsValid)
                {
                    try
                    {

                    String s =ddlTipoArticulo.SelectedValue;
                    Articulo art = new Articulo()
                    {
                        descripcion = txtDescripcion.Text,
                        precio = float.Parse(txtPrecio.Text),
                        stock = int.Parse(txtStock.Text),
                        disponible = ckbDisponible.Checked,
                        tipoArticulo = int.Parse( ddlTipoArticulo.SelectedValue)

                        };
                        GestorArticulos.insertarArticulo(art);
                        CargarGrilla();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('EXITO')", true);

                }
                catch
                    {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ERROR')", true);

                }

            }
               
            
        }
    }
}