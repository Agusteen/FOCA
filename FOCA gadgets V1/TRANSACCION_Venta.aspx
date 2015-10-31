<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="TRANSACCION_Venta.aspx.cs" Inherits="FOCA_gadgets_V1.TRANSACCION_Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-default">
        <div class="panel-body">

            <div class="col-xs-5">
                <div class="form-group">
                    <label for="factura">Número de Factura</label>
                    <asp:TextBox ID="txtFactura" class="form-control" placeholder="Ingrese aquí el número de factura" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="transaccion" ErrorMessage="* Este campo es requerido" ControlToValidate="txtFactura" runat="server" Display="Dynamic" Font-Size="X-Small" ForeColor="Red" />
                    <%--                    <asp:RegularExpressionValidator validationgroup="transaccion" ValidationExpression="\d{5}" ErrorMessage="* La factura debe tener 5 digitos" ControlToValidate="txtFactura" runat="server" Display="Dynamic" Font-Size="X-Small" ForeColor="Red" />--%>
                    <asp:CompareValidator ValidationGroup="transaccion" ErrorMessage="* Debe ser valor numérico" ControlToValidate="txtFactura" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" Type="Integer" Operator="DataTypeCheck" runat="server" />
                </div>
                <div class="form-group">
                    <label for="fechaVenta">Fecha de Venta</label>
                    <asp:TextBox ID="txtFechaVenta" class="form-control" Text="<%# DateTime.Today.ToShortDateString() %>" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="transaccion" ErrorMessage="* Este campo es requerido" ControlToValidate="txtFechaVenta" runat="server" Display="Dynamic" Font-Size="X-Small" ForeColor="Red" />
                    <asp:CompareValidator ValidationGroup="transaccion" ErrorMessage="* Debe ser una fecha valida" ControlToValidate="txtFechaVenta" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" Type="Date" Operator="DataTypeCheck" runat="server" />

                </div>
                <div class="form-group">
                    <asp:Label for="clientes" ID="lblCliente" Text="Cliente" runat="server" />
                    <asp:DropDownList ID="ddlClientes" class="form-control" runat="server"></asp:DropDownList>
                    <asp:CompareValidator ValidationGroup="transaccion" ErrorMessage="* Debe seleccionar algun cliente" ControlToValidate="ddlClientes" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" Type="Integer" Operator="NotEqual" ValueToCompare="-1" runat="server" />
                </div>

                <div class="form-group">
                    <label for="monto">Monto</label>
                    <asp:TextBox ID="txtMonto" class="form-control" placeholder="Monto de la factura" runat="server" ReadOnly="True"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ErrorMessage="* Este campo es requerido" ControlToValidate="txtMonto" runat="server" Font-Size="X-Small" ForeColor="Red" />--%>
                </div>

                <div>
                    <asp:Button ID="btnCargarArticulos" class="btn btn-default btn-lg" ValidationGroup="transaccion" Text="Cargar articulos" runat="server" OnClick="cargarArticulos_Click" CausesValidation="true"></asp:Button>
                </div>
                <div>
                    <asp:Button class="btn btn-default btn-lg" Text="Modificar datos" runat="server" OnClick="ModificarDatos_Click" CausesValidation="true"></asp:Button>
                </div>
            </div>

            <asp:Panel class="panel" ID="panelArticulos" runat="server" Visible="false">
                <div class="col-xs-7">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Tabla de Articulos</h3>
                        </div>
                        <div class="panel-body">
                            <td class="style3">MI CARRITO<br />
                                SubTotal:
                    <asp:Label ID="lblTotalPesos" runat="server" Text="0,00"></asp:Label>
                                <br />
                                Unidades:
                    <asp:Label ID="lblTotalUnidades" runat="server" Text="0"></asp:Label>
                                <br />
                                <br />
                                <asp:Button ID="btnVerDetalle" Text="Ver Detalle de compra" runat="server" OnClick="btnVerDetalle_Click" class="btn btn-default"></asp:Button>
                                <br />
                                <br />
                            </td>


                            <div class="form-group">
                                <label for="tipo">Tipo de articulo</label>
                                <asp:DropDownList ID="ddlTipoArticulo" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <label for="descripcion">Descripción</label>
                            <div class="input-group">
                                <span class="input-group-btn">
                                    <asp:Button class="btn btn-default" ID="btnFiltrar" Text="Filtrar" OnClick="btnFiltrar_Click" runat="server" CausesValidation="false"></asp:Button>
                                </span>
                                <asp:TextBox class="form-control" ID="txtFiltroDescripcion" placeholder="Ingrese la descripcion o parte de la descripcion" runat="server"></asp:TextBox>
                            </div>                            
                            <br />
                                <asp:GridView ID="dgvArticulos" PageSize="6" DataKeyNames="indexBD" HorizontalAlign="Center"
                                    AllowPaging="True" AllowSorting="True"
                                    class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="False"
                                    CellPadding="8" ForeColor="#333333" GridLines="None"
                                    OnPageIndexChanging="dgvArticulos_PageIndexChanging" PagerStyle-HorizontalAlign="Center" OnSorting="dgvArticulos_Sorting" EmptyDataText="No hay registros" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged">

                                    <Columns>

                                        <asp:CommandField SelectText="Agregar al carrito" ShowSelectButton="True" HeaderText="Seleccionar" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                        <asp:BoundField DataField="precio" HeaderText="Precio" />
                                        <asp:BoundField DataField="stock" HeaderText="Stock" />
                                        <asp:BoundField DataField="stringDisponible" HeaderText="Disponible" />
                                        <asp:BoundField DataField="tipoArticuloString" HeaderText="Familia" />


                                    </Columns>

                                    <PagerStyle HorizontalAlign="Center"></PagerStyle>

                                </asp:GridView>
                            
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel class="panel panel-default" ID="panelCarrito" runat="server" Visible="false">
                
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mi carrito</h3>
                        </div>
                        <div class="panel-body">
                            <td class="style3">MI CARRITO<br />
                                SubTotal:
                    <asp:Label ID="lbltotalPesosCarrito" runat="server" Text="0,00"></asp:Label>
                                <br />
                                Unidades:
                    <asp:Label ID="lbltotalUnidadesCarrito" runat="server" Text="0"></asp:Label>
                                <br />
                                <br />
                                <asp:Button ID="btnFinalizarVenta" Text="Finalizar venta" runat="server" OnClick="btnFinalizarVenta_Click"></asp:Button>
                                <br />
                                <div>
                                    <asp:Button ID="btnVolverAlCarrito" Text="Volver al carrito" runat="server" OnClick="btnVolverAlCarrito_Click"></asp:Button>
                                </div>
                                <br />
                            </td>


                            <div class="form-group">
                                <label for="tipo">Detalle de venta</label>
                            </div>
                            <br />

                            
                                <asp:GridView ID="dgvCarrito" PageSize="6" DataKeyNames="articulo" HorizontalAlign="Center"
                                    AllowPaging="True" AllowSorting="True"
                                    class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="False"
                                    CellPadding="8" ForeColor="#333333" GridLines="None"
                                    OnPageIndexChanging="dgvCarrito_PageIndexChanging" PagerStyle-HorizontalAlign="Center" OnSorting="dgvCarrito_Sorting" EmptyDataText="No hay registros" OnSelectedIndexChanged="dgvCarrito_SelectedIndexChanged">

                                    <Columns>
                                        <asp:CommandField SelectText="Sacar del carrito" ShowSelectButton="True" HeaderText="Seleccionar" />
                                        <asp:BoundField DataField="descripcionArticulo" HeaderText="Descripcion" />
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="precioArticulo" HeaderText="Precio Unitario" />
                                        <asp:BoundField DataField="subTotal" HeaderText="Precio Total" />

                                    </Columns>

                                    <PagerStyle HorizontalAlign="Center"></PagerStyle>

                                </asp:GridView>
                            
                        </div>
                    </div>
                
            </asp:Panel>
        </div>
    </div>

</asp:Content>
