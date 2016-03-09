/* Using Database sqlserver2008 and Connection String Server=localhost; Database=FoodManager; User Id=sa; Password=********; */
/* VersionMigration migrating ================================================ */

/* Beginning Transaction */
/* CreateTable VersionInfo */
CREATE TABLE [dbo].[VersionInfo] ([Version] BIGINT NOT NULL)

/* Committing Transaction */
/* VersionMigration migrated */

/* VersionUniqueMigration migrating ========================================== */

/* Beginning Transaction */
/* CreateIndex VersionInfo (Version) */
CREATE UNIQUE CLUSTERED INDEX [UC_Version] ON [dbo].[VersionInfo] ([Version] ASC)

/* AlterTable VersionInfo */
/* No SQL statement executed. */

/* CreateColumn VersionInfo AppliedOn DateTime */
ALTER TABLE [dbo].[VersionInfo] ADD [AppliedOn] DATETIME

/* Committing Transaction */
/* VersionUniqueMigration migrated */

/* VersionDescriptionMigration migrating ===================================== */

/* Beginning Transaction */
/* AlterTable VersionInfo */
/* No SQL statement executed. */

/* CreateColumn VersionInfo Description String */
ALTER TABLE [dbo].[VersionInfo] ADD [Description] NVARCHAR(1024)

/* Committing Transaction */
/* VersionDescriptionMigration migrated */

/* 20160302130210: _20160302130210_Seed migrating ============================ */

/* Beginning Transaction */
/* CreateTable Region */
CREATE TABLE [dbo].[Region] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Region] PRIMARY KEY ([Id]))

/* CreateTable Company */
CREATE TABLE [dbo].[Company] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [RegionId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Company] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Company_Region Company(RegionId) Region(Id) */
ALTER TABLE [dbo].[Company] ADD CONSTRAINT [FK_Company_Region] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([Id])

/* CreateTable Branch */
CREATE TABLE [dbo].[Branch] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Code] NVARCHAR(250) NOT NULL, [CompanyId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Branch] PRIMARY KEY ([Id]))

/* CreateIndex Branch (Code) */
CREATE UNIQUE INDEX [IX_Branch_Code] ON [dbo].[Branch] ([Code] ASC)

/* CreateForeignKey FK_Branch_Company Branch(CompanyId) Company(Id) */
ALTER TABLE [dbo].[Branch] ADD CONSTRAINT [FK_Branch_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])

/* CreateTable Department */
CREATE TABLE [dbo].[Department] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [BranchId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Department] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Department_Branch Department(BranchId) Branch(Id) */
ALTER TABLE [dbo].[Department] ADD CONSTRAINT [FK_Department_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([Id])

/* CreateTable Disease */
CREATE TABLE [dbo].[Disease] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Code] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Disease] PRIMARY KEY ([Id]))

/* CreateIndex Disease (Code) */
CREATE UNIQUE INDEX [IX_Disease_Code] ON [dbo].[Disease] ([Code] ASC)

/* CreateTable Warning */
CREATE TABLE [dbo].[Warning] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Code] NVARCHAR(250) NOT NULL, [DiseaseId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Warning] PRIMARY KEY ([Id]))

/* CreateIndex Warning (Code) */
CREATE UNIQUE INDEX [IX_Warning_Code] ON [dbo].[Warning] ([Code] ASC)

/* CreateForeignKey FK_Warning_Disease Warning(DiseaseId) Disease(Id) */
ALTER TABLE [dbo].[Warning] ADD CONSTRAINT [FK_Warning_Disease] FOREIGN KEY ([DiseaseId]) REFERENCES [dbo].[Disease] ([Id])

/* CreateTable User */
CREATE TABLE [dbo].[User] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Type] INT NOT NULL, [UserName] NVARCHAR(250) NOT NULL, [Password] NVARCHAR(250) NOT NULL, [PublicKey] NVARCHAR(250), [Time] NVARCHAR(250), [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_User] PRIMARY KEY ([Id]))

/* CreateIndex User (UserName) */
CREATE UNIQUE INDEX [IX_User_UserName] ON [dbo].[User] ([UserName] ASC)

/* ExecuteSqlStatement INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Yucatan', 1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Campeche', 1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1) */
INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Yucatan', 1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Campeche', 1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1)

/* ExecuteSqlStatement INSERT INTO Company (Name, RegionId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Bepensa Industria', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Bepensa Bebidas', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1) */
INSERT INTO Company (Name, RegionId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Bepensa Industria', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Bepensa Bebidas', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1)

/* ExecuteSqlStatement INSERT INTO Branch (Name, Code, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Opesystem', 'CODE1', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Finbe', 'CODE2', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1) */
INSERT INTO Branch (Name, Code, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Opesystem', 'CODE1', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Finbe', 'CODE2', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1)

/* ExecuteSqlStatement INSERT INTO Department (Name, BranchId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Desarrollo', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('HelpDesk', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1) */
INSERT INTO Department (Name, BranchId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Desarrollo', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('HelpDesk', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1)

/* ExecuteSqlStatement INSERT INTO [User] (Name, Type, UserName, Password, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('admin', 1, 'admin', 'JRxZJ9O9m6Y=',1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1) */
INSERT INTO [User] (Name, Type, UserName, Password, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('admin', 1, 'admin', 'JRxZJ9O9m6Y=',1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1)

/* ExecuteSqlStatement INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Enfermedad cardiaca', 'CODE1',1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Diabetes', 'CODE2',1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1) */
INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Enfermedad cardiaca', 'CODE1',1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Diabetes', 'CODE2',1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1)

/* ExecuteSqlStatement INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Te estas pasando de calorias', 'CODE1', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Cuidado con tu alimentacion', 'CODE2', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1) */
INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Te estas pasando de calorias', 'CODE1', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1),('Cuidado con tu alimentacion', 'CODE2', 1,1, 1, '03/08/2016 08:53:51', '03/08/2016 08:53:51', 1, 1)

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (20160302130210, '2016-03-08T14:53:52', '_20160302130210_Seed')
/* Committing Transaction */
/* 20160302130210: _20160302130210_Seed migrated */

/* Task completed. */
