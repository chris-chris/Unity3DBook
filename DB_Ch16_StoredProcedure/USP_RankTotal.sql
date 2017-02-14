CREATE PROCEDURE [dbo].USP_RankTotal
@Start bigint,
@Count bigint
AS
-- 시작점부터 개수만큼 랭크 테이블 데이터 조회
SELECT * FROM (
	SELECT Rank() over (order by Point desc) as Rank, * FROM TB_User
) a WHERE a.Rank >= @Start AND a.Rank <= @Start + @Count - 1;

RETURN 0