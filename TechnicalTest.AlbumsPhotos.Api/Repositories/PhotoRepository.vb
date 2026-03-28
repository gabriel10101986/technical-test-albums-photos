Imports System.Data.SqlClient

Public Class PhotoRepository

    Private ReadOnly _connectionString As String

    Public Sub New()
        _connectionString = ConfigurationManager.ConnectionStrings("TechnicalTestDb").ConnectionString
    End Sub

    Public Function GetAll() As List(Of Photo)
        Dim photos As New List(Of Photo)

        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "SELECT Id, AlbumId, Title, Url, ThumbnailUrl FROM dbo.Photos"

            Using command As New SqlCommand(query, connection)
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim photo As New Photo With {
                            .Id = Convert.ToInt32(reader("Id")),
                            .AlbumId = Convert.ToInt32(reader("AlbumId")),
                            .Title = reader("Title").ToString(),
                            .Url = reader("Url").ToString(),
                            .ThumbnailUrl = reader("ThumbnailUrl").ToString()
                        }

                        photos.Add(photo)
                    End While
                End Using
            End Using
        End Using

        Return photos
    End Function

    Public Function GetById(id As Integer) As Photo
        Dim photo As Photo = Nothing

        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "SELECT Id, AlbumId, Title, Url, ThumbnailUrl FROM dbo.Photos WHERE Id = @Id"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Id", id)

                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        photo = New Photo With {
                            .Id = Convert.ToInt32(reader("Id")),
                            .AlbumId = Convert.ToInt32(reader("AlbumId")),
                            .Title = reader("Title").ToString(),
                            .Url = reader("Url").ToString(),
                            .ThumbnailUrl = reader("ThumbnailUrl").ToString()
                        }
                    End If
                End Using
            End Using
        End Using

        Return photo
    End Function

    Public Function Search(albumId As Integer?, title As String) As List(Of Photo)
        Dim photos As New List(Of Photo)

        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "SELECT Id, AlbumId, Title, Url, ThumbnailUrl FROM dbo.Photos WHERE 1 = 1"

            If albumId.HasValue Then
                query &= " AND AlbumId = @AlbumId"
            End If

            If Not String.IsNullOrWhiteSpace(title) Then
                query &= " AND Title LIKE @Title"
            End If

            Using command As New SqlCommand(query, connection)
                If albumId.HasValue Then
                    command.Parameters.AddWithValue("@AlbumId", albumId.Value)
                End If

                If Not String.IsNullOrWhiteSpace(title) Then
                    command.Parameters.AddWithValue("@Title", "%" & title & "%")
                End If

                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim photo As New Photo With {
                            .Id = Convert.ToInt32(reader("Id")),
                            .AlbumId = Convert.ToInt32(reader("AlbumId")),
                            .Title = reader("Title").ToString(),
                            .Url = reader("Url").ToString(),
                            .ThumbnailUrl = reader("ThumbnailUrl").ToString()
                        }

                        photos.Add(photo)
                    End While
                End Using
            End Using
        End Using

        Return photos
    End Function

    Public Sub Create(photo As Photo)
        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "INSERT INTO dbo.Photos (Id, AlbumId, Title, Url, ThumbnailUrl) VALUES (@Id, @AlbumId, @Title, @Url, @ThumbnailUrl)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Id", photo.Id)
                command.Parameters.AddWithValue("@AlbumId", photo.AlbumId)
                command.Parameters.AddWithValue("@Title", photo.Title)
                command.Parameters.AddWithValue("@Url", photo.Url)
                command.Parameters.AddWithValue("@ThumbnailUrl", photo.ThumbnailUrl)

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function Update(id As Integer, photo As Photo) As Boolean
        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "UPDATE dbo.Photos SET AlbumId = @AlbumId, Title = @Title, Url = @Url, ThumbnailUrl = @ThumbnailUrl WHERE Id = @Id"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Id", id)
                command.Parameters.AddWithValue("@AlbumId", photo.AlbumId)
                command.Parameters.AddWithValue("@Title", photo.Title)
                command.Parameters.AddWithValue("@Url", photo.Url)
                command.Parameters.AddWithValue("@ThumbnailUrl", photo.ThumbnailUrl)

                connection.Open()
                Dim rowsAffected = command.ExecuteNonQuery()

                Return rowsAffected > 0
            End Using
        End Using
    End Function

    Public Function Delete(id As Integer) As Boolean
        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "DELETE FROM dbo.Photos WHERE Id = @Id"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Id", id)

                connection.Open()
                Dim rowsAffected = command.ExecuteNonQuery()

                Return rowsAffected > 0
            End Using
        End Using
    End Function

End Class