<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="ABMC_Clientes.aspx.cs" Inherits="FOCA_gadgets_V1.ABMC_Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">      
    <script src="js/jquery-1.11.3.min.js" ></script>
    <script src="js/jquery-birthday-picker.min.js" ></script>        
    <script>
        $(document).ready(function () {
            $('#long-month-birthday').birthdayPicker({
                "defaultDate": false,
                "minAge": 0,
                "maxAge": 120,
                "monthFormat": ">long",
                "dateFormat": "littleEndian",
                "sizeClass": "span3"              
            });         
        });
    </script>

    <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-xs-4">
                <div class="form-group">
                    <label for="nombre">Nombre</label>
                    <asp:TextBox ID="txtNombre" class="form-control" placeholder="Ingrese aquí su nombre" runat="server" Font-Size="Large"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="apellido">Apellido</label>
                    <asp:TextBox class="form-control" ID="txtApellido" placeholder="Ingrese aquí su apellido" runat="server" Font-Size="Large"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="dni">Número de documento</label>
                    <asp:TextBox class="form-control" ID="txtDni" name="txtDni" placeholder="Ingrese solo números, sin puntos" runat="server" onkeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" Font-Size="Large" />
                    <asp:RegularExpressionValidator ControlToValidate="txtDni" ValidationExpression="[0-9]{8}|[0-9]{7}" runat="server" ErrorMessage="* Debe tener como máximo 8 dígitos y como mínimo 7 dígitos" Font-Size="X-Small" ForeColor="Red"></asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <label for="domicilio">Domicilio</label>
                    <asp:TextBox class="form-control" ID="txtDomicilio" placeholder="Calle Numero Piso" runat="server" Font-Size="Large"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="localidad">Localidad</label>
                    <asp:DropDownList ID="ddlLocalidades" class="form-control" runat="server" Font-Size="Large"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <label for="telefono">Telefono</label>
                    <asp:TextBox ID="txtTelefono" class="form-control" placeholder="Ingrese solo números, sin signos" runat="server" Font-Size="Large"></asp:TextBox>
                    
                </div>

                <div class="form-group">
                    <label for="fechaNacimiento">Fecha de nacimiento</label>
                    <%-- <asp:TextBox ID="txtFechaNacimiento" class="birthdayPicker" placeholder="Fecha de Nacimiento" runat="server" onkeypress="event.returnValue = false;"></asp:TextBox>
                        <asp:RangeValidator ErrorMessage="* Debe ser mayor de 18 años" ControlToValidate="txtFechaNacimiento" runat="server" Font-Size="X-Small" ForeColor="Red" OnInit="rangeValidator_Init" Type="Date" />--%>
                    
                  <div id="long-month-birthday"></div>                                 
                    <%--<asp:TextBox ID="long-month-birthday" runat="server"></asp:TextBox>--%>                
                    
                    
                </div>                                        
                
                <div class="form-group">
                    <%--<label for="preferencial">Preferencial</label>
                    <br />
                    <asp:CheckBox type="checkbox-info" ID="chboxPreferencial" Text="Es cliente VIP" runat="server"></asp:CheckBox>--%>
                    <%--<input type="checkbox" name="fancy-checkbox-default" id="fancy-checkbox-default" runat="server" />  --%>
                    <asp:CheckBox ID="chboxPreferencial" name="fancy-checkbox-default" class="fancy-checkbox-default" runat="server"></asp:CheckBox>                 
                    <div class="[ btn-group ]">
                        <label for="fancy-checkbox-default" class="[ btn btn-default active ]">Preferencial</label>
                        <label for="fancy-checkbox-default" class="[ btn btn-default ]">
                            <span class="[ glyphicon glyphicon-ok ]"></span>
                            <span></span>
                        </label>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Button class="btn btn-default" Text="Guardar" runat="server" OnClick="enviar"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
