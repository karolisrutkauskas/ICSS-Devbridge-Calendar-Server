CREATE TABLE [dbo].[Goals]
(
	[GoalId] INT NOT NULL IDENTITY, 
    [UserId] INT NULL, 
    [TeamManagerId] INT NULL, 
    [TopicId] INT NOT NULL,
    [Role] NVARCHAR(50) NULL, 
    [Deadline] DATE NULL
    CONSTRAINT [PK_Goals_GoalId] PRIMARY KEY ([GoalId] ASC)
    CONSTRAINT [FK_Goals_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]) ON DELETE CASCADE
    CONSTRAINT [FK_Goals_Team] FOREIGN KEY ([TeamManagerId]) REFERENCES [Users]([UserId]) ON DELETE NO ACTION
    CONSTRAINT [FK_Goals_Topics] FOREIGN KEY ([TopicId]) REFERENCES [Topics]([TopicId]) ON DELETE CASCADE
)

--TODO: Add more indexes
