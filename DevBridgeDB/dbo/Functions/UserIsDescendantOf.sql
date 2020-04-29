--  Summary
--      Will determine whether user is descendant subordinate of another user
--
--  Parameters
--      @AncestorId: ancestor which will be the root user for searching descendant
--      @DecendantId: user which will be looked for when searching for ancestor's subordinates
--
--  Returns
--      1 if @Descendant is found as direct/indirect subordinate of @Ancestor,
--      otherwise returns 0

CREATE FUNCTION [dbo].[UserIsDescendantOf]
(
	@AncestorId int,
	@DescendantId int
)
RETURNS bit
AS BEGIN

    DECLARE @Result bit;

    WITH Descendants AS
    (
        SELECT e.UserId, e.FirstName, e.ManagerId
        FROM Users AS e
        WHERE e.ManagerId = @AncestorId

        UNION ALL

        SELECT e.UserId, e.FirstName, e.ManagerId
        FROM Users AS e
        JOIN Descendants AS dscn
        ON e.ManagerId = dscn.UserId
    )

    SELECT @Result = COUNT(1)
      FROM Descendants AS d
     WHERE d.UserId = @DescendantId

    RETURN @Result;

END