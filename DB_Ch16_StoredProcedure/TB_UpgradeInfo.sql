/*
 Navicat Premium Data Transfer

 Source Server         : RDS for Unity
 Source Server Type    : SQL Server
 Source Server Version : 11002100
 Source Host           : mssql.czzkqlp6lf2e.ap-northeast-1.rds.amazonaws.com
 Source Database       : UnityAction
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 11002100
 File Encoding         : utf-8

 Date: 02/02/2017 19:39:33 PM
*/

-- ----------------------------
--  Table structure for TB_UpgradeInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID('[dbo].[TB_UpgradeInfo]') AND type IN ('U'))
	DROP TABLE [dbo].[TB_UpgradeInfo]
GO
CREATE TABLE [dbo].[TB_UpgradeInfo] (
	[UpgradeID] int NOT NULL,
	[UpgradeType] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UpgradeLevel] int NULL,
	[UpgradeAmount] int NULL,
	[UpgradeCost] int NULL
)
ON [PRIMARY]
GO

-- ----------------------------
--  Records of TB_UpgradeInfo
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('101', 'Health', '1', '0', '0');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('102', 'Health', '2', '10', '2');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('103', 'Health', '3', '15', '4');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('104', 'Health', '4', '20', '6');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('105', 'Health', '5', '25', '8');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('106', 'Health', '6', '30', '10');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('107', 'Health', '7', '30', '12');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('108', 'Health', '8', '35', '14');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('109', 'Health', '9', '35', '16');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('110', 'Health', '10', '40', '18');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('201', 'Defense', '1', '0', '0');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('202', 'Defense', '2', '5', '2');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('203', 'Defense', '3', '10', '4');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('204', 'Defense', '4', '15', '6');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('205', 'Defense', '5', '20', '8');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('206', 'Defense', '6', '20', '10');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('207', 'Defense', '7', '35', '12');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('208', 'Defense', '8', '40', '14');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('209', 'Defense', '9', '45', '16');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('210', 'Defense', '10', '45', '18');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('301', 'Damage', '1', '0', '0');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('302', 'Damage', '2', '1', '2');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('303', 'Damage', '3', '2', '4');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('304', 'Damage', '4', '3', '6');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('305', 'Damage', '5', '3', '8');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('306', 'Damage', '6', '3', '10');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('307', 'Damage', '7', '3', '12');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('308', 'Damage', '8', '3', '14');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('309', 'Damage', '9', '3', '16');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('310', 'Damage', '10', '4', '18');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('401', 'Speed', '1', '0', '0');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('402', 'Speed', '2', '10', '2');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('403', 'Speed', '3', '20', '4');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('404', 'Speed', '4', '30', '6');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('405', 'Speed', '5', '40', '8');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('406', 'Speed', '6', '40', '10');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('407', 'Speed', '7', '40', '12');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('408', 'Speed', '8', '40', '14');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('409', 'Speed', '9', '45', '16');
INSERT INTO [dbo].[TB_UpgradeInfo] VALUES ('410', 'Speed', '10', '50', '18');
GO
COMMIT
GO


-- ----------------------------
--  Primary key structure for table TB_UpgradeInfo
-- ----------------------------
ALTER TABLE [dbo].[TB_UpgradeInfo] ADD
	CONSTRAINT [PK__TB_Upgra__CA188BC0FD8EFBD4] PRIMARY KEY CLUSTERED ([UpgradeID]) 
	WITH (PAD_INDEX = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON)
	ON [default]
GO

-- ----------------------------
--  Options for table TB_UpgradeInfo
-- ----------------------------
ALTER TABLE [dbo].[TB_UpgradeInfo] SET (LOCK_ESCALATION = TABLE)
GO

