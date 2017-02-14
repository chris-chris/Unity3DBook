CREATE PROCEDURE [dbo].USP_User_Upgrade
	@UserID bigint,
	@UpgradeType varchar(100),
	@ResultCode int OUTPUT,
	@Message varchar(300) OUTPUT
AS
	DECLARE @UpgradeCost int, @UpgradeAmountHealth int, @UpgradeAmountDefense int, @UpgradeAmountSpeed int,@UpgradeAmountDamage int, @HealthLevel int, @DefenseLevel int, @SpeedLevel int, @DamageLevel int
	DECLARE @UserDiamond int, @UserHealthLevel int, @UserDefenseLevel int, @UserSpeedLevel int, @UserDamageLevel int
	SELECT @UserDiamond = Diamond, @UserHealthLevel = HealthLevel, @UserDefenseLevel = DefenseLevel, @UserSpeedLevel = SpeedLevel, @UserDamageLevel = DamageLevel FROM TB_User WHERE UserID = @UserID

	SET @UpgradeAmountHealth = 0
	SET @UpgradeAmountDefense = 0
	SET @UpgradeAmountSpeed = 0
	SET @UpgradeAmountDamage = 0

	IF @UpgradeType = 'Health'
	BEGIN
		SELECT @UpgradeCost = UpgradeCost, @UpgradeAmountHealth = UpgradeAmount, @HealthLevel = UpgradeLevel FROM TB_UpgradeInfo WHERE UpgradeType = @UpgradeType AND UpgradeLevel = @UserHealthLevel + 1
		SET @DefenseLevel = @UserDefenseLevel
		SET @SpeedLevel = @UserSpeedLevel
		SET @DamageLevel = @UserDamageLevel
	END

	ELSE IF @UpgradeType = 'Defense'
	BEGIN
		SELECT @UpgradeCost = UpgradeCost, @UpgradeAmountDefense = UpgradeAmount, @DefenseLevel = UpgradeLevel FROM TB_UpgradeInfo WHERE UpgradeType = @UpgradeType AND UpgradeLevel = @UserDefenseLevel + 1
		SET @HealthLevel = @UserHealthLevel
		SET @SpeedLevel = @UserSpeedLevel
		SET @DamageLevel = @UserDamageLevel
	END

	ELSE IF @UpgradeType = 'Speed'
	BEGIN
		SELECT @UpgradeCost = UpgradeCost, @UpgradeAmountSpeed = UpgradeAmount, @SpeedLevel = UpgradeLevel FROM TB_UpgradeInfo WHERE UpgradeType = @UpgradeType AND UpgradeLevel = @UserSpeedLevel + 1
		SET @HealthLevel = @UserHealthLevel
		SET @DefenseLevel = @UserDefenseLevel
		SET @DamageLevel = @UserDamageLevel
	END

	IF @UpgradeType = 'Damage'
	BEGIN
		SELECT @UpgradeCost = UpgradeCost, @UpgradeAmountDamage = UpgradeAmount, @DamageLevel = UpgradeLevel FROM TB_UpgradeInfo WHERE UpgradeType = @UpgradeType AND UpgradeLevel = @UserDamageLevel + 1
		SET @HealthLevel = @UserHealthLevel
		SET @DefenseLevel = @UserDefenseLevel
		SET @SpeedLevel = @UserSpeedLevel
	END

	IF @UpgradeCost IS NULL
	BEGIN
		SET @ResultCode = 4
		SET @Message = 'Upgrade Fail : Max Level'
		RETURN 0
	END

	IF @UserDiamond < @UpgradeCost
	BEGIN
		SET @ResultCode = 5
		SET @Message = 'Not Enough Diamond'
		RETURN 0
	END

	UPDATE TB_User SET 
		Health = Health + @UpgradeAmountHealth,
		Defense = Defense + @UpgradeAmountDefense,
		Speed = Speed + @UpgradeAmountSpeed,
		Damage = Damage + @UpgradeAmountDamage,
		HealthLevel = @HealthLevel,
		DefenseLevel = @DefenseLevel,	
		DamageLevel = @DamageLevel, 
		SpeedLevel = @SpeedLevel,
		Diamond = Diamond - @UpgradeCost
		WHERE UserID = @UserID

	SET @Message = 'Success'
	SET @ResultCode = 1
RETURN 0