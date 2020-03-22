CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL IDENTITY,
	[FirstName] NVARCHAR(200) NOT NULL,
	[LastName] NVARCHAR(200) NOT NULL,
	[Email] NVARCHAR(200) NOT NULL, 
    [Role] NVARCHAR(50) NULL, 
    [Password] NVARCHAR(200) NOT NULL,
	[TeamId] INT NULL
	CONSTRAINT [PK_Users_UserID] PRIMARY KEY ([UserId] ASC)
	CONSTRAINT [UQ_Users_Email] UNIQUE ([Email])
)

GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [dbo].[Users] ([Email])