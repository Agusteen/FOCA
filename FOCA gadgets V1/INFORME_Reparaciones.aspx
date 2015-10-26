<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="INFORME_Reparaciones.aspx.cs" Inherits="FOCA_gadgets_V1.INFORME_Reparaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Seleccione informe</h3>
        </div>
        <div class="panel-body">
            <asp:DropDownList ID="ddlInforme" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInforme_SelectedIndexChanged">
                <asp:ListItem Value="v">Informe de ventas</asp:ListItem>
                <asp:ListItem Value="r">Informe de reparaciones</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    
    
    <div class="panel panel-default">     
                
                    
                    <div class="panel-heading">
                       <h3 class="panel-title">Listado de Reparaciones</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-6">
        <div>
            <label for="filtro">Filtros </label>
        </div>

        <div>
            <label for="Fecha">Fecha de reparación</label>
            <asp:TextBox class="form-control" ID="txtFiltroFecha" placeholder="Ingrese la fecha de la reparacion buscada" runat="server"></asp:TextBox>
            <asp:CompareValidator ErrorMessage="* Debe ser una fecha valida" ControlToValidate="txtFiltroFecha" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" Type="Date" Operator="DataTypeCheck" runat="server" runat="server" />
        </div>

        <div>
            <label for="Cliente">Cliente</label>
            <asp:DropDownList ID="ddlCliente" class="form-control" runat="server"></asp:DropDownList>
        </div>
                            </div>
                        <br />
<div class="col-xs-6">
        <div>
            <label for="Estado">Estado</label>
            <asp:DropDownList ID="ddlEstado" class="form-control" runat="server"></asp:DropDownList>
        </div>
    <br />

        <div class="panel-heading text-right">
            <asp:Button class="btn btn-default" ID="btnFiltrar" Text="Filtrar" runat="server" OnClick="btnFiltrar_Click" CausesValidation="False"></asp:Button>
        </div>
    </div>
        <div class="col-xs-12">
            <asp:GridView ID="dgvListadoReparaciones" PageSize="20" Style="margin-left: 0px" HorizontalAlign="Center"
                AllowPaging="true" AllowSorting="true"
                class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="False"
                CellPadding="7" ForeColor="#333333" GridLines="None"
                EmptyDataText="No hay registros">

                <Columns>

                    <asp:BoundField DataField="nombreCliente" HeaderText="Nombre y Apellido " />
                    <asp:BoundField DataField="stringPreferencial" HeaderText="Preferencial" />
                    <asp:BoundField DataField="monto" HeaderText="Monto" />
                    <asp:BoundField DataField="fechareparacion" HeaderText="Fecha de registracion" />
                    <asp:BoundField DataField="fechadevolucion" HeaderText="Fecha de devolucion" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />



                </Columns>

            </asp:GridView>
        </div>





    
</div>
        </div>


</asp:Content>
