DECLARE @dbname nvarchar(128) = N'RSLArtifactManager';
IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)) BEGIN
	PRINT('Database ' + @dbname + ' does not exist');
	THROW 1, 'This is not a Valid Instance Database', 16
END

PRINT('Using database [' + @dbname + ']');
USE [RSLArtifactManager]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Stats') BEGIN
	PRINT('Creating Table [Stats]');
	CREATE TABLE [Stats] (
		[Id] bigint NOT NULL IDENTITY,

		[HP] int NULL,
		[HPe] int NULL,
		[HPp] int NULL,
		[HPpe] int NULL,

		[ATK] int NULL,
		[ATKe] int NULL,
		[ATKp] int NULL,
		[ATKpe] int NULL,

		[DEF] int NULL,
		[DEFe] int NULL,
		[DEFp] int NULL,
		[DEFpe] int NULL,

		[SPD] int NULL,
		[SPDe] int NULL,
		[SPDp] int NULL,

		[CRATE] int NULL,
		[CRATEe] int NULL,

		[CDMG] int NULL,
		[CDMGe] int NULL,

		[RESIST] int NULL,
		[RESISTe] int NULL,

		[ACC] int NULL,
		[ACCe] int NULL,
		
		CONSTRAINT [PK_Stats] PRIMARY KEY ([Id])
	);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Artifact') BEGIN
	PRINT('Creating Table [Artifact]');
	CREATE TABLE [Artifact] (
		[Id] bigint NOT NULL IDENTITY,

		[Level] tinyint NOT NULL,
		[Stars] tinyint NOT NULL,

		[Set] tinyint NOT NULL,
		[Type] tinyint NOT NULL,
		[Rarity] tinyint NOT NULL,

		[Main] tinyint NOT NULL,
		[Sub1] tinyint NULL,
		[Sub2] tinyint NULL,
		[Sub3] tinyint NULL,
		[Sub4] tinyint NULL,

		[UpgradesSub1] tinyint NULL,
		[UpgradesSub2] tinyint NULL,
		[UpgradesSub3] tinyint NULL,
		[UpgradesSub4] tinyint NULL,

		[IdStats] bigint foreign key references Stats(Id),

		CONSTRAINT [PK_Artifact] PRIMARY KEY ([Id]),
	);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Champions') BEGIN
	PRINT('Creating Table [Champions]');
	CREATE TABLE [Champions] (
		[Id] bigint NOT NULL IDENTITY,

		[Name] nvarchar(60) NOT NULL,
		[Faction] tinyint NOT NULL,
		[Affinity] tinyint NOT NULL,
		[Type] tinyint NOT NULL,
		[Rarity] tinyint NOT NULL,

		[Level] tinyint NOT NULL,
		[Stars] tinyint NOT NULL,
		[Ascended] tinyint NOT NULL,

		[IdBase] bigint foreign key references Stats(Id),
		[IdMasteries] bigint foreign key references Stats(Id),

		[IdWeapon] bigint foreign key references Artifact(Id),
		[IdHelmet] bigint foreign key references Artifact(Id),
		[IdShield] bigint foreign key references Artifact(Id),
		[IdGauntlets] bigint foreign key references Artifact(Id),
		[IdChestplate] bigint foreign key references Artifact(Id),
		[IdBoots] bigint foreign key references Artifact(Id),
		[IdRing] bigint foreign key references Artifact(Id),
		[IdAmulet] bigint foreign key references Artifact(Id),
		[IdBanner] bigint foreign key references Artifact(Id),

		CONSTRAINT [PK_Champions] PRIMARY KEY ([Id])
	);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Global') BEGIN
	PRINT('Creating Table [Global]');
	CREATE TABLE [Global] (
		[Id] int NOT NULL IDENTITY,

		[IdGreatHallMagic] bigint foreign key references Stats(Id),
		[IdGreatHallSpirit] bigint foreign key references Stats(Id),
		[IdGreatHallForce] bigint foreign key references Stats(Id),
		[IdGreatHallVoid] bigint foreign key references Stats(Id),

		[IdArenaTier] bigint foreign key references Stats(Id),
		[IdClan] bigint foreign key references Stats(Id),
		
		CONSTRAINT [PK_Global] PRIMARY KEY ([Id])
	)
END
GO