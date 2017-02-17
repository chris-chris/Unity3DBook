CREATE PROCEDURE [dbo].[USP_User_Login]
@FacebookID varchar(200),
@FacebookName nvarchar(255),
@FacebookPhotoURL varchar(512),
@AccessToken varchar(255) OUTPUT,
@UserID bigint OUTPUT,
@ResultCode int OUTPUT,
@Message varchar(300) OUTPUT
AS
-- 새로운 엑세스토큰 생성
SET @AccessToken = NEWID()

SELECT @UserID = UserID FROM TB_User WHERE FacebookID = @FacebookID 

-- 유저가 이미 회원가입을 하여 동일한 페이스북 아이디가 존재하는 경우
IF (@UserID IS NOT NULL)
BEGIN
	UPDATE TB_User SET 
		FacebookName = @FacebookName,
		FacebookPhotoURL = @FacebookPhotoURL,
		AccessToken = @AccessToken
		WHERE UserID = @UserID;

	SET @Message = 'Login'
	SET @ResultCode = 2
	RETURN 0
END
ELSE
BEGIN
	-- 테이블에 데이터 삽입
	INSERT INTO TB_User ( FacebookID, FacebookName, FacebookPhotoURL, Point, AccessToken) 
	VALUES( @FacebookID, @FacebookName, @FacebookPhotoURL, 0, @AccessToken)

	SELECT @UserID = UserID FROM TB_User WHERE FacebookID = @FacebookID 

	SELECT @AccessToken

	SET @Message = 'Login New User'
	SET @ResultCode = 1
	RETURN 0
END