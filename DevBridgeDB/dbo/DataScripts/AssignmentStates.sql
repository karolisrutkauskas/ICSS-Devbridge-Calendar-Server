CREATE TABLE #StateSource (
	StateId INTEGER NOT NULL,
	State NVARCHAR(1024) NOT NULL
)

INSERT INTO #StateSource (StateId, State) VALUES(1, 'Upcomming')
INSERT INTO #StateSource (StateId, State) VALUES(2, 'In progress')
INSERT INTO #StateSource (StateId, State) VALUES(3, 'Completed')
INSERT INTO #StateSource (StateId, State) VALUES(4, 'Skipped')
INSERT INTO #StateSource (StateId, State) VALUES(5, 'Failed')

MERGE AssignmentStates AS target
USING #StateSource AS source
ON (target.StateId = source.StateId)
WHEN MATCHED
THEN UPDATE SET target.State = source.State
WHEN NOT MATCHED THEN
INSERT VALUES (source.StateId, source.State);