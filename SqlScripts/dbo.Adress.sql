CREATE TABLE [dbo].[Adress]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [StreetLine1] NVARCHAR(255) NOT NULL, 
    [SreeetLine2] NVARCHAR(255) NOT NULL, 
    [City] NVARCHAR(255) NOT NULL, 
    [State] NVARCHAR(255) NOT NULL, 
    [UserId] INT NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Adress_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) 
)
