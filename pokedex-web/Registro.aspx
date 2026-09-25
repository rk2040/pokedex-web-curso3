<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="pokedex_web.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-4">
            <h2>Crea tu perfil Trainee</h2>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server" />
            </div>
            <asp:Button Text="Registrarse" ID="btnRegistrarse" CssClass="btn btn-primary" OnClick="btnRegistrarse_Click" runat="server" />
            <asp:HyperLink NavigateUrl="/" Text="Cancelar" CssClass="btn btn-danger" runat="server" />
        </div>
    </div>

</asp:Content>
