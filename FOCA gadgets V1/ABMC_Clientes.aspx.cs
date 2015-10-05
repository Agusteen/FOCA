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
                cargarComboLocalidades();
            cargarGrillaClientes();
        }

        private void cargarGrillaClientes()
        {
            grdClientes.DataSource = GestorClientes.ObtenerTodos();
            grdClientes.DataBind();
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
                        cli.domicilio = txtDomicilio.Text;
                        cli.dni = long.Parse(txtDni.Text);
                        cli.telefono = long.Parse(txtTelefono.Text);
                        cli.localidad = ddlLocalidades.SelectedIndex;
                        cli.fechaNac = txtFechaNacimiento.Text;
                        cli.preferencial = chboxPreferencial.Checked;

                    
                    GestorClientes.Insertar(cli);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('EXITO')", true);
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ERROR')", true);
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
        
    }
}