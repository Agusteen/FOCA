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
        }

        private void cargarComboLocalidades()
        {
            ddlLocalidades.DataSource = GestorLocalidades.ObtenerTodas();
            ddlLocalidades.DataTextField = "Nombre";
            ddlLocalidades.DataBind();
        }

        protected void enviar(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Cliente cli = new Cliente()
                    {
                        nombre = txtNombre.Text,
                        apellido = txtApellido.Text,
                        domicilio = txtDomicilio.Text,
                        dni = int.Parse(txtDni.Text),
                        telefono = int.Parse(txtTelefono.Text)

                    };
                    GestorClientes.Insertar(cli);  
                }
                catch
                {

                }
                              
            }

        }

        protected void rangeValidator_Init(object sender, EventArgs e)
        {
            ((RangeValidator)sender).MaximumValue =DateTime.Now.Date.AddYears(-18).ToString("dd/MM/yyyy");
            ((RangeValidator)sender).MinimumValue = DateTime.Now.Date.AddYears(-120).ToString("dd/MM/yyyy");

        }
        
    }
}