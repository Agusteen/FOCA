<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="TRANSACCION_Reparacion.aspx.cs" Inherits="FOCA_gadgets_V1.TRANSACCION_Reparacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Codigo del datepicker de fecha de devolucion--%>
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="js/bootstrap-datepicker.js"></script>
    <link href="css/datepicker.css" rel="stylesheet" />
    <script>
        $(function () {
            $('.datepicker').datepicker();
        });
    </script>
    
    <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-xs-6">

                <div class="form-group">
                    <label for="fechaReparacion">Fecha de reparación</label>
                    <asp:TextBox ID="txtFechaRepracion" class="form-control" Font-Size="Large" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="* Este campo es requerido" ControlToValidate="txtFechaRepracion" runat="server" Font-Size="X-Small" ForeColor="Red" />
                </div>

                <div class="form-group">
                    <label for="fechaDevolucion">Fecha de devolución</label>
                    <asp:TextBox ID="txtFechaDevolucion" class="datepicker form-control" placeholder="Click para desplegar el calendario" Font-Size="Large" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="* Este campo es requerido" ControlToValidate="txtFechaDevolucion" runat="server" Font-Size="X-Small" ForeColor="Red" />
                </div>

                <div class="form-group">
                    <label for="clientes">Cliente</label>
                    <asp:DropDownList ID="ddlClientes" class="form-control" runat="server" Font-Size="Large"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <label for="equipo">Equipo a reparar</label>
                    <asp:TextBox ID="txtEquipo" class="form-control" placeholder="Ingrese aquí una descripción del equipo" runat="server" Font-Size="Large"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="* Este campo es requerido" ControlToValidate="txtEquipo" runat="server" Font-Size="X-Small" ForeColor="Red" />
                </div>

                <div class="form-group">
                    <label for="descripcion">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" class="form-control" placeholder="Ingrese aquí una descripción de la reparación" runat="server" Font-Size="Large"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="* Este campo es requerido" ControlToValidate="txtDescripcion" runat="server" Font-Size="X-Small" ForeColor="Red" />
                </div>

                <div class="form-group">
                    <label for="estado">Estado</label>
                    <asp:DropDownList ID="ddlEstados" lass="form-control" runat="server" Font-Size="Large"></asp:DropDownList>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
