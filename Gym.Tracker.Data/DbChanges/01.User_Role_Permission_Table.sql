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
    Password VARBINARY(MAx) NOT NULL,

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

    IsMailNotificationAllowed BIT NULL ,
    RoleId BIGINT NOT NULL
);

CREATE TABLE RoleType (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleCode INT NOT NULL UNIQUE,           
    RoleName NVARCHAR(100) NOT NULL UNIQUE  
);

CREATE TABLE RolePermission (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleTypeId INT NOT NULL,
    ApplicationPermissionId INT NOT NULL,    
    CONSTRAINT FK_RoleTypeId FOREIGN KEY (RoleTypeId) REFERENCES RoleType(Id),
    CONSTRAINT FK_ApplicationPermissionId FOREIGN KEY (ApplicationPermissionId) REFERENCES ApplicationPermission(Id)
);

CREATE TABLE ApplicationPermission (
    Id INT IDENTITY(1,1) PRIMARY KEY,      
    PermissionCode INT NOT NULL UNIQUE,    -- Enum value (1,2,3,...)
    PermissionName NVARCHAR(100) NOT NULL  -- Enum name (allowallaction, etc.)
);
