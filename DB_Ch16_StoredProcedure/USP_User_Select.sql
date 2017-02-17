CREATE PROCEDURE [dbo].[USP_User_Select]
	@UserID bigint,
	@ResultCode int OUTPUT,
	@Message varchar(300) OUTPUT
AS
	DECLARE @Cnt int
	SELECT @Cnt = COUNT(*) FROM TB_User WHERE UserID = @UserID AND Deleted = 0

	IF @Cnt != 1
	BEGIN
		SET @Message = 'Invalid UserID'
		SET @ResultCode = 3
		RETURN 0
	END

	SELECT *, 
		dbo.FNC_Exp_AfterLastLevel(Experience) as ExpAfterLastLevel, 
		dbo.FNC_Exp_ForNextLevel(Experience) as ExpForNextLevel
		FROM TB_User WHERE UserID = @UserID AND Deleted = 0

	SET @Message = 'Success'
	SET @ResultCode = 1
RETURN 0