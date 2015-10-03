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
            cargarComboTipoArticulos();
        }
        private void cargarComboTipoArticulos()
        {
            ddlTipoArticulo.DataSource = GestorArticulos.obtenerTiposArticulos();
            ddlTipoArticulo.DataTextField = "descripcion";
            ddlTipoArticulo.DataBind();
        }

        protected void guardarArticulo(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                if (Page.IsValid)
                {
                    try
                    {
                        Articulo art = new Articulo()
                        {
                            descripcion = txtDescripcion.Text,
                            precio = float.Parse(txtPrecio.Text),
                            stock = int.Parse(txtStock.Text),
                            disponible = ckbDisponible.Checked,
                            tipoArticulo = ddlTipoArticulo.SelectedIndex

                        };
                        GestorArticulos.insertarArticulo(art);
                    }
                    catch
                    {

                    }

                }
               
            }
        }
    }
}