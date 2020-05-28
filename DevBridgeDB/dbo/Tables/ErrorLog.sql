CREATE TABLE [dbo].[ErrorLog]
(
	[LogId] INT NOT NULL IDENTITY,
	[ExceptionType] NVARCHAR(512) NOT NULL, 
    [ExceptionMessage] NVARCHAR(1024) NULL, 
    [StackTrace] NVARCHAR(MAX) NULL, 
    [Timestamp] DATETIME NOT NULL, 
    CONSTRAINT PK_ErrorLog PRIMARY KEY ([LogId])
)
