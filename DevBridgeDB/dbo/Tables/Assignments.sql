CREATE TABLE [dbo].[Assignments]
(
	[AsgnId] INT NOT NULL IDENTITY, 
    [UserId] INT NOT NULL, 
    [TopicId] INT NOT NULL,
    [State] NVARCHAR(50) NOT NULL,
    [Comments] NVARCHAR(2000) NULL, 
    [Date] DATE NOT NULL
    CONSTRAINT [PK_Assignments_AsgnId] PRIMARY KEY ([AsgnId] ASC)
    CONSTRAINT [UQ_Assignments_UserTopic] UNIQUE ([UserId], [TopicId], [Date])
    CONSTRAINT [FK_Assignments_Users] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    CONSTRAINT [FK_Assignments_Topics] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([TopicId]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_Assignments_Calendar] ON [dbo].[Assignments] ([UserId], [Date])
