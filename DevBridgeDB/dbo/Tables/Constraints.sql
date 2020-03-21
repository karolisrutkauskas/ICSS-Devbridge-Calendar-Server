CREATE TABLE [dbo].[Constraints]
(
	[ConstrId] INT NOT NULL IDENTITY, 
    [ConsecLimit] INT NULL, 
    [MonthLimit] INT NULL, 
    [YearLimit] INT NULL, 
    [TeamId] INT NULL, 
    [UserId] INT NULL, 
    [Global] BIT NOT NULL
    CONSTRAINT [PK_Constraints_ConstrId] PRIMARY KEY ([ConstrId] ASC)
    CONSTRAINT [FK_Constraints_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([TeamId]) ON DELETE CASCADE
    CONSTRAINT [FK_Constraints_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_Constraints_TeamId] ON [dbo].[Constraints] ([TeamId])
