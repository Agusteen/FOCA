<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="ABMC_Clientes.aspx.cs" Inherits="FOCA_gadgets_V1.ABMC_Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
    <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-xs-5">
                <div class="form-group">
                    <label for="nombre">Nombre</label>                    
                    <asp:TextBox ID="txtNombre" class="form-control" placeholder="Nombre" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="apellido">Apellido</label>
                    <asp:TextBox class="form-control" id="txtApellido" placeholder="Apellido" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="dni">Número de documento</label>
                    <asp:TextBox class="form-control" id="txtDni" name="txtDni" placeholder="Número de documento" runat="server" onkeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" />
                    <asp:RegularExpressionValidator ControlToValidate="txtDni" ValidationExpression="[0-9]{8}|[0-9]{7}" runat="server" ErrorMessage="* Debe tener como máximo 8 dígitos y como mínimo 7 dígitos" Font-Size="Small" ForeColor="Red" ></asp:RegularExpressionValidator>
                </div>

                             

                <div class="form-group">
                    <label for="domicilio">Domicilio</label>
                    <asp:TextBox class="form-control" id="txtDomicilio" placeholder="Domicilio" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="localidad">Localidad</label>                   
                        <asp:DropDownList ID="ddlLocalidades" class="form-control" runat="server"></asp:DropDownList>                    
                </div>

                <asp:Button class="btn btn-default" Text="Aceptar" runat="server" OnClick="enviar"></asp:Button>
            </div>

        </div>
    </div>

</asp:Content>
