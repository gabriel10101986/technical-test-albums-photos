CREATE DATABASE TechnicalTestAlbumsPhotos;
GO

IF DB_ID('TechnicalTestAlbumsPhotos') IS NULL
BEGIN
    CREATE DATABASE TechnicalTestAlbumsPhotos;
END
GO

CREATE TABLE dbo.Albums
(
    Id INT NOT NULL PRIMARY KEY,
    UserId INT NOT NULL,
    Title NVARCHAR(200) NOT NULL
);
GO

CREATE TABLE dbo.Photos
(
    Id INT NOT NULL PRIMARY KEY,
    AlbumId INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Url NVARCHAR(500) NOT NULL,
    ThumbnailUrl NVARCHAR(500) NOT NULL,
    CONSTRAINT FK_Photos_Albums
        FOREIGN KEY (AlbumId) REFERENCES dbo.Albums(Id)
);
GO