CREATE DATABASE DiscordClone;
GO

USE DiscordClone;
GO

CREATE TABLE Roles(
	RoleId INT IDENTITY(1, 1) NOT NULL,
	Code NVARCHAR(10) NOT NULL,
	ShowName NVARCHAR(100) NOT NULL,
	CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

CREATE TABLE UserStatusType(
	UserStatusId INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	Code NVARCHAR(10) NOT NULL,
	ShowName NVARCHAR(11) NOT NULL,
	CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
);
GO

INSERT INTO UserStatusType (Code, ShowName)
VALUES
	('online', 'En línea'),
	('not_disturb', 'No molestar'),
	('idle', 'Ausente'),
	('ghost', 'Invisible')
GO

CREATE TABLE Users(
	UserId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	Username NVARCHAR(32) NOT NULL,
	DisplayName NVARCHAR(100) NOT NULL,
	Description NVARCHAR(255) NULL,
	StatusType INT NOT NULL REFERENCES UserStatusType(UserStatusId) DEFAULT (1), -- online
	StatusTime INT NULL,
	StatusContent NVARCHAR(50) NULL DEFAULT ('Hi, there!'),
	AvatarURL NVARCHAR(255) NULL,
	BannerURL NVARCHAR(255) NULL,
--	RoleId INT NOT NULL REFERENCES Roles (RoleId),
	CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
--	CONSTRAINT FK_Roles_RoleId FOREIGN KEY (RoleId) REFERENCES Roles (RoleId),
);
GO

CREATE TABLE Collections(
	CollectionId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	Name NVARCHAR(20) NOT NULL,
	Description NVARCHAR(100) NOT NULL DEFAULT ('This is my collection'),
	CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
	DeletedAt DATETIME2 NULL,
);
GO

CREATE TABLE Items(
	ItemId INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL UNIQUE,
	CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
);

INSERT INTO Items (Name)
VALUES
	('Hollow Knight'), -- Me pagó $10
	('Osu!'); -- Este también me pagó $10
GO

CREATE TABLE CollectionsItems(
	CollectionId UNIQUEIDENTIFIER NOT NULL REFERENCES Collections(CollectionId),
	ItemId INT NOT NULL REFERENCES Items(ItemId) ON DELETE CASCADE,
	CONSTRAINT PK_CollectionsItems_CollectionId_ItemId PRIMARY KEY (CollectionId, ItemId),
);
GO

INSERT INTO Collections(Name, Description)
VALUES
	('Mis juegos', 'Juegos');
GO

SELECT * FROM Collections
WHERE DeletedAt IS NULL;

SELECT * FROM ITEMS;

DECLARE @CollectionId UNIQUEIDENTIFIER = 'A9C7FAB7-8844-4CDF-B354-8FDAC0287046';
DECLARE @ItemHollowKnightId INT = (SELECT ItemId FROM Items WHERE ItemId = 1);
DECLARE @ItemOsuId INT = (SELECT ItemId FROM Items WHERE ItemId = 2);

INSERT INTO CollectionsItems (CollectionId, ItemId)
VALUES
	(@CollectionId, @ItemOsuId);
GO

DECLARE @ItemABuscar NVARCHAR(50) = 'Hollow Knight';

SELECT * FROM Items
WHERE Name = @ItemABuscar;

CREATE INDEX IX_Items_Name ON Items (Name);