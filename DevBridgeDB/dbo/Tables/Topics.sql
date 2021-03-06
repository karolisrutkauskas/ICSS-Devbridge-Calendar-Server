﻿CREATE TABLE [dbo].[Topics]
(
	[TopicId] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(200) NOT NULL, 
    [Description] NVARCHAR(4000) NULL, 
    [ParentTopicId] INT NULL, 
    [ChangeByUserId] INT NOT NULL,
    [SysStart] DATETIME2 (7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEnd] DATETIME2 (7) GENERATED ALWAYS AS ROW END NOT NULL,
    PERIOD FOR SYSTEM_TIME ([SysStart], [SysEnd]),
    CONSTRAINT [PK_Topics_TopicId] PRIMARY KEY ([TopicId]),
    CONSTRAINT [FK_PrevTopic] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([TopicId])
    
) WITH (SYSTEM_VERSIONING = ON(HISTORY_TABLE=[dbo].[Topics_HISTORY], DATA_CONSISTENCY_CHECK=ON))

GO

CREATE INDEX [IX_Topics_PrevTopic] ON [dbo].[Topics] ([ParentTopicId])