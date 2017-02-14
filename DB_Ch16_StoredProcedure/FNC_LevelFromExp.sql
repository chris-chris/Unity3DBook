CREATE FUNCTION [dbo].[FNC_LevelFromExp]
	(
	@Experience int)
RETURNS int
AS
BEGIN
	DECLARE @Level int;
	SET @Level = 0;

	SELECT TOP 1 @Level = Level
		FROM TB_LevelInfo 
		WHERE  @Experience > Experience 
		ORDER BY Experience DESC;
	
	RETURN @Level;

END