Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Linq
Imports System.Net.Http
Imports System.Web.Script.Serialization
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class _Default
    Inherits Page

    Private ReadOnly _internalApiBaseUrl As String = ConfigurationManager.AppSettings("InternalApiBaseUrl")
    Private ReadOnly _externalApiBaseUrl As String = ConfigurationManager.AppSettings("ExternalApiBaseUrl")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadAlbums()
        End If
    End Sub

    Protected Sub BtnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        LoadAlbums()
    End Sub

    Private Sub LoadAlbums()
        lblMessage.Text = String.Empty

        Try
            Dim source As String = ddlSource.SelectedValue
            Dim titleFilter As String = txtTitle.Text.Trim()
            Dim albums As List(Of AlbumItem)

            If source = "internal" Then
                albums = GetAlbumsFromInternalApi(titleFilter)
            Else
                albums = GetAlbumsFromExternalApi(titleFilter)
            End If

            gvAlbums.DataSource = albums
            gvAlbums.DataBind()

        Catch ex As Exception
            gvAlbums.DataSource = Nothing
            gvAlbums.DataBind()
            lblMessage.Text = "Ocurrió un error al cargar los albums: " & ex.Message
        End Try
    End Sub

    Private Function GetAlbumsFromInternalApi(titleFilter As String) As List(Of AlbumItem)
        Dim url As String = _internalApiBaseUrl.TrimEnd("/"c) & "/api/albums"

        If Not String.IsNullOrWhiteSpace(titleFilter) Then
            url &= "/search?title=" & Uri.EscapeDataString(titleFilter)
        End If

        Using client As New HttpClient()
            Dim json As String = client.GetStringAsync(url).Result
            Dim serializer As New JavaScriptSerializer()
            Return serializer.Deserialize(Of List(Of AlbumItem))(json)
        End Using
    End Function

    Private Function GetAlbumsFromExternalApi(titleFilter As String) As List(Of AlbumItem)
        Dim url As String = _externalApiBaseUrl.TrimEnd("/"c) & "/albums"

        Using client As New HttpClient()
            Dim json As String = client.GetStringAsync(url).Result
            Dim serializer As New JavaScriptSerializer()
            Dim albums = serializer.Deserialize(Of List(Of AlbumItem))(json)

            If String.IsNullOrWhiteSpace(titleFilter) Then
                Return albums
            End If

            Return albums.
                Where(Function(a) Not String.IsNullOrWhiteSpace(a.Title) AndAlso
                a.Title.IndexOf(titleFilter, StringComparison.OrdinalIgnoreCase) >= 0).
                ToList()
        End Using
    End Function

    Protected Sub gvAlbums_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType <> DataControlRowType.DataRow Then
            Return
        End If

        Dim album As AlbumItem = CType(e.Row.DataItem, AlbumItem)
        Dim lnkPhotos As HyperLink = CType(e.Row.FindControl("lnkPhotos"), HyperLink)

        If lnkPhotos IsNot Nothing AndAlso album IsNot Nothing Then
            Dim source As String = ddlSource.SelectedValue
            lnkPhotos.NavigateUrl = String.Format("~/Photos.aspx?albumId={0}&title={1}&source={2}", album.Id, album.Title, source)
        End If
    End Sub

    Public Class AlbumItem
        Public Property Id As Integer
        Public Property UserId As Integer
        Public Property Title As String
    End Class

End Class