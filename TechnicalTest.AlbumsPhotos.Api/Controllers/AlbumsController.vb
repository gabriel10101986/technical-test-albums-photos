Imports System.Web.Http

Public Class AlbumsController
    Inherits ApiController

    Private ReadOnly _albumRepository As AlbumRepository

    Public Sub New()
        _albumRepository = New AlbumRepository()
    End Sub

    <HttpGet>
    <Route("api/albums")>
    Public Function GetAlbums() As IHttpActionResult
        Dim albums = _albumRepository.GetAll()
        Return Ok(albums)
    End Function

    <HttpGet>
    <Route("api/albums/{id:int}")>
    Public Function GetAlbumById(id As Integer) As IHttpActionResult
        Dim album = _albumRepository.GetById(id)

        If album Is Nothing Then
            Return NotFound()
        End If

        Return Ok(album)
    End Function

    <HttpGet>
    <Route("api/albums/search")>
    Public Function SearchAlbumsByTitle(title As String) As IHttpActionResult
        Dim albums = _albumRepository.SearchByTitle(title)
        Return Ok(albums)
    End Function

    <HttpPost>
    <Route("api/albums")>
    Public Function CreateAlbum(<FromBody> album As Album) As IHttpActionResult
        If album Is Nothing Then
            Return BadRequest("El álbum es requerido.")
        End If

        _albumRepository.Create(album)

        Return Ok()
    End Function

    <HttpPut>
    <Route("api/albums/{id:int}")>
    Public Function UpdateAlbum(id As Integer, <FromBody> album As Album) As IHttpActionResult
        If album Is Nothing Then
            Return BadRequest("El álbum es requerido.")
        End If

        Dim updated = _albumRepository.Update(id, album)

        If Not updated Then
            Return NotFound()
        End If

        Return Ok()
    End Function

    <HttpDelete>
    <Route("api/albums/{id:int}")>
    Public Function DeleteAlbum(id As Integer) As IHttpActionResult
        Dim deleted = _albumRepository.Delete(id)

        If Not deleted Then
            Return NotFound()
        End If

        Return Ok()
    End Function

End Class