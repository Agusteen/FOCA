<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="ABMC_Articulos.aspx.cs" Inherits="FOCA_gadgets_V1.ABMC_Articulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-xs-5">
                <div class="form-group">
                    <label for="descripcion">Descripción</label>                    
                    <asp:TextBox ID="txtdescripcion" class="form-control" placeholder="Ingrese una descripción" runat="server"></asp:TextBox>
                </div>
              
                <div class="form-group">
                    <label for="precio">Precio unitario</label>
                    <asp:TextBox class="form-control" id="txtPrecio" placeholder="Ingrese el precio del articulo" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="stock">Stock</label>
                    <asp:TextBox class="form-control" id="txtStock" placeholder="Ingrese la cantidad de unidades a cargar" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="disponible">Disponible para venta</label>
                    <br />
                    <asp:CheckBox ID="chboxDisponible" Text="Esta disponible para venta?" runat="server"></asp:CheckBox>
                </div>

                  <div class="form-group">
                    <label for="tipo">Tipo de articulo</label>                   
                        <asp:DropDownList ID="ddlTipoArticulo" class="form-control" runat="server"></asp:DropDownList>                    
                </div>

                <div>
                    <asp:Button class="btn btn-default" Text="Guardar" runat="server"></asp:Button>
                </div>


                </div>
            </div>
         </div>



</asp:Content>
