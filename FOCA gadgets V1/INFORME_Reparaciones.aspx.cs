using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FOCA_gadgets_V1
{
    public partial class INFORME_Reparaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarComboClientes();
                cargarComboEstados();
            }
        }

        private void cargarComboEstados()
        {
            throw new NotImplementedException();
        }

        private void cargarComboClientes()
        {
            throw new NotImplementedException();
        }
    }
}