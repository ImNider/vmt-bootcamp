-- ============================================================
--  TalentInsights - Base de datos SQL Server
--  Reconoce el crecimiento de los colaboradores en una empresa
-- ============================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TalentInsights')
    CREATE DATABASE TalentInsights;
GO

USE TalentInsights;
GO

-- ============================================================
--  TABLA: Collaborators
-- ============================================================
CREATE TABLE Collaborators (
    Id            UNIQUEIDENTIFIER   NOT NULL DEFAULT NEWID(),
    FullName      NVARCHAR(150)      NOT NULL,
    GitlabProfile NVARCHAR(255)      NULL,
    Position      NVARCHAR(100)      NOT NULL,
    JoinedAt      DATETIME2          NOT NULL DEFAULT SYSUTCDATETIME(),
    IsActive      BIT                NOT NULL DEFAULT 1,
    CreatedAt     DATETIME2          NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt     DATETIME2          NOT NULL DEFAULT SYSUTCDATETIME(),

    CONSTRAINT PK_Collaborators PRIMARY KEY (Id)
);
GO

INSERT INTO Collaborators (FullName, Position)
VALUES
('Neider', 'Desarrollador');

SELECT Id, FullName, Position FROM Collaborators
WHERE FullName = 'Neider2'

CREATE TABLE Roles(
	RoleId INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	ShowName NVARCHAR(100) NOT NULL,
	CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
);
GO

INSERT INTO Roles (Name, ShowName)
VALUES
('MODERATOR', 'Moderador');

SELECT * FROM Roles