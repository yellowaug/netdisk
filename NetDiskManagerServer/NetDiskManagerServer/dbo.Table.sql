CREATE TABLE [dbo].[FolderTable]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Netpath] VARCHAR(50) NULL, 
    [FolderName] VARCHAR(200) NULL, 
    [FtId] INT NULL, 
    [CreateDate] DATETIME NULL
)
