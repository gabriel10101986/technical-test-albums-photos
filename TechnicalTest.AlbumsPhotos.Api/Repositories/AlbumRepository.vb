Imports System.Data.SqlClient

Public Class AlbumRepository

    Private ReadOnly _connectionString As String

    Public Sub New()
        _connectionString = ConfigurationManager.ConnectionStrings("TechnicalTestDb").ConnectionString
    End Sub

    Public Function GetAll() As List(Of Album)
        Dim albums As New List(Of Album)

        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "SELECT Id, UserId, Title FROM dbo.Albums"

            Using command As New SqlCommand(query, connection)
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim album As New Album With {
                            .Id = Convert.ToInt32(reader("Id")),
                            .UserId = Convert.ToInt32(reader("UserId")),
                            .Title = reader("Title").ToString()
                        }

                        albums.Add(album)
                    End While
                End Using
            End Using
        End Using

        Return albums
    End Function

    Public Function GetById(id As Integer) As Album
        Dim album As Album = Nothing

        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "SELECT Id, UserId, Title FROM dbo.Albums WHERE Id = @Id"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Id", id)

                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        album = New Album With {
                            .Id = Convert.ToInt32(reader("Id")),
                            .UserId = Convert.ToInt32(reader("UserId")),
                            .Title = reader("Title").ToString()
                        }
                    End If
                End Using
            End Using
        End Using

        Return album
    End Function

    Public Function SearchByTitle(title As String) As List(Of Album)
        Dim albums As New List(Of Album)

        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "SELECT Id, UserId, Title FROM dbo.Albums WHERE Title LIKE @Title"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Title", "%" & title & "%")

                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim album As New Album With {
                            .Id = Convert.ToInt32(reader("Id")),
                            .UserId = Convert.ToInt32(reader("UserId")),
                            .Title = reader("Title").ToString()
                        }

                        albums.Add(album)
                    End While
                End Using
            End Using
        End Using

        Return albums
    End Function

    Public Sub Create(album As Album)
        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "INSERT INTO dbo.Albums (Id, UserId, Title) VALUES (@Id, @UserId, @Title)"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Id", album.Id)
                command.Parameters.AddWithValue("@UserId", album.UserId)
                command.Parameters.AddWithValue("@Title", album.Title)

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function Update(id As Integer, album As Album) As Boolean
        Using connection As New SqlConnection(_connectionString)
            Dim query As String = "UPDATE dbo.Albums SET UserId = @UserId, Title = @Title WHERE Id = @Id"

            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Id", id)
                command.Parameters.AddWithValue("@UserId", album.UserId)
                command.Parameters.AddWithValue("@Title", album.Title)

                connection.Open()
                Dim rowsAffected = command.ExecuteNonQuery()

                Return rowsAffected > 0
            End Using
        End Using
    End Function

    Public Function Delete(id As Integer) As Boolean
        Using connection As New SqlConnection(_connectionString)
            connection.Open()

            Using transaction = connection.BeginTransaction()
                Try
                    Dim deletePhotosQuery As String = "DELETE FROM dbo.Photos WHERE AlbumId = @AlbumId"
                    Using deletePhotosCommand As New SqlCommand(deletePhotosQuery, connection, transaction)
                        deletePhotosCommand.Parameters.AddWithValue("@AlbumId", id)
                        deletePhotosCommand.ExecuteNonQuery()
                    End Using

                    Dim deleteAlbumQuery As String = "DELETE FROM dbo.Albums WHERE Id = @Id"
                    Using deleteAlbumCommand As New SqlCommand(deleteAlbumQuery, connection, transaction)
                        deleteAlbumCommand.Parameters.AddWithValue("@Id", id)
                        Dim rowsAffected = deleteAlbumCommand.ExecuteNonQuery()

                        transaction.Commit()
                        Return rowsAffected > 0
                    End Using

                Catch
                    transaction.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Function

End Class
