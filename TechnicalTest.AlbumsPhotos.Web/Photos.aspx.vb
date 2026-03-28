Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Linq
Imports System.Net.Http
Imports System.Web.Script.Serialization

Public Class Photos
    Inherits System.Web.UI.Page

    Private ReadOnly _internalApiBaseUrl As String = ConfigurationManager.AppSettings("InternalApiBaseUrl")
    Private ReadOnly _externalApiBaseUrl As String = ConfigurationManager.AppSettings("ExternalApiBaseUrl")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPhotos()
        End If
    End Sub

    Private Sub LoadPhotos()
        lblMessage.Text = String.Empty
        lblAlbumInfo.Text = String.Empty

        Dim albumIdValue As String = Request.QueryString("albumId")
        Dim titleValue As String = Request.QueryString("title")
        Dim source As String = Request.QueryString("source")

        If String.IsNullOrWhiteSpace(albumIdValue) Then
            lblMessage.Text = "No se recibió el albumId."
            Return
        End If

        Dim albumId As Integer
        If Not Integer.TryParse(albumIdValue, albumId) Then
            lblMessage.Text = "El albumId no es válido."
            Return
        End If

        If String.IsNullOrWhiteSpace(source) Then
            source = "internal"
        End If

        Try
            Dim photos As List(Of PhotoItem)

            If source = "external" Then
                photos = GetPhotosFromExternalApi(albumId)
            Else
                photos = GetPhotosFromInternalApi(albumId)
            End If

            lblAlbumInfo.Text = String.Format("Álbum seleccionado: {0} | Titulo: {1} | Fuente: {2}", albumId, titleValue, source)

            rptPhotos.DataSource = photos
            rptPhotos.DataBind()

            If photos Is Nothing OrElse photos.Count = 0 Then
                lblMessage.Text = "No se encontraron fotos para este álbum."
            End If

        Catch ex As Exception
            lblMessage.Text = "Ocurrió un error al cargar las fotos: " & ex.Message
        End Try
    End Sub

    Private Function GetPhotosFromInternalApi(albumId As Integer) As List(Of PhotoItem)
        Dim url As String = _internalApiBaseUrl.TrimEnd("/"c) & "/api/photos/search?albumId=" & albumId.ToString()

        Using client As New HttpClient()
            Dim json As String = client.GetStringAsync(url).Result
            Dim serializer As New JavaScriptSerializer()
            Return serializer.Deserialize(Of List(Of PhotoItem))(json)
        End Using
    End Function

    Private Function GetPhotosFromExternalApi(albumId As Integer) As List(Of PhotoItem)
        Dim url As String = _externalApiBaseUrl.TrimEnd("/"c) & "/photos?albumId=" & albumId.ToString()

        Using client As New HttpClient()
            Dim json As String = client.GetStringAsync(url).Result
            Dim serializer As New JavaScriptSerializer()
            Dim photos = serializer.Deserialize(Of List(Of PhotoItem))(json)

            Return photos.Select(Function(p) New PhotoItem With {
                .Id = p.Id,
                .AlbumId = p.AlbumId,
                .Title = p.ShortTitle,
                .Url = p.Url,
                .ThumbnailUrl = p.ThumbnailUrl
            }).ToList()
        End Using
    End Function

    Public Class PhotoItem
        Public Property Id As Integer
        Public Property AlbumId As Integer
        Public Property Title As String
        Public Property Url As String
        Public Property ThumbnailUrl As String

        Public ReadOnly Property ShortTitle As String
            Get
                If String.IsNullOrWhiteSpace(Title) Then
                    Return String.Empty
                End If

                If Title.Length <= 45 Then
                    Return Title
                End If

                Return Title.Substring(0, 45) & "..."
            End Get
        End Property
    End Class

End Class