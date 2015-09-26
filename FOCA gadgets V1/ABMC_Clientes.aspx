<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="ABMC_Clientes.aspx.cs" Inherits="FOCA_gadgets_V1.ABMC_Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default" >
        <div class="panel-body">
            <div class="col-xs-5">
                <div class="form-group">                    
                    <label for="nombre">Nombre</label>
                   <%-- <input type="text" class="form-control" name="txtNombre"  runat="server"/>--%>
                    <asp:TextBox ID="txtNombre"  class="form-control" placeholder="Nombre" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="apellido">Apellido</label>
                    <input type="text"  class="form-control" id="txtApellido" name="txtApellido" placeholder="Apellido" runat="server"/>
                </div>

                <div class="form-group">
                    <label for="dni">Número de documento</label>
                    <input type="text" class="form-control" id="txtDni" name="txtDni" placeholder="Número de documento" runat="server"/>
                </div>
                 <div class="form-group">
                    <label for="domicilio">Domicilio</label>
                    <input type="text" class="form-control" id="txtDomicilio" name="txtDomicilio" placeholder="Domicilio" runat="server"/>
                </div>

                <div class="form-group">
                    <label for="localidad">Localidad</label>
                    <div class="form-group">
                        <select class="form-control" id="Localidad" name="Localidad" runat="server">
                            <option value="white">White</option>
                        </select>
                    </div>
                </div>
            

               
                

                  



                <button type="submit" class="btn btn-default">Enviar</button>
            </div>
            
        </div>
        </div>

</asp:Content>
