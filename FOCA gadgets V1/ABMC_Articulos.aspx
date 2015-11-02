<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="ABMC_Articulos.aspx.cs" Inherits="FOCA_gadgets_V1.ABMC_Articulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="css/ConfirmacionStyle.css" rel="stylesheet" />
  
    <script src="js/Confirmacion.js"></script>
  
  <script>
      $(function () {
          $("#dialog-confirm").dialog({
              resizable: false,
              height: 150,
              modal: true,
              buttons: {
                  "OK": function () {
                      $(this).dialog("close");
                  }
                  
              }
          });
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

                        <%--<div id="dialog-confirm" title="Aviso">
                            <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Ingrese los datos del articulo</p>
                        </div>--%>

                        <div class="form-group">
                                    <label for="descripcion">Descripción</label>
                                    <asp:TextBox ID="txtDescripcion" class="form-control" placeholder="Ingrese una descripción" runat="server" EnableTheming="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtDescripcion" Display="Dynamic" runat="server" ErrorMessage="* Este campo no puede quedar vacío" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="precio">Precio unitario</label>
                                    <asp:TextBox class="form-control" ID="txtPrecio" placeholder="Ingrese el precio del articulo" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtPrecio" Display="Dynamic" runat="server" ErrorMessage="* Este campo no puede quedar vacío" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ErrorMessage="* Debe ser un valor numérico" ControlToValidate="txtPrecio" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ValueToCompare="0" Type="Double" Operator="GreaterThanEqual" runat="server"></asp:CompareValidator>
                                </div>

                                <div class="form-group">
                                    <label for="stock">Stock</label>
                                    <asp:TextBox class="form-control" ID="txtStock" placeholder="Ingrese la cantidad de unidades a cargar" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtStock" Display="Dynamic" runat="server" ErrorMessage="* Este campo no puede quedar vacío" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ErrorMessage="* Debe ser un valor numérico" ControlToValidate="txtStock" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ValueToCompare="0" Type="Integer" Operator="GreaterThanEqual" runat="server"></asp:CompareValidator>                                    
                                </div>

                                
                                <div>
                                    <asp:CheckBox ID="ckbDisponible" Text="Disponible para venta" Checked="true" runat="server"></asp:CheckBox>
                                </div>
                        <br />

                                <div class="form-group">
                                    <label for="tipo">Tipo de articulo</label>
                                    <asp:DropDownList ID="ddlTipoArticulo" class="form-control" runat="server"></asp:DropDownList>
                                </div>

                        <div class="panel-heading text-center">

                            <div>
                                <asp:Button class="btn btn-default btn-lg" Text="Guardar" runat="server" OnClick="guardarArticulo"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-7">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Tabla de Articulos</h3>
                    </div>
                    <div class="panel-body">

                        <div class="input-group">
                            <span class="input-group-btn">
                                <asp:Button class="btn btn-default" ID="btnFiltrar" Text="Filtrar" OnClick="btnFiltrar_Click" runat="server" CausesValidation="False"></asp:Button>
                            </span>
                            <asp:TextBox class="form-control" ID="txtFiltroDescripcion" placeholder="Ingrese la descripcion o parte de la descripcion" runat="server"></asp:TextBox>
                        </div>

                        <br />
<%--                        <label for="filtro">Filtro descripcion</label>
                        <asp:TextBox class="form-control" ID="txtFiltroDescripcion" placeholder="Ingrese la descripcion o parte de la descripcion" runat="server"></asp:TextBox>
                        <asp:Button class="btn btn-default" ID="btnFiltrar" Text="Filtrar" runat="server" OnClick="btnFiltrar_Click" CausesValidation="False"></asp:Button>--%>

                        
                        <asp:GridView  ID="dgvArticulos" PageSize="6" DataKeyNames="indexBD" HorizontalAlign="Center"
                            AllowPaging="true" AllowSorting="true"
                            class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="False"
                            CellPadding="8" ForeColor="#333333" GridLines="None" OnRowCommand="dgvArticulos_RowCommand"
                            OnPageIndexChanging="dgvArticulos_PageIndexChanging" PagerStyle-HorizontalAlign="Center" OnSorting="dgvArticulos_Sorting" EmptyDataText="No hay registros">

                            <Columns>
                                <asp:ButtonField CommandName="Modificar" ImageUrl="images/editar.png" ButtonType="Image" />
                                <asp:ButtonField CommandName="Eliminar" ButtonType="Image" ImageUrl="images/eliminar.png" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                <asp:BoundField DataField="precio" HeaderText="Precio" />
                                <asp:BoundField DataField="stock" HeaderText="Stock" />
                                <asp:BoundField DataField="stringDisponible" HeaderText="Disponible" />
                                <asp:BoundField DataField="tipoArticuloString" HeaderText="Familia" />


                            </Columns>
                            
                        </asp:GridView>
                            </div>
                    
                </div>
            </div>

           

           
            </div>
         </div>



</asp:Content>
