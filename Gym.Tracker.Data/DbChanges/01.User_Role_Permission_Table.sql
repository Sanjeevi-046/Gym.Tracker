-- Create Users
CREATE TABLE [dbo].[Users]
(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(200) NULL,
    FirstName NVARCHAR(100) NOT NULL,
    MiddleName NVARCHAR(100) NULL,
    LastName NVARCHAR(100) NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NULL,
    Gender NVARCHAR(20) NULL,

    CountryId BIGINT NULL,
    UserTypeId BIGINT NULL,
    BillingTermsId BIGINT NULL,
    PaymentMethodId BIGINT NULL,
    Nationality BIGINT NULL,

    -- Security
    Password VARBINARY(MAX) NOT NULL,

    IsTerminated BIT NULL,
    IsUserLocked BIT NULL,
    IsForcePwdChange BIT NULL,
    LastLoginAt DATETIME NULL,
    LastLoginDate DATETIME NULL,

    ContactInformation NVARCHAR(MAX) NULL,
    BillingAddress NVARCHAR(MAX) NULL,
    MailingAddress NVARCHAR(MAX) NULL,

    CreatedBy BIGINT NULL,
    CreatedAt DATETIME NULL,
    UpdatedBy BIGINT NULL,
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT(1),
    DeletedBy BIGINT NULL,
    DeletedAt DATETIME NULL,
    RestoredBy BIGINT NULL,
    RestoredAt DATETIME NULL,

    IsMailNotificationAllowed BIT NULL,
    RoleId BIGINT NOT NULL
);
GO

-- Create RoleTypes
CREATE TABLE RoleType (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleCode INT NOT NULL UNIQUE,           
    RoleName NVARCHAR(100) NOT NULL UNIQUE  
);
GO

-- Create ApplicationPermission
CREATE TABLE ApplicationPermission (
    Id INT IDENTITY(1,1) PRIMARY KEY,      
    PermissionCode INT NOT NULL UNIQUE,    
    PermissionName NVARCHAR(100) NOT NULL  
);
GO

-- Create RolePermission
CREATE TABLE RolePermission (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleTypeId INT NOT NULL,
    ApplicationPermissionId INT NOT NULL,    
    CONSTRAINT FK_RoleTypeId FOREIGN KEY (RoleTypeId) REFERENCES RoleType(Id),
    CONSTRAINT FK_ApplicationPermissionId FOREIGN KEY (ApplicationPermissionId) REFERENCES ApplicationPermission(Id)
);
GO

-- Insert RoleTypes
INSERT INTO RoleType (RoleCode, RoleName) VALUES
(1, 'Admin'),
(2, 'Trainer'),
(3, 'Staff'),
(4, 'Member'),
(5, 'Guest');
GO

-- Insert ApplicationPermission
INSERT INTO ApplicationPermission (PermissionCode, PermissionName) VALUES
(1, 'allowallaction'),
(2, 'createadmin'),
(3, 'createstaff'),
(4, 'createmember'),
(5, 'createguest'),
(6, 'createtrainer'),
(7, 'viewdashboard'),
(8, 'viewstaffuser'),
(9, 'viewmemberuser'),
(10, 'viewguest'),
(11, 'viewmemberslot'),
(12, 'viewattendance'),
(13, 'editattendance'),
(14, 'editslot');
GO

-- Insert [RolePermission]
SET IDENTITY_INSERT [dbo].[RolePermission] ON;
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (1, 1, 1);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (2, 2, 7);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (3, 2, 9);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (4, 2, 11);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (5, 2, 12);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (6, 2, 13);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (7, 2, 14);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (8, 4, 7);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (9, 4, 11);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (10, 4, 12);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (11, 5, 7);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (12, 3, 7);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (13, 3, 8);
GO

INSERT [dbo].[RolePermission] ([Id], [RoleTypeId], [ApplicationPermissionId]) VALUES (14, 3, 11);
GO

SET IDENTITY_INSERT [dbo].[RolePermission] OFF;
GO
