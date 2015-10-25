<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="INFORME_Ventas.aspx.cs" Inherits="FOCA_gadgets_V1.INFORME_Ventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">

         <div class="col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                       <h3 class="panel-title">Listado de ventas</h3>
                    </div>
                    <div class="panel-body">
                        <label for="filtro">Filtros </label>
                         <label for ="Monto">Monto</label>
                        <asp:TextBox class="form-control" ID="txtFiltroMonto" placeholder="Ingrese el monto maximo con el que desea filtrar" runat="server"></asp:TextBox>
                        <label for="Fecha">Fecha</label>
                        <asp:TextBox class="form-control" ID="txtFiltroFecha" placeholder="Ingrese la fecha de la venta buscada" runat="server"></asp:TextBox>
                        <label for="Cliente">Cliente</label>
                         <asp:DropDownList ID="ddlCliente" class="form-control" runat="server"></asp:DropDownList>    
                        <asp:Button class="btn btn-default" ID="btnFiltrar" Text="Filtrar" runat="server" OnClick="btnFiltrar_Click" CausesValidation="False"></asp:Button>

                        <div class="col-xs-12">
                        <asp:GridView  ID="dgvVentas" PageSize="20" DataKeyNames="indexBD" Style="margin-left: 0px" HorizontalAlign="Center"
                            AllowPaging="true" AllowSorting="true"
                            class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="False"
                            CellPadding="7" ForeColor="#333333" GridLines="None" OnRowCommand="dgvVentas_RowCommand"
                            OnPageIndexChanging="dgvVentas_PageIndexChanging" OnSorting="dgvVentass_Sorting" EmptyDataText="No hay registros">

                            <Columns>
                               <%-- <asp:ButtonField CommandName="Modificar" HeaderText="" Text="Modificar" ButtonType="Button" />
                                <asp:ButtonField CommandName="Eliminar" HeaderText="" Text="Eliminar" ButtonType="Button" />--%>
                                <asp:BoundField DataField="nombreCliente" HeaderText="Nombre y Apellido " />
                                <asp:BoundField DataField="stringPreferencial" HeaderText="Preferencial" />
                                <asp:BoundField DataField="monto" HeaderText="Monto" />
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                            


                            </Columns>
                            
                        </asp:GridView>
                            </div>
                    </div>
                </div>
            </div>




        </div>
</asp:Content>

  