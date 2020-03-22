CREATE TABLE [dbo].[TeamManagers]
(
	[TeamId] INT NOT NULL,
	[ManagerId] INT NOT NULL
	CONSTRAINT [PK_TeamManagers_TeamId] PRIMARY KEY (TeamId ASC)
	CONSTRAINT [UQ_TeamManagers_ManagerId] UNIQUE (ManagerId)
    CONSTRAINT [FK_TeamManagers_Teams] FOREIGN KEY ([TeamId]) REFERENCES [Teams]([TeamId]) ON DELETE CASCADE
	CONSTRAINT [FK_TeamManagers_Users] FOREIGN KEY ([ManagerId]) REFERENCES [Users]([UserId]) ON DELETE NO ACTION -- Can't have team with no manager
)

GO

CREATE UNIQUE INDEX [IX_TeamManagers_ManagerId] ON [dbo].[TeamManagers] ([ManagerId])

