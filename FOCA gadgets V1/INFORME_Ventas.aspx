﻿<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="INFORME_Ventas.aspx.cs" Inherits="FOCA_gadgets_V1.INFORME_Ventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="js/bootstrap-datepicker.js"></script>    
    <script>
        $(function () {
            $('.datepicker').datepicker();
            $('#datepicker').datepicker('option', { dateFormat: 'mm/dd/yy' });
        });
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Seleccione informe</h3>
        </div>
        <div class="panel-body">
            <asp:DropDownList ID="ddlInforme" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInforme_SelectedIndexChanged">
                <asp:ListItem Text="Informe de ventas"></asp:ListItem>
                <asp:ListItem Text="Informe de reparaciones"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <div class="panel panel-default">     
                
                    
                    <div class="panel-heading">
                       <h3 class="panel-title">Listado de ventas</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-6">
                        <div>
                        <label for="filtro">Filtros </label>
                            </div>

                        <div>
                         <label for ="Monto">Monto</label>
                        <asp:TextBox class="form-control" ID="txtFiltroMonto" placeholder="Ingrese el monto maximo con el que desea filtrar" runat="server"></asp:TextBox>
                         <asp:CompareValidator ErrorMessage=" * Debe ser un valor numérico" ControlToValidate="txtFiltroMonto" Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ValueToCompare="0" Type="Double" Operator="GreaterThanEqual" runat="server" ></asp:CompareValidator>
                   </div>
                        <div>
                        <label for="Fecha">Fecha</label>
                        <asp:TextBox class="datepicker form-control" ID="txtFiltroFecha" placeholder="Ingrese la fecha de la venta buscada" runat="server"></asp:TextBox>
                    <asp:CompareValidator ErrorMessage="* Debe ser una fecha valida" ControlToValidate="txtFiltroFecha" Font-Size="X-Small" ForeColor="Red" Display="Dynamic"  Type="Date" Operator="DataTypeCheck" runat="server" runat="server" />
                     </div>
</div>
                        <div class="col-xs-6">
                            <br />
                        <div>
                            <label for="Cliente">Cliente</label>
                        <asp:DropDownList ID="ddlCliente" class="form-control" runat="server"></asp:DropDownList>    
                       </div>
                            <br />
                        <div class="panel-heading text-right">
                             <asp:Button class="btn btn-default text-left" ID="btnFiltrar" Text="Filtrar" runat="server" OnClick="btnFiltrar_Click" CausesValidation="False"></asp:Button>
                        </div>
                            </div>
                        <div class="col-xs-12">
                        <asp:GridView  ID="dgvVentas" PageSize="20" Style="margin-left: 0px" HorizontalAlign="Center"
                            AllowPaging="true" AllowSorting="true"
                            class="form-control" CssClass=" table table-hover table-striped" runat="server" AutoGenerateColumns="False"
                            CellPadding="7" ForeColor="#333333" GridLines="None" 
                            OnPageIndexChanging="dgvVentas_PageIndexChanging" OnSorting="dgvVentass_Sorting" EmptyDataText="No hay registros">

                            <Columns>
                                
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

        
    
</asp:Content>

  