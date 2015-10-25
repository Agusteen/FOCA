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

    <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-xs-5">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Datos de clientes
                            <asp:Label ID="lblEstadoPage" runat="server" Visible="true"></asp:Label>
                        </h3>
                    </div>
                    
                    <div class="panel-body">
                        
                        <div class="panel panel-default">
                    
                    <div class="panel-body">
                        
                        <div class="form-group">
                            <label for="E-Mail">E-Mail</label>
                            <asp:TextBox ID="txtMail" class="form-control" placeholder="ejemplo@mail.com" runat="server" Font-Size="Large"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="* Este campo es requerido" ControlToValidate="txtMail" runat="server" Font-Size="X-Small" ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="valEmailAddress" ControlToValidate="txtMail"	ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Size="X-Small" ForeColor="Red" ErrorMessage="Formato de mail no es correcto" Display="None" Runat="server"/>
                        </div>

                        <div class="form-group">
                            <label for="nombre">Password</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" placeholder="Ingrese aquí una contraseña" runat="server" Font-Size="Large"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="* Este campo es requerido" ControlToValidate="txtPassword" runat="server" Font-Size="X-Small" ForeColor="Red" />
                        </div>

                        <div class="form-group">
                            <label for="rol">Rol</label>
                            <asp:DropDownList ID="ddlRoles" class="form-control" runat="server" Font-Size="Large"></asp:DropDownList>
                        </div></div></div>
                        
                        <div class="form-group">
                            <label for="nombre">Nombre</label>
                            <asp:TextBox ID="txtNombre" class="form-control" placeholder="Ingrese aquí su nombre" runat="server" Font-Size="Large"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="* Este campo es requerido" ControlToValidate="txtNombre" runat="server" Font-Size="X-Small" ForeColor="Red" />
                        </div>

                        
                        <div class="form-group">
                            <label for="apellido">Apellido</label>
                            <asp:TextBox class="form-control" ID="txtApellido" placeholder="Ingrese aquí su apellido" runat="server" Font-Size="Large"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="*Este campo es requerido" ControlToValidate="txtApellido" runat="server" Font-Size="X-Small" ForeColor="Red" />

                        </div>             

                        <div class="form-group">
                            <label for="localidad">Localidad</label>
                            <asp:DropDownList ID="ddlLocalidades" class="form-control" runat="server" Font-Size="Large"></asp:DropDownList>
                        </div>


                        <div class="form-group">
                            <label for="fechaNacimiento">Fecha de nacimiento</label>
                            <asp:TextBox ID="txtFechaNacimiento" class="datepicker form-control" placeholder="Fecha de Nacimiento" Font-Size="Large" runat="server"></asp:TextBox>
                            <asp:RangeValidator ErrorMessage="* Debe ser mayor de 18 años" ControlToValidate="txtFechaNacimiento" runat="server" Font-Size="X-Small" ForeColor="Red" OnInit="rangeValidator_Init" Type="Date" />
                            <asp:RequiredFieldValidator ErrorMessage="* Este campo es requerido" ControlToValidate="txtFechaNacimiento" runat="server" Font-Size="X-Small" ForeColor="Red" />
                        </div>

                        <asp:CheckBox type="checkbox-info" ID="chboxPreferencial" Text=" Preferencial" runat="server"></asp:CheckBox>

                        
                        
                        <div class="panel-heading text-center" >
                            <asp:Button class="btn btn-default btn-lg"  Text="Guardar" runat="server" OnClick="enviar" ></asp:Button>
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

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <asp:Button class="btn btn-default" ID="btnFiltrar" Text="Filtrar" OnClick="btnFiltrar_Click" runat="server" CausesValidation="False"></asp:Button>
                                    </span>
                                    <asp:TextBox class="form-control" ID="txtFiltroApellido" placeholder="Ingrese el apellido del cliente" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                            <br />                            

                        <asp:Panel runat="server" ID="panelGrid" Width="100%" ScrollBars="Both" ShowFooter="True" CssClass="panelCss">
                            <asp:GridView ID="grdClientes" Style="margin-left: 0px" HorizontalAlign="Center" DataKeyNames="indexBD" class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="false" CellPadding="7" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="grdClientes_SelectedIndexChanged" OnRowCommand="grdClientes_RowCommand" OnPageIndexChanging="grdClientes_PageIndexChanging" PageSize="13" AllowPaging="true" PagerStyle-HorizontalAlign="Center" OnSorting="grdClientes_Sorting" >

                                <Columns>
                                    <asp:ButtonField CommandName="Modificar" ImageUrl="images/editar.png" ButtonType="Image" />                                    
                                    <asp:ButtonField CommandName="Eliminar" ButtonType="Image" ImageUrl="images/eliminar.png"/>
                                    <asp:BoundField DataField="nombreyapellido" HeaderText="Nombre" />
                                    <asp:BoundField DataField="mail" HeaderText="Mail" />
                                    <asp:BoundField DataField="rolString" HeaderText="Rol" />
                                    <asp:BoundField DataField="preferencial" HeaderText="Preferencial" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>

        </div>
        </div>
        
    
</asp:Content>
