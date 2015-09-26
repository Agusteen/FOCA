<%@ Page Title="" Language="C#" MasterPageFile="~/FOCAMasterPage.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="FOCA_gadgets_V1.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default" >
        <div class="panel-body">
            <div class="col-xs-5">
                <div class="form-group">
                    <label for="ejemplo_email_1">Email</label>
                    <input type="email" class="form-control" id="ejemplo_email_1"
                        placeholder="Introduce tu email"/>
                </div>

                <div class="form-group">
                    <label for="ejemplo_password_1">Contraseña</label>
                    <input type="password" class="form-control" id="ejemplo_password_1"
                        placeholder="Contraseña"/>
                </div>

                <div class="checkbox">
                    <label>
                        <input type="checkbox"/>
                        Activa esta casilla
                    </label>
                </div>
                <button type="submit" class="btn btn-default">Enviar</button>
            </div>
        </div>

    </div>
</asp:Content>
