<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FOCA_gadgets_V1.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-xs-8 col-lg-offset-2"  >
    <div class="panel panel-default" >
  <div class="panel-heading">
    <h3 class="panel-title">Iniciar sesion</h3>
  </div>
  <div class="panel-body" >
      
    <asp:Login TextBoxStyle-CssClass="form-control" LoginButtonStyle-CssClass="btn btn-default" ID="login" runat="server" OnAuthenticate="Login_Authenticate" Height="100px" PasswordLabelText="Contraseña" RememberMeText="Recordármelo la próxima vez" TextLayout="TextOnTop" TitleText="" UserNameLabelText="Mail" Width="500px">
        <CheckBoxStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" />
<LoginButtonStyle CssClass="btn btn-default btn-lg"></LoginButtonStyle>

<TextBoxStyle CssClass="form-control"></TextBoxStyle>
        </asp:Login>
          
  </div>
</div>
    </div>
        
    
</asp:Content>
