<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="ABMC_Articulos.aspx.cs" Inherits="FOCA_gadgets_V1.ABMC_Articulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-xs-5">
                <div class="form-group">
                    <label for="descripcion">Descripción</label>                    
                    <asp:TextBox ID="txtDescripcion" class="form-control" placeholder="Ingrese una descripción" runat="server"></asp:TextBox>
               <asp:RequiredFieldValidator ControlToValidate="txtDescripcion" Display="Dynamic" runat="server" ErrorMessage="No puede quedar vacío" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>   
                     </div>
              
                <div class="form-group">
                    <label for="precio">Precio unitario</label>
                    <asp:TextBox class="form-control" id="txtPrecio" placeholder="Ingrese el precio del articulo" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ControlToValidate="txtPrecio" Display="Dynamic" runat="server" ErrorMessage=" * No puede quedar vacío" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>   
                       <asp:CompareValidator ErrorMessage=" * Debe ser un valor numérico" ControlToValidate="txtPrecio" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual" runat="server" ></asp:CompareValidator>
                   </div>

                <div class="form-group">
                    <label for="stock">Stock</label>
                    <asp:TextBox class="form-control" ID="txtStock" placeholder="Ingrese la cantidad de unidades a cargar" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtStock" Display="Dynamic" runat="server" ErrorMessage=" * No puede quedar vacío" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ErrorMessage=" * Debe ser un valor numérico" ControlToValidate="txtStock" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual" runat="server" ></asp:CompareValidator>
                    <%--<asp:RangeValidator ErrorMessage=" * Ingrese un valor mayor que 0 o menor que 1000" MinimumValue="0" MaximumValue="1000" ControlToValidate="txtStock" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" runat="server"></asp:RangeValidator>--%>

                </div>

                <div class="form-group">
                    <label for="disponible">Disponible para venta</label>
                </div>
                <div>
                 <asp:checkbox ID="ckbDisponible" Text="Esta disponible para venta?" Checked="true" runat="server"></asp:checkbox>
                </div>


                  <div class="form-group">
                    <label for="tipo">Tipo de articulo</label>                   
                        <asp:DropDownList ID="ddlTipoArticulo" class="form-control" runat="server"></asp:DropDownList>                    
                </div>

                <div>
                    <asp:Button class="btn btn-default" Text="Guardar" runat="server" OnClick="guardarArticulo"></asp:Button>
                </div>

             
        </div>

            <div class="col-xs-7">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Tabla de clientes</h3>
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="gvArticulos" class="form-control" CssClass="footable" runat="server" AutoGenerateColumns="True">
                            <%--<Columns>
                                <asp:CommandField SelectText="Eliminar" ShowSelectButton="True" />
                                <asp:CommandField SelectText="Modificar" ShowSelectButton="True" />
                                <asp:BoundField HeaderText="ID Articulo" ReadOnly="True" Visible="False" />
                                <asp:BoundField HeaderText="Descripcion" ReadOnly="True" />
                                <asp:BoundField HeaderText="Precio" ReadOnly="True" />
                                <asp:BoundField HeaderText="Stock" ReadOnly="True" />
                                <asp:BoundField HeaderText="Esta Disponible" ReadOnly="True" />
                                <asp:BoundField HeaderText="TipoID" ReadOnly="True" Visible="False" />
                                <asp:BoundField HeaderText="Tipo" ReadOnly="True" />
                            </Columns>--%>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            </div>
         </div>



</asp:Content>
