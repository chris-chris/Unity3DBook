CREATE FUNCTION [dbo].[FNC_Exp_AfterLastLevel]
	(
	@Experience int)
RETURNS int
AS
BEGIN
	DECLARE @LastExp int;
	SET @LastExp = 0;

	SELECT TOP 1 @LastExp = Experience
		FROM TB_LevelInfo 
		WHERE  @Experience > Experience 
		ORDER BY Experience DESC;
	
	RETURN @Experience - @LastExp;

END