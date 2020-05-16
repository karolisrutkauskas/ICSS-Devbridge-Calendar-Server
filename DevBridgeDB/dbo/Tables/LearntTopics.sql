CREATE TABLE [dbo].[LearntTopics]
(
	[TopicId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    CONSTRAINT PK_LearntTopics PRIMARY KEY (TopicId, UserId ASC),
    CONSTRAINT FK_LearntTopics_Topics FOREIGN KEY (TopicId) REFERENCES Topics (TopicId) ON DELETE CASCADE,
    CONSTRAINT FK_LearntTopics_Users FOREIGN KEY (UserId) REFERENCES Users (UserId) ON DELETE CASCADE
)
