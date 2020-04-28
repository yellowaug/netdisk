CREATE TABLE [dbo].[FolderTable] (
    [Id]         INT           NOT NULL IDENTITY(1,1),
    [Netpath]    VARCHAR (50)  NULL,
    [FolderName] VARCHAR (200) NULL,
    [FtId]       INT           NULL,
    [CreateDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[UserTable] (
    [Id]         INT           NOT NULL IDENTITY(1,1),
    [Username]    VARCHAR (50)  NULL,
    [GroupName] VARCHAR (200) NULL,
    [Password]       INT           NULL,
    [Utid]   Int      NULL,
	[CreateDate] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);