<%@ Page Title="Photos" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photos.aspx.vb" Inherits="TechnicalTest.AlbumsPhotos.Web.Photos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Photos</h1>
        <p>Fotos del álbum seleccionado.</p>
    </div>

    <asp:Label ID="lblAlbumInfo" runat="server" CssClass="h4" />
    <br />
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" EnableViewState="false" />
    <br />
    <asp:Repeater ID="rptPhotos" runat="server">
        <HeaderTemplate>
            <div class="row">
        </HeaderTemplate>

        <%--        <ItemTemplate>
            <div class="col-md-4" style="margin-bottom:25px;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <strong><%# Eval("Title") %></strong>
                    </div>
                    <div class="panel-body text-center">
                        <img src='<%# Eval("ThumbnailUrl") %>'
                             alt='<%# Eval("Title") %>'
                             style="max-width:150px; max-height:150px;" />
                    </div>
                    <div class="panel-footer text-center">
                        <a href='<%# Eval("Url") %>' target="_blank">Ver imagen</a>
                    </div>
                </div>
            </div>
        </ItemTemplate>--%>

        <ItemTemplate>
            <div class="col-md-4" style="margin-bottom: 25px;">
                <div class="panel panel-default" style="min-height: 360px;">

                    <div class="panel-heading" style="height: 70px; overflow: hidden;">
                        <strong><%# Eval("ShortTitle") %></strong>
                    </div>

                    <div class="panel-body text-center" style="height: 180px; display: flex; align-items: center; justify-content: center; flex-direction: column;">
                        <img src='<%# Eval("ThumbnailUrl") %>'
                            alt='<%# Eval("Title") %>'
                            style="max-width: 150px; max-height: 150px;"
                            onerror="this.style.display='none'; this.nextElementSibling.style.display='block';" />

                        <div style="display: none; color: #777; font-style: italic;">
                            Imagen no disponible
               
                        </div>
                    </div>

                    <div class="panel-footer text-center">
                        <a href='<%# Eval("Url") %>' target="_blank">Ver imagen</a>
                    </div>

                </div>
            </div>
        </ItemTemplate>

        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <p>
        <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Default.aspx" Text="Volver a Albums" />
    </p>

</asp:Content>
