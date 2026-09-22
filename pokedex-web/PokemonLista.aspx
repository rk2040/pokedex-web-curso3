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
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox Text="Filtro Avanzado" CssClass="" ID="chkAvanzado" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" runat="server" />
            </div>
        </div>

        <%if (FiltroAvanzado)
            { %>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Campo" runat="server" />
                    <asp:DropDownList ID="ddlCampo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" runat="server">
                        <asp:ListItem Text="Nombre" />
                        <asp:ListItem Text="Tipo" />
                        <asp:ListItem Text="Número" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Criterio" runat="server" />
                    <asp:DropDownList ID="ddlCriterio" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Filtro" runat="server" />
                    <asp:TextBox ID="txtFiltroAvanzado" CssClass="form-control" runat="server" />
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Estado" runat="server" />
                    <asp:DropDownList ID="ddlEstado" CssClass="form-control" runat="server">
                        <asp:ListItem Text="Todos" />
                        <asp:ListItem Text="Activo" />
                        <asp:ListItem Text="Inactivo" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Button ID="btnBuscar" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" runat="server" />
                </div>
            </div>
        </div>
        <%  } %>

        <%-- Con los datoa ya cargados va poniendo lo que va en cada pagina que vamos pasando --%>
        <%-- para poder poner un limite por pagina --%>
    </div>

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

</asp:Content>
