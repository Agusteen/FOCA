<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FOCA_gadgets_V1.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-xs-12">
        <asp:Login ID="login" runat="server" OnAuthenticate="Login_Authenticate">
        </asp:Login>
    </div>
</asp:Content>
