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

            if (!Page.IsPostBack)
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

                    String s = ddlTipoArticulo.SelectedValue;
                    Articulo art = new Articulo()
                    {
                        descripcion = txtDescripcion.Text.ToUpper(),
                        precio = float.Parse(txtPrecio.Text),
                        stock = int.Parse(txtStock.Text),
                        disponible = ckbDisponible.Checked,
                        tipoArticulo = int.Parse(ddlTipoArticulo.SelectedValue)


                   
                        };
                        GestorArticulos.insertarArticulo(art);
                        CargarGrilla();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('EXITO')", true);

                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ERROR')", true);

                }
                finally
                {
                    CargarGrilla();
                }

            }


        }

       
        protected void dgvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            {
                   String commandName = e.CommandName.ToUpper();
                int index = Convert.ToInt32(e.CommandArgument);
               
                if (commandName.Equals("MODIFICAR"))
                    {

                    }

                    if (commandName.Equals("ELIMINAR"))
                    {
                    try
                    {
                      
                            GridViewRow row = dgvArticulos.Rows[index];
                            int indexBD = int.Parse(row.Cells[2].Text);
                        
                        GestorArticulos.eliminarArticulo(indexBD);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('EXITO')", true);
                   
                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ERROR')", true);

                        }
                        finally
                        {
                            CargarGrilla();
                        }
                    }
                }
            }
        }
    }
