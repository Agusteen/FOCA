<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="ABMC_Clientes.aspx.cs" Inherits="FOCA_gadgets_V1.ABMC_Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="js/bootstrap-datepicker.js"></script>
    <link href="css/datepicker.css" rel="stylesheet" />
    <script>
        $(function () {
            $('.datepicker').datepicker();
        });
    </script>


    <div class="panel-body">
        <div class="col-xs-5">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Datos de clientes</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label for="nombre">Nombre</label>
                        <asp:TextBox ID="txtNombre" class="form-control" placeholder="Ingrese aquí su nombre" runat="server" Font-Size="Large"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="apellido">Apellido</label>
                        <asp:TextBox class="form-control" ID="txtApellido" placeholder="Ingrese aquí su apellido" runat="server" Font-Size="Large"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="dni">Número de documento</label>
                        <asp:TextBox class="form-control" ID="txtDni" name="txtDni" placeholder="Ingrese solo números, sin puntos" runat="server" onkeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" Font-Size="Large" />
                        <asp:RegularExpressionValidator ControlToValidate="txtDni" ValidationExpression="[0-9]{8}|[0-9]{7}" runat="server" ErrorMessage="* Debe tener como máximo 8 dígitos y como mínimo 7 dígitos" Font-Size="X-Small" ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>

                    <div class="form-group">
                        <label for="domicilio">Domicilio</label>
                        <asp:TextBox class="form-control" ID="txtDomicilio" placeholder="Calle Numero Piso" runat="server" Font-Size="Large"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="localidad">Localidad</label>
                        <asp:DropDownList ID="ddlLocalidades" class="form-control" runat="server" Font-Size="Large"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="telefono">Telefono</label>
                        <asp:TextBox ID="txtTelefono" class="form-control" placeholder="Ingrese solo números, sin signos" runat="server" Font-Size="Large"></asp:TextBox>

                    </div>

                    <div class="form-group">
                        <label for="fechaNacimiento">Fecha de nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimiento" class="datepicker form-control" placeholder="Fecha de Nacimiento" runat="server" onkeypress="event.returnValue = false;"></asp:TextBox>
                        <asp:RangeValidator ErrorMessage="* Debe ser mayor de 18 años" ControlToValidate="txtFechaNacimiento" runat="server" Font-Size="X-Small" ForeColor="Red" OnInit="rangeValidator_Init" Type="Date" />
                    </div>

                    <asp:CheckBox type="checkbox-info" ID="chboxPreferencial" Text=" Preferencial" runat="server"></asp:CheckBox>

                    <div class="form-group">
                        <asp:Button class="btn btn-default" Text="Guardar" runat="server" OnClick="enviar"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-xs-7">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Tabla de clientes</h3>
                </div>
                <div class="panel-body">
                    <asp:Panel runat="server" ID="panelGrid" Width="100%" ScrollBars="Both" ShowFooter="True" CssClass="panelCss">
                    <asp:GridView ID="grdClientes" style="margin-left: 0px" HorizontalAlign="Center" class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="true" CellPadding="7" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="grdClientes_SelectedIndexChanged" >   
                           
                    <Columns>
                                <asp:CommandField ButtonType="Image" SelectImageUrl="images/editar.png" ShowSelectButton="True" />
                                <asp:CommandField ButtonType="Image" SelectImageUrl="images/eliminar.png" ShowSelectButton="True" />
                            </Columns>
                    </asp:GridView>
                    </asp:Panel>
                </div>
        </div>
    </div>

    </div>

</asp:Content>
