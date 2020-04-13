CREATE TABLE [dbo].[AssignmentStates]
(
	[StateId] INT NOT NULL,
    [State] NVARCHAR(2000) NOT NULL
	CONSTRAINT [PK_AssignmentStates_StateId] PRIMARY KEY ([StateId] ASC) 
)
