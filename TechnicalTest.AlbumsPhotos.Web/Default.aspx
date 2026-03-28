<%@ Page Title="Albums" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="TechnicalTest.AlbumsPhotos.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Albums</h1>
        <p>Listado de albums consumiendo API interna o API externa.</p>
    </div>

    <div class="row" style="margin-bottom:20px;">
        <div class="col-md-3">
            <label for="<%= ddlSource.ClientID %>">Fuente</label>
            <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control">
                <asp:ListItem Text="Interna" Value="internal" Selected="True" />
                <asp:ListItem Text="Externa" Value="external" />
            </asp:DropDownList>
        </div>

        <div class="col-md-5">
            <label for="<%= txtTitle.ClientID %>">Filtrar por título</label>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-2" style="margin-top:25px;">
            <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        </div>
    </div>

    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" EnableViewState="false" />

    <asp:GridView ID="gvAlbums"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped"
        EmptyDataText="No se encontraron albums."
        OnRowDataBound="gvAlbums_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:TemplateField HeaderText="Acción">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkPhotos" runat="server" Text="Ver fotos" CssClass="btn btn-default btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
