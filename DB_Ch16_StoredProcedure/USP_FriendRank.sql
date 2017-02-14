CREATE PROCEDURE [dbo].[USP_FriendRank]
	@List varchar(5000)
AS
	DECLARE @Query varchar(5100)
	SET @Query = 'SELECT Rank() over (order by Point desc) as Rank, * FROM TB_User
		WHERE FacebookID IN ( ' + @List + ')'
	EXECUTE(@Query)
	

RETURN 0