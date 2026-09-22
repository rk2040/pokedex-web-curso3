<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PokemonLista.aspx.cs" Inherits="pokedex_web.PokemonLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />
    <h1>Lista Pokemon </h1>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label Text="Filtrar" runat="server" />
                <asp:TextBox ID="txtFiltro" AutoPostBack="true" OnTextChanged="filtro_TextChanged" CssClass="form-control" runat="server" />
            </div>
        </div>
        <%-- Con los datoa ya cargados va poniendo lo que va en cada pagina que vamos pasando --%>
        <%-- para poder poner un limite por pagina --%>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="mb-3">
                    <asp:GridView ID="dgvPokemons" CssClass="table" AutoGenerateColumns="false"
                        DataKeyNames="Id" OnSelectedIndexChanged="dgvPokemons_SelectedIndexChanged" OnPageIndexChanging="dgvPokemons_PageIndexChanging" AllowPaging="true" PageSize="5" runat="server">

                        <Columns>
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                            <asp:BoundField HeaderText="Número" DataField="Numero" />
                            <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
                            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
                            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="👉​" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HyperLink NavigateUrl="FormularioPokemon.aspx" Text="Agregar" CssClass="btn btn-primary" runat="server" />

    </div>
</asp:Content>
