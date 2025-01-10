CREATE TABLE [dbo].[News]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [ArcticleLink] NVARCHAR(MAX) NOT NULL, 
    [GUID] NVARCHAR(MAX) NOT NULL, 
    [PubDate] DATETIME NOT NULL, 
    [MediaContent] NVARCHAR(MAX) NULL
)
