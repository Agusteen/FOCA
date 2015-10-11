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
                ViewState["esmodificacion"] = false;
                ViewState["ordenGvArticulos"] = "Descripcion";
                cargarComboTipoArticulos();
                CargarGrilla();
               
            }
           
        }
        private void CargarGrilla()
        {
            string contieneDescripcion = "%" + txtFiltroDescripcion.Text + "%";
                string orden = "Descripcion";
            if (ViewState["ordenGvArticulos"] != null)
            {
                orden = ViewState["ordenGvArticulos"].ToString();
            }
            dgvArticulos.DataSource = GestorArticulos.obtenerArticulos( contieneDescripcion, orden);
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
                if (ViewState["esmodificacion"] != null)
                {
                    
                    if (((Boolean)ViewState["esmodificacion"]) == false)
                    {
                        try
                        {

                            
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
                            limpiarcampos();
                        }

                    }
                    else
                    {
                        if (((Boolean)ViewState["esmodificacion"]) == true)
                        {
                            try
                            {

                                Articulo art = new Articulo()
                                {
                                    descripcion = txtDescripcion.Text.ToUpper(),
                                    precio = float.Parse(txtPrecio.Text),
                                    stock = int.Parse(txtStock.Text),
                                    disponible = ckbDisponible.Checked,
                                    tipoArticulo = int.Parse(ddlTipoArticulo.SelectedValue)

                                };
                                GestorArticulos.modificarArticulo(art);
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
                                limpiarcampos();

                            }

                        }
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
                                     
                    int indexBD = int.Parse(dgvArticulos.DataKeys[index]["indexBD"].ToString());
                    Articulo art = GestorArticulos.obtenerArticulo(indexBD);
                    txtDescripcion.Text = art.descripcion.ToString();
                    txtDescripcion.ReadOnly = true;
                    ViewState["esmodificacion"] = true;
                    txtPrecio.Text = art.precio.ToString();
                    txtStock.Text = art.stock.ToString();
                    ckbDisponible.Checked = art.disponible;
                    ddlTipoArticulo.SelectedValue = art.tipoArticulo.ToString();

                }

                if (commandName.Equals("ELIMINAR"))
                    {
                    try
                    {
                      
                            
                        int indexBD = int.Parse(dgvArticulos.DataKeys[index]["indexBD"].ToString());


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

        protected void limpiarcampos()
        {
            txtDescripcion.Text = "";
            ViewState["esmodificacion"] = false;
            txtDescripcion.ReadOnly = false;
            txtPrecio.Text = "";
            txtStock.Text = "";
            ckbDisponible.Checked = true;
            ddlTipoArticulo.SelectedIndex =1 ;
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void dgvArticulos_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["OrdenGvArticulos"] = e.SortExpression;
            CargarGrilla();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
    }
