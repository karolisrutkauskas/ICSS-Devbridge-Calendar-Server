:r ./AssignmentStates.sql
IF '$(UseTestData)' = 'true'
BEGIN
	:r ./TestData/TestData.sql
END