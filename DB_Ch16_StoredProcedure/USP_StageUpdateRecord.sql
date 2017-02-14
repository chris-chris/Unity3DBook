CREATE PROCEDURE [dbo].USP_StageUpdateRecord
@UserID bigint,
@Point bigint
AS
DECLARE @UserPoint bigint
DECLARE @ExpBeforeGame int
DECLARE @ExpAfterGame int

SELECT @UserPoint = Point, @ExpBeforeGame = Experience FROM TB_User WHERE UserID = @UserID
--회원 데이터가 없는경우
IF (@UserPoint IS NULL)
BEGIN
SELECT 'UserID is invalid' as Message, 1002 as ResultCode
RETURN 0
END

--테이블에 데이터 삽입
INSERT INTO TB_StageRecord (UserID, Point, RecordTime) 
VALUES (@UserID, @Point, GETDATE())

--현재 포인트가 최고점인 경우 점수 갱신
IF (@UserPoint < @Point )
BEGIN
UPDATE TB_User SET Point = @Point WHERE UserID = @UserID
END
UPDATE TB_User SET Experience = Experience + @Point WHERE UserID = @UserID

DECLARE @LevelBeforeGame int
DECLARE @LevelAfterGame int
SET @ExpAfterGame = @ExpBeforeGame + @Point

SET @LevelBeforeGame = dbo.FNC_LevelFromExp(@ExpBeforeGame)
SET @LevelAfterGame = dbo.FNC_LevelFromExp(@ExpAfterGame)

DECLARE @IsLevelUp int
IF @LevelBeforeGame != @LevelAfterGame
BEGIN
	SET @IsLevelUp = 1
	UPDATE TB_User SET Level = @LevelAfterGame WHERE UserID = @UserID
END
ELSE SET @IsLevelUp = 0

SELECT 'Success' as Message, 1 as ResultCode, @IsLevelUp as IsLevelUp, 
	@LevelBeforeGame as LevelBeforeGame, @LevelAfterGame as LevelAfterGame,
	@ExpBeforeGame as ExpBeforeGame, @ExpAfterGame as ExpAfterGame

RETURN 0