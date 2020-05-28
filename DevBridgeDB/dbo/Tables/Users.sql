CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL IDENTITY,
	[FirstName] NVARCHAR(200) NOT NULL,
	[LastName] NVARCHAR(200) NOT NULL,
	[Email] NVARCHAR(200) NOT NULL, 
    [Role] NVARCHAR(50) NULL, 
    [Password] NVARCHAR(200) NULL,
	[ManagerId] INT NULL,
    [ConsecLimit] INT NULL, 
    [MonthlyLimit] INT NULL,
	[QuarterLimit] INT NULL,
    [YearlyLimit] INT NULL,
	[RegistrationToken] NVARCHAR(200) NULL
	CONSTRAINT [PK_Users_UserID] PRIMARY KEY ([UserId] ASC)
	CONSTRAINT [UQ_Users_Email] UNIQUE ([Email])
	CONSTRAINT [FK_Users_Manager] FOREIGN KEY ([ManagerId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION 
)

GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [dbo].[Users] ([Email])

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_RegistrationToken]
ON [Users]([RegistrationToken])
WHERE [RegistrationToken] IS NOT NULL;