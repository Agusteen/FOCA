using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FOCA_Entidades;
using FOCA_Negocio;


namespace FOCA_gadgets_V1
{
    public partial class ABMC_Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["esmodificacion"] = false;
                ViewState["ordenGvClientes"] = "Apellido";
                cargarComboLocalidades();
                cargarComboRoles();
                cargarGrillaClientes();
                divMensaje.Visible = false;
                
                
            }
            
        }

        private void cargarGrillaClientes()
        {
            string contieneDescripcion = "%" + txtFiltroApellido.Text + "%";
            string orden = "Apellido";
            if (ViewState["ordenGvClientes"] != null)
            {
                orden = ViewState["ordenGvClientes"].ToString();
            }
            grdClientes.DataSource = GestorClientes.ObtenerTodos(contieneDescripcion, orden);
            grdClientes.DataBind();
        }

        private void limpiarCampos()
        {
            lblEstadoPage.Text = "";
            txtMail.ReadOnly = false;
            txtMail.Text = "";
            txtPassword.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtFechaNacimiento.Text = "";
            txtFiltroApellido.Text = "";
            

        }

        private void cargarComboRoles()
        {
            ddlRoles.DataSource = GestorRoles.ObtenerTodas();
            ddlRoles.DataTextField = "descripcionRol";
            ddlRoles.DataValueField = "idRol";
            ddlRoles.DataBind();
        }

        private void cargarComboLocalidades()
        {
            ddlLocalidades.DataSource = GestorLocalidades.ObtenerTodas();
            ddlLocalidades.DataTextField = "Nombre";
            ddlLocalidades.DataValueField = "idLocalidad";
            ddlLocalidades.DataBind();
        }

        protected void enviar(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["esmodificacion"] != null)
                {

                    if (((Boolean)ViewState["esmodificacion"]) == false)
                    {
                        try
                        {
                            Cliente cli = new Cliente();

                            cli.nombre = txtNombre.Text;
                            cli.apellido = txtApellido.Text;
                            cli.localidad = int.Parse(ddlLocalidades.SelectedValue);
                            DateTime fecha = DateTime.Parse(txtFechaNacimiento.Text);
                            cli.fechaNac =  fecha.ToString("yyyy-MM-dd");
                            cli.mail = txtMail.Text;
                            cli.password = txtPassword.Text;
                            cli.rol = int.Parse(ddlRoles.SelectedValue);
                            cli.preferencial = chboxPreferencial.Checked;


                            GestorClientes.Insertar(cli);
                            lblMensaje.Text = "El cliente se registró correctamente";
                            divMensaje.Visible = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El cliente se registró correctamente')", true);
                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El cliente no se pudo registrar')", true);
                        }
                        finally
                        {
                            cargarGrillaClientes();
                            limpiarCampos();
                        }

                    }
                    else
                    {
                        if (((Boolean)ViewState["esmodificacion"]) == true)
                        {
                            try
                            {

                                Cliente cli = new Cliente();

                                cli.nombre = txtNombre.Text;
                                cli.apellido = txtApellido.Text;
                                cli.localidad = int.Parse(ddlLocalidades.SelectedValue);
                                DateTime fecha = DateTime.Parse(txtFechaNacimiento.Text);
                                cli.fechaNac = fecha.ToString("yyyy-MM-dd");
                                cli.mail = txtMail.Text;
                                cli.password = txtPassword.Text;
                                cli.rol = int.Parse(ddlRoles.SelectedValue);
                                cli.preferencial = chboxPreferencial.Checked;
                                GestorClientes.modificarCliente(cli);

                                
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El cliente se modificó correctamente')", true);

                            }
                            catch
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El cliente no se pudo modificar')", true);

                            }
                            finally
                            {
                                cargarGrillaClientes();
                                limpiarCampos();
                            }

                        }
                    }
                }

            }
        }

        protected void rangeValidator_Init(object sender, EventArgs e)
        {
            ((RangeValidator)sender).MaximumValue =DateTime.Now.Date.AddYears(-18).ToString("dd/MM/yyyy");
            ((RangeValidator)sender).MinimumValue = DateTime.Now.Date.AddYears(-120).ToString("dd/MM/yyyy");

        }

        protected void grdClientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            {
                String commandName = e.CommandName.ToUpper();
                int index = Convert.ToInt32(e.CommandArgument);

                if (commandName.Equals("MODIFICAR"))
                {
                    int indexBD = int.Parse(grdClientes.DataKeys[index]["indexBD"].ToString());
                    Cliente cli = GestorClientes.obtenerClientePorID(indexBD);
                    txtMail.Text = cli.mail.ToString();
                    txtMail.ReadOnly = true;
                    
                    txtPassword.Text = cli.password.ToString();
                    ddlRoles.SelectedValue = cli.rol.ToString();
                    txtNombre.Text = cli.nombre.ToString();
                    txtApellido.Text = cli.apellido.ToString();
                    ddlLocalidades.SelectedValue = cli.localidad.ToString();
                    txtFechaNacimiento.Text = cli.fechaNac.ToString();
                    chboxPreferencial.Checked = cli.preferencial;
                    ViewState["esmodificacion"] = true;
                    lblEstadoPage.Text = " (Modificacion)";
                }

                if (commandName.Equals("ELIMINAR"))
                {
                    try
                    {

                        int indexBD = int.Parse(grdClientes.DataKeys[index]["indexBD"].ToString());
                        GestorClientes.eliminarCliente(indexBD);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('El cliente se eliminó correctamente')", true);
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se pudo eliminar el cliente')", true);

                    }
                    finally
                    {
                        cargarGrillaClientes();
                        limpiarCampos();
                    }
                }
            }
        }

        protected void grdClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdClientes.PageIndex = e.NewPageIndex;
            cargarGrillaClientes();
        }

        protected void grdClientes_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["OrdenGvClientes"] = e.SortExpression;
            cargarGrillaClientes();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarGrillaClientes();
        }       

        
    }
}