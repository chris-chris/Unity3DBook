CREATE PROCEDURE [dbo].[USP_UpgradeInfo_Select]
	
AS
	SELECT UpgradeType, UpgradeLevel, UpgradeAmount, UpgradeCost FROM TB_UpgradeInfo