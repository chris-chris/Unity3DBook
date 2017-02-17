CREATE FUNCTION [dbo].[FNC_Exp_ForNextLevel]
	(
	@Experience int)
RETURNS int
AS
BEGIN
	DECLARE @NextLevel int;
	SET @NextLevel = 0;
	DECLARE @NextExp int;
	SET @NextExp = 0;

	SELECT TOP 1 @NextLevel = Level, @NextExp = Experience
		FROM TB_LevelInfo 
		WHERE  @Experience < Experience 
		ORDER BY Experience ASC;
	
	RETURN @NextExp;

END