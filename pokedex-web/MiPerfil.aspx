<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="pokedex_web.MiPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Mi Perfil</h1>

    <div class="row">

        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Fecha de Nacimiento</label>
                <asp:TextBox ID="txtfechaNacimiento" TextMode="Date" CssClass="form-control" runat="server" />
            </div>
        </div>

        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Imagen Perfil</label>
                <input type="file" id="txtImagen" class="form-control" runat="server"  />
            </div>
            <asp:Image ID="imgNuevoPerfil" CssClass="img-fluid mb-3"  runat="server" />
        </div>

    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:Button ID="btnGuardar" Text="Guardar" Cssclass="btn btn-primary" OnClick="btnGuardar_Click" runat="server" />
            <asp:HyperLink NavigateUrl="/" Text="Regresar" CssClass="btn btn-danger" runat="server" />
        </div>
    </div>

</asp:Content>
