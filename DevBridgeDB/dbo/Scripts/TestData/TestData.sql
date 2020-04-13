DELETE [dbo].[Assignments] WHERE [AsgnId] BETWEEN 1 AND 250;
DELETE [dbo].[Goals] WHERE [GoalId] BETWEEN 1 AND 50;
DELETE FROM [dbo].[Topics] WHERE [TopicId] BETWEEN 1 AND 50;
DELETE [dbo].[Users] WHERE [UserId] BETWEEN 1 AND 50;
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[Goals] OFF
SET IDENTITY_INSERT [dbo].[Assignments] OFF
SET IDENTITY_INSERT [dbo].[Goals] OFF

:r ./dbo.Users.data.sql
:r ./dbo.Topics.data.sql
:r ./dbo.Goals.data.sql
:r ./dbo.Assignments.data.sql