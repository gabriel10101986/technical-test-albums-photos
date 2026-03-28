Imports System.Web.Http

Public Class PhotosController
    Inherits ApiController

    Private ReadOnly _photoRepository As PhotoRepository

    Public Sub New()
        _photoRepository = New PhotoRepository()
    End Sub

    <HttpGet>
    <Route("api/photos")>
    Public Function GetPhotos() As IHttpActionResult
        Dim photos = _photoRepository.GetAll()
        Return Ok(photos)
    End Function

    <HttpGet>
    <Route("api/photos/{id:int}")>
    Public Function GetPhotoById(id As Integer) As IHttpActionResult
        Dim photo = _photoRepository.GetById(id)

        If photo Is Nothing Then
            Return NotFound()
        End If

        Return Ok(photo)
    End Function

    '<HttpGet>
    '<Route("api/photos/search")>
    'Public Function SearchPhotos(albumId As Integer?, title As String) As IHttpActionResult
    '    Dim photos = _photoRepository.Search(albumId, title)
    '    Return Ok(photos)
    'End Function

    <HttpGet>
    <Route("api/photos/search")>
    Public Function SearchPhotos(Optional albumId As Integer? = Nothing, Optional title As String = Nothing) As IHttpActionResult
        Dim photos = _photoRepository.Search(albumId, title)
        Return Ok(photos)
    End Function

    <HttpPost>
    <Route("api/photos")>
    Public Function CreatePhoto(<FromBody> photo As Photo) As IHttpActionResult
        If photo Is Nothing Then
            Return BadRequest("La foto es requerida.")
        End If

        _photoRepository.Create(photo)
        Return Ok()
    End Function

    <HttpPut>
    <Route("api/photos/{id:int}")>
    Public Function UpdatePhoto(id As Integer, <FromBody> photo As Photo) As IHttpActionResult
        If photo Is Nothing Then
            Return BadRequest("La foto es requerida.")
        End If

        Dim updated = _photoRepository.Update(id, photo)

        If Not updated Then
            Return NotFound()
        End If

        Return Ok()
    End Function

    <HttpDelete>
    <Route("api/photos/{id:int}")>
    Public Function DeletePhoto(id As Integer) As IHttpActionResult
        Dim deleted = _photoRepository.Delete(id)

        If Not deleted Then
            Return NotFound()
        End If

        Return Ok()
    End Function
End Class