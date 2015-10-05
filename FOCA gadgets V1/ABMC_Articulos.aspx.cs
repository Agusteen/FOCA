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
                ViewState["esmodificacion"] = false;
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
                if (ViewState["esModificacion"] != null)
                {
                    
                    if (((Boolean)ViewState["esModificacion"]) == false)
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
                    else
                    {
                        //hacer modificacion de valor en BD
                    }
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
                    GridViewRow row = dgvArticulos.Rows[index];
                    Articulo art = new Articulo();
                    art.indexBD = int.Parse(row.Cells[2].Text);
                    art.descripcion = row.Cells[3].Text;
                    art.precio = float.Parse(row.Cells[4].Text);
                    art.stock = int.Parse(row.Cells[5].Text);
                    object o = row.Cells[6].;
                    art.disponible = Boolean.Parse(row.Cells[6].Text);
                    art.tipoArticulo = int.Parse(row.Cells[7].Text);
                    art.tipoArticuloString = row.Cells[8].Text;

                    txtDescripcion.Text = art.descripcion.ToString();
                    txtPrecio.Text = art.precio.ToString();
                    txtStock.Text = art.stock.ToString();
                    ckbDisponible.Checked = art.disponible;
                    ddlTipoArticulo.SelectedValue = art.tipoArticulo.ToString();

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
