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

 Date: 02/02/2017 19:39:16 PM
*/

-- ----------------------------
--  Table structure for TB_LevelInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID('[dbo].[TB_LevelInfo]') AND type IN ('U'))
	DROP TABLE [dbo].[TB_LevelInfo]
GO
CREATE TABLE [dbo].[TB_LevelInfo] (
	[Level] int NOT NULL,
	[Experience] int NOT NULL,
	[Defense] int NOT NULL,
	[Health] int NOT NULL,
	[Damage] int NOT NULL,
	[Speed] int NOT NULL
)
ON [PRIMARY]
GO

-- ----------------------------
--  Records of TB_LevelInfo
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [dbo].[TB_LevelInfo] VALUES ('1', '0', '2', '100', '10', '1');
INSERT INTO [dbo].[TB_LevelInfo] VALUES ('2', '100', '3', '110', '12', '2');
INSERT INTO [dbo].[TB_LevelInfo] VALUES ('3', '300', '4', '120', '14', '4');
INSERT INTO [dbo].[TB_LevelInfo] VALUES ('4', '700', '5', '130', '17', '6');
INSERT INTO [dbo].[TB_LevelInfo] VALUES ('5', '1200', '6', '140', '20', '8');
INSERT INTO [dbo].[TB_LevelInfo] VALUES ('6', '1500', '7', '180', '22', '10');
GO
COMMIT
GO


-- ----------------------------
--  Primary key structure for table TB_LevelInfo
-- ----------------------------
ALTER TABLE [dbo].[TB_LevelInfo] ADD
	CONSTRAINT [PK__TB_Level__AAF899637DB0C904] PRIMARY KEY CLUSTERED ([Level]) 
	WITH (PAD_INDEX = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON)
	ON [default]
GO

-- ----------------------------
--  Options for table TB_LevelInfo
-- ----------------------------
ALTER TABLE [dbo].[TB_LevelInfo] SET (LOCK_ESCALATION = TABLE)
GO

