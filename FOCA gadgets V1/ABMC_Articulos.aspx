<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="ABMC_Articulos.aspx.cs" Inherits="FOCA_gadgets_V1.ABMC_Articulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-xs-5">
                <div class="form-group">
                    <label for="descripcion">Descripción</label>                    
                    <asp:TextBox ID="txtDescripcion" class="form-control" placeholder="Ingrese una descripción" runat="server" EnableTheming="True"></asp:TextBox>
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
                        <h3 class="panel-title">Tabla de Articulos</h3>
                    </div>
                    <div class="panel-body">
                        <label for="filtro">Filtro descripcion</label>
                        <asp:TextBox class="form-control" ID="txtFiltroDescripcion" placeholder="Ingrese la descripcion o parte de la descripcion" runat="server"></asp:TextBox>
                        <asp:Button class="btn btn-default" ID="btnFiltrar" Text="Filtrar" runat="server" OnClick="btnFiltrar_Click" CausesValidation="False"></asp:Button>

                        <div class="col-xs-12">
                        <asp:GridView  ID="dgvArticulos" PageSize="6" DataKeyNames="indexBD" Style="margin-left: 0px" HorizontalAlign="Center"
                            AllowPaging="true" AllowSorting="true"
                            class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="False"
                            CellPadding="7" ForeColor="#333333" GridLines="None" OnRowCommand="dgvArticulos_RowCommand"
                            OnPageIndexChanging="dgvArticulos_PageIndexChanging" OnSorting="dgvArticulos_Sorting" EmptyDataText="No hay registros">

                            <Columns>
                                <asp:ButtonField CommandName="Modificar" HeaderText="" Text="Modificar" ButtonType="Button" />
                                <asp:ButtonField CommandName="Eliminar" HeaderText="" Text="Eliminar" ButtonType="Button" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                <asp:BoundField DataField="precio" HeaderText="Precio" />
                                <asp:BoundField DataField="stock" HeaderText="Stock" />
                                <asp:BoundField DataField="stringDisponible" HeaderText="Disponible para venta" />
                                <asp:BoundField DataField="tipoArticuloString" HeaderText="Familia" />


                            </Columns>
                            
                        </asp:GridView>
                            </div>
                    </div>
                </div>
            </div>

           

           
            </div>
         </div>



</asp:Content>
