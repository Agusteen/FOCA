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
            if (!IsPostBack)
            {
                cargarComboLocalidades();
                cargarComboRoles();
                cargarGrillaClientes();
            }
            
        }

        private void cargarGrillaClientes()
        {
            grdClientes.DataSource = GestorClientes.ObtenerTodos();
            grdClientes.DataBind();
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
                try
                {
                    Cliente cli = new Cliente();
                    
                        cli.nombre = txtNombre.Text;                        
                        cli.apellido = txtApellido.Text;                    
                        cli.localidad = ddlLocalidades.SelectedIndex;
                        cli.fechaNac = txtFechaNacimiento.Text;
                        cli.mail = txtMail.Text;
                        cli.password = txtPassword.Text;
                        cli.rol = ddlRoles.SelectedIndex;
                        cli.preferencial = chboxPreferencial.Checked;

                    
                    GestorClientes.Insertar(cli);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('EXITO')", true);
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ERROR')", true);
                }
                finally
                {
                    cargarGrillaClientes();
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

                }

                if (commandName.Equals("ELIMINAR"))
                {
                    try
                    {

                        GridViewRow row = grdClientes.Rows[index];
                        int indexBD = int.Parse(row.Cells[2].Text);

                        GestorClientes.eliminarCliente(indexBD);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('EXITO')", true);

                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ERROR')", true);

                    }
                    finally
                    {
                        cargarGrillaClientes();
                    }
                }
            }
        }       

        
    }
}