USE TechnicalTestAlbumsPhotos;
GO

INSERT INTO dbo.Albums (Id, UserId, Title)
VALUES
(1, 1, N'Álbum de prueba 1'),
(2, 1, N'Álbum de prueba 2');
GO

INSERT INTO dbo.Photos (Id, AlbumId, Title, Url, ThumbnailUrl)
VALUES
(1, 1, N'Foto de montaña', N'https://picsum.photos/id/10/600/600', N'https://picsum.photos/id/10/150/150'),
(2, 1, N'Foto de bosque',   N'https://picsum.photos/id/20/600/600', N'https://picsum.photos/id/20/150/150'),
(3, 1, N'Foto de río',      N'https://picsum.photos/id/30/600/600', N'https://picsum.photos/id/30/150/150'),
(4, 2, N'Foto urbana',      N'https://picsum.photos/id/40/600/600', N'https://picsum.photos/id/40/150/150'),
(5, 2, N'Foto de playa',    N'https://picsum.photos/id/50/600/600', N'https://picsum.photos/id/50/150/150'),
(6, 2, N'Foto nocturna',    N'https://picsum.photos/id/60/600/600', N'https://picsum.photos/id/60/150/150');
GO