<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="INFORME_Reparaciones.aspx.cs" Inherits="FOCA_gadgets_V1.INFORME_Reparaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="js/bootstrap-datepicker.js"></script>    
    <script>
        $(function () {
            $('.datepicker').datepicker();            
        });
    </script>

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
            <label for="Fecha">Fecha de reparación desde</label>
            <asp:TextBox class="datepicker form-control" ID="txtFiltroFechaDesde" placeholder="Ingrese la fecha de la reparacion buscada" runat="server"></asp:TextBox>
            <asp:CompareValidator  ErrorMessage="* Debe ser una fecha valida" ControlToValidate="txtFiltroFechaDesde" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" Type="Date" Operator="LessThanEqual" runat="server" ControlToCompare="txtFiltroFechaHasta"/>
        </div>

        <div>
             <label for="Fecha">Fecha de reparación hasta</label>
             <asp:TextBox class="datepicker form-control" ID="txtFiltroFechaHasta" placeholder="Ingrese la fecha de la reparacion buscada" runat="server"></asp:TextBox>
             <asp:CompareValidator  ErrorMessage="* Debe ser una fecha valida" ControlToValidate="txtFiltroFechaHasta" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" Type="Date" Operator="DataTypeCheck" runat="server" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"/>
        </div>

        
                            </div>
                        <br />
<div class="col-xs-6">
        <div>
            <label for="Cliente">Cliente</label>
            <asp:DropDownList ID="ddlClientes" class="form-control" runat="server"></asp:DropDownList>
        </div>
    
    <div>
            <label for="Estado">Estado</label>
            <asp:DropDownList ID="ddlEstados" class="form-control" runat="server"></asp:DropDownList>
        </div>
    <br />

        <div class="panel-heading text-right">
            <asp:Button class="btn btn-default" ID="btnFiltrar" Text="Filtrar" runat="server" OnClick="btnFiltrar_Click" CausesValidation="true"></asp:Button>
        </div>
    </div>
                    
        
            
            
            <asp:GridView ID="dgvListadoReparaciones" PageSize="20" Style="margin-left: 0px" HorizontalAlign="Center"
                AllowPaging="true" AllowSorting="true"
                class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="False"
                CellPadding="7" ForeColor="#333333" GridLines="None"
                EmptyDataText="No hay registros">

                <Columns>

                    <asp:BoundField DataField="nombreCliente" HeaderText="Nombre y Apellido " />
                    <asp:BoundField DataField="stringPreferencial" HeaderText="Preferencial" />
                    <%--<asp:BoundField DataField="monto" HeaderText="Monto" />--%>
                    <asp:BoundField DataField="fechareparacion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha de registracion" />
                    <asp:BoundField DataField="fechadevolucion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha de devolucion" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />

                </Columns>

            </asp:GridView>
                
           
</div>      


</asp:Content>
