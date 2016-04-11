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

/* 1: _1_Seed migrating ====================================================== */

/* Beginning Transaction */
/* CreateTable Region */
CREATE TABLE [dbo].[Region] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Region] PRIMARY KEY ([Id]))

/* CreateTable Company */
CREATE TABLE [dbo].[Company] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [RegionId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Company] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Company_Region Company(RegionId) Region(Id) */
ALTER TABLE [dbo].[Company] ADD CONSTRAINT [FK_Company_Region] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([Id])

/* CreateTable Branch */
CREATE TABLE [dbo].[Branch] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Code] NVARCHAR(250) NOT NULL, [CompanyId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Branch] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Branch_Company Branch(CompanyId) Company(Id) */
ALTER TABLE [dbo].[Branch] ADD CONSTRAINT [FK_Branch_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])

/* CreateTable Department */
CREATE TABLE [dbo].[Department] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [BranchId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Department] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Department_Branch Department(BranchId) Branch(Id) */
ALTER TABLE [dbo].[Department] ADD CONSTRAINT [FK_Department_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([Id])

/* CreateTable Disease */
CREATE TABLE [dbo].[Disease] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Code] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Disease] PRIMARY KEY ([Id]))

/* CreateTable Warning */
CREATE TABLE [dbo].[Warning] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Code] NVARCHAR(250) NOT NULL, [DiseaseId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Warning] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Warning_Disease Warning(DiseaseId) Disease(Id) */
ALTER TABLE [dbo].[Warning] ADD CONSTRAINT [FK_Warning_Disease] FOREIGN KEY ([DiseaseId]) REFERENCES [dbo].[Disease] ([Id])

/* CreateTable Job */
CREATE TABLE [dbo].[Job] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Job] PRIMARY KEY ([Id]))

/* CreateTable Tip */
CREATE TABLE [dbo].[Tip] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Tip] PRIMARY KEY ([Id]))

/* CreateTable Dealer */
CREATE TABLE [dbo].[Dealer] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Dealer] PRIMARY KEY ([Id]))

/* CreateTable BranchDealer */
CREATE TABLE [dbo].[BranchDealer] ([BranchId] INT NOT NULL, [DealerId] INT NOT NULL)

/* CreateForeignKey FK_BranchDealer_Branch BranchDealer(BranchId) Branch(Id) */
ALTER TABLE [dbo].[BranchDealer] ADD CONSTRAINT [FK_BranchDealer_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([Id])

/* CreateForeignKey FK_BranchDealer_Dealer BranchDealer(DealerId) Dealer(Id) */
ALTER TABLE [dbo].[BranchDealer] ADD CONSTRAINT [FK_BranchDealer_Dealer] FOREIGN KEY ([DealerId]) REFERENCES [dbo].[Dealer] ([Id])

/* CreateIndex BranchDealer (BranchId, DealerId) */
CREATE UNIQUE INDEX [IX_BranchDealer] ON [dbo].[BranchDealer] ([BranchId] ASC, [DealerId] ASC)

/* CreateTable User */
CREATE TABLE [dbo].[User] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [UserName] NVARCHAR(250) NOT NULL, [Password] NVARCHAR(250) NOT NULL, [PublicKey] NVARCHAR(250), [Time] NVARCHAR(250), [DealerId] INT, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_User] PRIMARY KEY ([Id]))

/* CreateIndex User (UserName) */
CREATE UNIQUE INDEX [IX_User_UserName] ON [dbo].[User] ([UserName] ASC)

/* CreateForeignKey FK_User_Dealer User(DealerId) Dealer(Id) */
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Dealer] FOREIGN KEY ([DealerId]) REFERENCES [dbo].[Dealer] ([Id])

/* CreateTable Saucer */
CREATE TABLE [dbo].[Saucer] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Type] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Saucer] PRIMARY KEY ([Id]))

/* CreateTable DealerSaucer */
CREATE TABLE [dbo].[DealerSaucer] ([DealerId] INT NOT NULL, [SaucerId] INT NOT NULL)

/* CreateForeignKey FK_DealerSaucer_Dealer DealerSaucer(DealerId) Dealer(Id) */
ALTER TABLE [dbo].[DealerSaucer] ADD CONSTRAINT [FK_DealerSaucer_Dealer] FOREIGN KEY ([DealerId]) REFERENCES [dbo].[Dealer] ([Id])

/* CreateForeignKey FK_DealerSaucer_Saucer DealerSaucer(SaucerId) Saucer(Id) */
ALTER TABLE [dbo].[DealerSaucer] ADD CONSTRAINT [FK_DealerSaucer_Saucer] FOREIGN KEY ([SaucerId]) REFERENCES [dbo].[Saucer] ([Id])

/* CreateIndex DealerSaucer (DealerId, SaucerId) */
CREATE UNIQUE INDEX [IX_DealerSaucer] ON [dbo].[DealerSaucer] ([DealerId] ASC, [SaucerId] ASC)

/* CreateTable SaucerMultimedia */
CREATE TABLE [dbo].[SaucerMultimedia] ([Id] INT NOT NULL IDENTITY(1,1), [Path] NVARCHAR(MAX) NOT NULL, [SaucerId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_SaucerMultimedia] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_SaucerMultimedia_Saucer SaucerMultimedia(SaucerId) Saucer(Id) */
ALTER TABLE [dbo].[SaucerMultimedia] ADD CONSTRAINT [FK_SaucerMultimedia_Saucer] FOREIGN KEY ([SaucerId]) REFERENCES [dbo].[Saucer] ([Id])

/* CreateTable IngredientGroup */
CREATE TABLE [dbo].[IngredientGroup] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Color] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_IngredientGroup] PRIMARY KEY ([Id]))

/* CreateTable Ingredient */
CREATE TABLE [dbo].[Ingredient] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Amount] DECIMAL(19,5) NOT NULL, [Energy] DECIMAL(19,5) NOT NULL, [Protein] DECIMAL(19,5) NOT NULL, [Carbohydrate] DECIMAL(19,5) NOT NULL, [Sugar] DECIMAL(19,5) NOT NULL, [Lipid] DECIMAL(19,5) NOT NULL, [Sodium] DECIMAL(19,5) NOT NULL, [IngredientGroupId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Ingredient] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Ingredient_IngredientGroup Ingredient(IngredientGroupId) IngredientGroup(Id) */
ALTER TABLE [dbo].[Ingredient] ADD CONSTRAINT [FK_Ingredient_IngredientGroup] FOREIGN KEY ([IngredientGroupId]) REFERENCES [dbo].[IngredientGroup] ([Id])

/* CreateTable SaucerConfiguration */
CREATE TABLE [dbo].[SaucerConfiguration] ([Id] INT NOT NULL IDENTITY(1,1), [SaucerId] INT NOT NULL, [IngredientId] INT NOT NULL, [Amount] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_SaucerConfiguration] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_SaucerConfiguration_Saucer SaucerConfiguration(SaucerId) Saucer(Id) */
ALTER TABLE [dbo].[SaucerConfiguration] ADD CONSTRAINT [FK_SaucerConfiguration_Saucer] FOREIGN KEY ([SaucerId]) REFERENCES [dbo].[Saucer] ([Id])

/* CreateForeignKey FK_SaucerConfiguration_Ingredient SaucerConfiguration(IngredientId) Ingredient(Id) */
ALTER TABLE [dbo].[SaucerConfiguration] ADD CONSTRAINT [FK_SaucerConfiguration_Ingredient] FOREIGN KEY ([IngredientId]) REFERENCES [dbo].[Ingredient] ([Id])

/* CreateTable Worker */
CREATE TABLE [dbo].[Worker] ([Id] INT NOT NULL IDENTITY(1,1), [Code] NVARCHAR(250) NOT NULL, [FirstName] NVARCHAR(250) NOT NULL, [LastName] NVARCHAR(250) NOT NULL, [Email] NVARCHAR(250) NOT NULL, [Imss] NVARCHAR(250) NOT NULL, [Gender] INT NOT NULL, [Badge] NVARCHAR(250) NOT NULL, [PublicKey] NVARCHAR(250), [Time] NVARCHAR(250), [DepartmentId] INT NOT NULL, [JobId] INT NOT NULL, [DealerId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Worker] PRIMARY KEY ([Id]))

/* CreateIndex Worker (Email) */
CREATE UNIQUE INDEX [IX_Worker_Email] ON [dbo].[Worker] ([Email] ASC)

/* CreateIndex Worker (Imss) */
CREATE UNIQUE INDEX [IX_Worker_Imss] ON [dbo].[Worker] ([Imss] ASC)

/* CreateIndex Worker (Badge) */
CREATE UNIQUE INDEX [IX_Worker_Badge] ON [dbo].[Worker] ([Badge] ASC)

/* CreateForeignKey FK_Worker_Department Worker(DepartmentId) Department(Id) */
ALTER TABLE [dbo].[Worker] ADD CONSTRAINT [FK_Worker_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id])

/* CreateForeignKey FK_Worker_Job Worker(JobId) Job(Id) */
ALTER TABLE [dbo].[Worker] ADD CONSTRAINT [FK_Worker_Job] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Job] ([Id])

/* CreateForeignKey FK_Worker_Dealer Worker(DealerId) Dealer(Id) */
ALTER TABLE [dbo].[Worker] ADD CONSTRAINT [FK_Worker_Dealer] FOREIGN KEY ([DealerId]) REFERENCES [dbo].[Dealer] ([Id])

/* CreateTable Menu */
CREATE TABLE [dbo].[Menu] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [DayWeek] INT NOT NULL, [Type] INT, [Limit] INT, [StartDate] DATETIME NOT NULL, [EndDate] DATETIME NOT NULL, [MaxAmount] INT, [DealerId] INT NOT NULL, [SaucerId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Menu] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Menu_Dealer Menu(DealerId) Dealer(Id) */
ALTER TABLE [dbo].[Menu] ADD CONSTRAINT [FK_Menu_Dealer] FOREIGN KEY ([DealerId]) REFERENCES [dbo].[Dealer] ([Id])

/* CreateForeignKey FK_Menu_Saucer Menu(SaucerId) Saucer(Id) */
ALTER TABLE [dbo].[Menu] ADD CONSTRAINT [FK_Menu_Saucer] FOREIGN KEY ([SaucerId]) REFERENCES [dbo].[Saucer] ([Id])

/* CreateTable Reservation */
CREATE TABLE [dbo].[Reservation] ([Id] INT NOT NULL IDENTITY(1,1), [Date] DATETIME NOT NULL, [Energy] DECIMAL(19,5) NOT NULL, [Protein] DECIMAL(19,5) NOT NULL, [Carbohydrate] DECIMAL(19,5) NOT NULL, [Sugar] DECIMAL(19,5) NOT NULL, [Lipid] DECIMAL(19,5) NOT NULL, [Sodium] DECIMAL(19,5) NOT NULL, [WorkerId] INT NOT NULL, [SaucerId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Reservation] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Reservation_Worker Reservation(WorkerId) Worker(Id) */
ALTER TABLE [dbo].[Reservation] ADD CONSTRAINT [FK_Reservation_Worker] FOREIGN KEY ([WorkerId]) REFERENCES [dbo].[Worker] ([Id])

/* CreateForeignKey FK_Reservation_Saucer Reservation(SaucerId) Saucer(Id) */
ALTER TABLE [dbo].[Reservation] ADD CONSTRAINT [FK_Reservation_Saucer] FOREIGN KEY ([SaucerId]) REFERENCES [dbo].[Saucer] ([Id])

/* ExecuteSqlStatement INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Yucatan', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Campeche', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Yucatan', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Campeche', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Company (Name, RegionId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Bepensa Industria', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Bepensa Bebidas', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Company (Name, RegionId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Bepensa Industria', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Bepensa Bebidas', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Branch (Name, Code, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Opesystem', 'CODE1', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Finbe', 'CODE2', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Branch (Name, Code, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Opesystem', 'CODE1', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Finbe', 'CODE2', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Department (Name, BranchId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Desarrollo', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('HelpDesk', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Department (Name, BranchId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Desarrollo', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('HelpDesk', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO [User] (Name, UserName, Password, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('admin', 'admin', 'JRxZJ9O9m6Y=',1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO [User] (Name, UserName, Password, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('admin', 'admin', 'JRxZJ9O9m6Y=',1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Enfermedad cardiaca', 'CODE1',1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Diabetes', 'CODE2',1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Enfermedad cardiaca', 'CODE1',1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Diabetes', 'CODE2',1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Te estas pasando de calorias', 'CODE1', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Cuidado con tu alimentacion', 'CODE2', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Te estas pasando de calorias', 'CODE1', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Cuidado con tu alimentacion', 'CODE2', 1,1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Job (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Secretaria', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Programador', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Job (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Secretaria', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Programador', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Tip (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Nunca olvides que el desayuno es primordial', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Evita catalogar los alimentos', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Tip (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Nunca olvides que el desayuno es primordial', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Evita catalogar los alimentos', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Dealer (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Areca', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Cocina Walter', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Dealer (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Areca', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Cocina Walter', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO BranchDealer (BranchId, DealerId) VALUES (1,1), (1,2) */
INSERT INTO BranchDealer (BranchId, DealerId) VALUES (1,1), (1,2)

/* ExecuteSqlStatement INSERT INTO Saucer (Name, Type, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol con puerco', 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Pechuga asada', 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Saucer (Name, Type, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol con puerco', 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Pechuga asada', 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO DealerSaucer (DealerId, SaucerId) VALUES (1,1), (1,2) */
INSERT INTO DealerSaucer (DealerId, SaucerId) VALUES (1,1), (1,2)

/* ExecuteSqlStatement INSERT INTO SaucerMultimedia (Path, SaucerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol1.jpg', 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Frijol2.jpg', 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO SaucerMultimedia (Path, SaucerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol1.jpg', 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Frijol2.jpg', 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO IngredientGroup (Name, Color, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Carnes y Pescado', 'Rojo', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Verduras y Frutas', 'Verde', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO IngredientGroup (Name, Color, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Carnes y Pescado', 'Rojo', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Verduras y Frutas', 'Verde', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Ingredient (Name, Amount, Energy, Protein, Carbohydrate, Sugar, Lipid, Sodium, IngredientGroupId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol', 100, 10, 10, 10, 10, 10, 10, 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Puerco', 100, 10, 10, 10, 10, 10, 10, 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Ingredient (Name, Amount, Energy, Protein, Carbohydrate, Sugar, Lipid, Sodium, IngredientGroupId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol', 100, 10, 10, 10, 10, 10, 10, 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('Puerco', 100, 10, 10, 10, 10, 10, 10, 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO SaucerConfiguration (SaucerId, IngredientId, Amount, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (1, 1, 3, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),(1, 2, 3, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO SaucerConfiguration (SaucerId, IngredientId, Amount, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (1, 1, 3, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),(1, 2, 3, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

/* ExecuteSqlStatement INSERT INTO Worker (Code, FirstName, LastName, Email, Imss, Gender, Badge, DepartmentId, JobId, DealerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('1122', 'Juan', 'Martinez', 'juan@gmail.com', 'WV12H78', 1, '010107002113774', 1, 1, 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('3344', 'Luis', 'Hernandez', 'luis@gmail.com', 'kV34H23', 1, '010107002112355', 1, 1, 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1) */
INSERT INTO Worker (Code, FirstName, LastName, Email, Imss, Gender, Badge, DepartmentId, JobId, DealerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('1122', 'Juan', 'Martinez', 'juan@gmail.com', 'WV12H78', 1, '010107002113774', 1, 1, 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1),('3344', 'Luis', 'Hernandez', 'luis@gmail.com', 'kV34H23', 1, '010107002112355', 1, 1, 1, 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (1, '2016-04-11T05:08:10', '_1_Seed')
/* Committing Transaction */
/* 1: _1_Seed migrated */

/* 2: _2_AddRoles migrating ================================================== */

/* Beginning Transaction */
/* CreateTable AccessLevel */
CREATE TABLE [dbo].[AccessLevel] ([Id] INT NOT NULL, [Name] NVARCHAR(250) NOT NULL, [Description] NVARCHAR(250) NOT NULL, CONSTRAINT [PK_AccessLevel] PRIMARY KEY ([Id]))

/* ExecuteSqlStatement INSERT INTO AccessLevel (Id, Name, Description) VALUES (1, 'Post', 'Crear'),(2, 'Put', 'Actualizar'),(3, 'Get', 'Ver'),(4, 'Delete', 'Eliminar') */
INSERT INTO AccessLevel (Id, Name, Description) VALUES (1, 'Post', 'Crear'),(2, 'Put', 'Actualizar'),(3, 'Get', 'Ver'),(4, 'Delete', 'Eliminar')

/* CreateTable Permission */
CREATE TABLE [dbo].[Permission] ([Id] INT NOT NULL, [Name] NVARCHAR(250) NOT NULL, [Description] NVARCHAR(250) NOT NULL, CONSTRAINT [PK_Permission] PRIMARY KEY ([Id]))

/* ExecuteSqlStatement INSERT INTO Permission (Id, Name, Description) VALUES (1, 'AccessLevel', 'Niveles de acceso'),(2, 'Permission', 'Permisos'),(3, 'Role', 'Roles'),(4, 'User', 'Usuarios'),(5, 'Worker', 'Trabajadores'),(6, 'Region', 'Regiones'),(7, 'Company', 'Companias'),(8, 'Branch', 'Sucursales'),(9, 'Department', 'Departamentos'),(10, 'Disease', 'Enfermedades'),(11, 'Warning', 'Alertas'),(12, 'Tip', 'Consejos'),(13, 'Job', 'Puestos'),(14, 'Dealer', 'Distribuidores'),(15, 'Menu', 'Menus'),(16, 'Saucer', 'Platillos'),(17, 'SaucerMultimedia', 'Multimedias'),(18, 'SaucerConfiguration', 'Configuracion de platillos'),(19, 'Ingredient', 'Ingredientes'),(20, 'IngredientGroup', 'Grupo de ingredientes'),(21, 'Reservation', 'Reservaciones') */
INSERT INTO Permission (Id, Name, Description) VALUES (1, 'AccessLevel', 'Niveles de acceso'),(2, 'Permission', 'Permisos'),(3, 'Role', 'Roles'),(4, 'User', 'Usuarios'),(5, 'Worker', 'Trabajadores'),(6, 'Region', 'Regiones'),(7, 'Company', 'Companias'),(8, 'Branch', 'Sucursales'),(9, 'Department', 'Departamentos'),(10, 'Disease', 'Enfermedades'),(11, 'Warning', 'Alertas'),(12, 'Tip', 'Consejos'),(13, 'Job', 'Puestos'),(14, 'Dealer', 'Distribuidores'),(15, 'Menu', 'Menus'),(16, 'Saucer', 'Platillos'),(17, 'SaucerMultimedia', 'Multimedias'),(18, 'SaucerConfiguration', 'Configuracion de platillos'),(19, 'Ingredient', 'Ingredientes'),(20, 'IngredientGroup', 'Grupo de ingredientes'),(21, 'Reservation', 'Reservaciones')

/* CreateTable PermissionAccessLevel */
CREATE TABLE [dbo].[PermissionAccessLevel] ([PermissionId] INT NOT NULL, [AccessLevelId] INT NOT NULL)

/* CreateForeignKey FK_PermissionAccessLevel_Permission PermissionAccessLevel(PermissionId) Permission(Id) */
ALTER TABLE [dbo].[PermissionAccessLevel] ADD CONSTRAINT [FK_PermissionAccessLevel_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([Id])

/* CreateForeignKey FK_PermissionAccessLevel_AccessLevel PermissionAccessLevel(AccessLevelId) AccessLevel(Id) */
ALTER TABLE [dbo].[PermissionAccessLevel] ADD CONSTRAINT [FK_PermissionAccessLevel_AccessLevel] FOREIGN KEY ([AccessLevelId]) REFERENCES [dbo].[AccessLevel] ([Id])

/* CreateIndex PermissionAccessLevel (PermissionId, AccessLevelId) */
CREATE UNIQUE INDEX [IX_PermissionAccessLevel] ON [dbo].[PermissionAccessLevel] ([PermissionId] ASC, [AccessLevelId] ASC)

/* ExecuteSqlStatement INSERT INTO PermissionAccessLevel (PermissionId, AccessLevelId) VALUES (1, 3),(2, 3),(3, 1),(3, 2),(3, 3),(3, 4),(4, 1),(4, 2),(4, 3),(5, 1),(5, 2),(5, 3),(6, 1),(6, 2),(6, 3),(6, 4),(7, 1),(7, 2),(7, 3),(7, 4),(8, 1),(8, 2),(8, 3),(8, 4),(9, 1),(9, 2),(9, 3),(9, 4),(10, 1),(10, 2),(10, 3),(10, 4),(11, 1),(11, 2),(11, 3),(11, 4),(12, 1),(12, 2),(12, 3),(12, 4),(13, 1),(13, 2),(13, 3),(13, 4),(14, 1),(14, 2),(14, 3),(14, 4),(15, 1),(15, 2),(15, 3),(15, 4),(16, 1),(16, 2),(16, 3),(16, 4),(17, 1),(17, 2),(17, 3),(17, 4),(18, 1),(18, 2),(18, 3),(18, 4),(19, 1),(19, 2),(19, 3),(19, 4),(20, 1),(20, 2),(20, 3),(20, 4),(21, 1),(21, 2),(21, 3),(21, 4) */
INSERT INTO PermissionAccessLevel (PermissionId, AccessLevelId) VALUES (1, 3),(2, 3),(3, 1),(3, 2),(3, 3),(3, 4),(4, 1),(4, 2),(4, 3),(5, 1),(5, 2),(5, 3),(6, 1),(6, 2),(6, 3),(6, 4),(7, 1),(7, 2),(7, 3),(7, 4),(8, 1),(8, 2),(8, 3),(8, 4),(9, 1),(9, 2),(9, 3),(9, 4),(10, 1),(10, 2),(10, 3),(10, 4),(11, 1),(11, 2),(11, 3),(11, 4),(12, 1),(12, 2),(12, 3),(12, 4),(13, 1),(13, 2),(13, 3),(13, 4),(14, 1),(14, 2),(14, 3),(14, 4),(15, 1),(15, 2),(15, 3),(15, 4),(16, 1),(16, 2),(16, 3),(16, 4),(17, 1),(17, 2),(17, 3),(17, 4),(18, 1),(18, 2),(18, 3),(18, 4),(19, 1),(19, 2),(19, 3),(19, 4),(20, 1),(20, 2),(20, 3),(20, 4),(21, 1),(21, 2),(21, 3),(21, 4)

/* CreateTable Role */
CREATE TABLE [dbo].[Role] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Role] PRIMARY KEY ([Id]))

INSERT INTO [dbo].[Role] ([Name], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [Status], [IsActive]) VALUES ('Administrador', 1, 1, '04/11/2016 00:08:10', '04/11/2016 00:08:10', 1, 1)
/* CreateTable RoleConfiguration */
CREATE TABLE [dbo].[RoleConfiguration] ([Id] INT NOT NULL IDENTITY(1,1), [RoleId] INT NOT NULL, [PermissionId] INT NOT NULL, [AccessLevelId] INT NOT NULL, CONSTRAINT [PK_RoleConfiguration] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_RoleConfiguration_Role RoleConfiguration(RoleId) Role(Id) */
ALTER TABLE [dbo].[RoleConfiguration] ADD CONSTRAINT [FK_RoleConfiguration_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])

/* CreateForeignKey FK_RoleConfiguration_Permission RoleConfiguration(PermissionId) Permission(Id) */
ALTER TABLE [dbo].[RoleConfiguration] ADD CONSTRAINT [FK_RoleConfiguration_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([Id])

/* CreateForeignKey FK_RoleConfiguration_AccessLevel RoleConfiguration(AccessLevelId) AccessLevel(Id) */
ALTER TABLE [dbo].[RoleConfiguration] ADD CONSTRAINT [FK_RoleConfiguration_AccessLevel] FOREIGN KEY ([AccessLevelId]) REFERENCES [dbo].[AccessLevel] ([Id])

/* CreateIndex RoleConfiguration (RoleId, PermissionId, AccessLevelId) */
CREATE UNIQUE INDEX [IX_RoleConfiguration] ON [dbo].[RoleConfiguration] ([RoleId] ASC, [PermissionId] ASC, [AccessLevelId] ASC)

/* ExecuteSqlStatement INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId) VALUES (1, 1, 3),(1, 2, 3),(1, 3, 1),(1, 3, 2),(1, 3, 3),(1, 3, 4),(1, 4, 1),(1, 4, 2),(1, 4, 3),(1, 5, 1),(1, 5, 2),(1, 5, 3),(1, 6, 1),(1, 6, 2),(1, 6, 3),(1, 6, 4),(1, 7, 1),(1, 7, 2),(1, 7, 3),(1, 7, 4),(1, 8, 1),(1, 8, 2),(1, 8, 3),(1, 8, 4),(1, 9, 1),(1, 9, 2),(1, 9, 3),(1, 9, 4),(1, 10, 1),(1, 10, 2),(1, 10, 3),(1, 10, 4),(1, 11, 1),(1, 11, 2),(1, 11, 3),(1, 11, 4),(1, 12, 1),(1, 12, 2),(1, 12, 3),(1, 12, 4),(1, 13, 1),(1, 13, 2),(1, 13, 3),(1, 13, 4),(1, 14, 1),(1, 14, 2),(1, 14, 3),(1, 14, 4),(1, 15, 1),(1, 15, 2),(1, 15, 3),(1, 15, 4),(1, 16, 1),(1, 16, 2),(1, 16, 3),(1, 16, 4),(1, 17, 1),(1, 17, 2),(1, 17, 3),(1, 17, 4),(1, 18, 1),(1, 18, 2),(1, 18, 3),(1, 18, 4),(1, 19, 1),(1, 19, 2),(1, 19, 3),(1, 19, 4),(1, 20, 1),(1, 20, 2),(1, 20, 3),(1, 20, 4),(1, 21, 1),(1, 21, 2),(1, 21, 3),(1, 21, 4) */
INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId) VALUES (1, 1, 3),(1, 2, 3),(1, 3, 1),(1, 3, 2),(1, 3, 3),(1, 3, 4),(1, 4, 1),(1, 4, 2),(1, 4, 3),(1, 5, 1),(1, 5, 2),(1, 5, 3),(1, 6, 1),(1, 6, 2),(1, 6, 3),(1, 6, 4),(1, 7, 1),(1, 7, 2),(1, 7, 3),(1, 7, 4),(1, 8, 1),(1, 8, 2),(1, 8, 3),(1, 8, 4),(1, 9, 1),(1, 9, 2),(1, 9, 3),(1, 9, 4),(1, 10, 1),(1, 10, 2),(1, 10, 3),(1, 10, 4),(1, 11, 1),(1, 11, 2),(1, 11, 3),(1, 11, 4),(1, 12, 1),(1, 12, 2),(1, 12, 3),(1, 12, 4),(1, 13, 1),(1, 13, 2),(1, 13, 3),(1, 13, 4),(1, 14, 1),(1, 14, 2),(1, 14, 3),(1, 14, 4),(1, 15, 1),(1, 15, 2),(1, 15, 3),(1, 15, 4),(1, 16, 1),(1, 16, 2),(1, 16, 3),(1, 16, 4),(1, 17, 1),(1, 17, 2),(1, 17, 3),(1, 17, 4),(1, 18, 1),(1, 18, 2),(1, 18, 3),(1, 18, 4),(1, 19, 1),(1, 19, 2),(1, 19, 3),(1, 19, 4),(1, 20, 1),(1, 20, 2),(1, 20, 3),(1, 20, 4),(1, 21, 1),(1, 21, 2),(1, 21, 3),(1, 21, 4)

/* CreateColumn User RoleId Int32 */
ALTER TABLE [dbo].[User] ADD [RoleId] INT

/* ExecuteSqlStatement UPDATE [User] SET RoleId = 1 */
UPDATE [User] SET RoleId = 1

/* AlterColumn User RoleId Int32 */
ALTER TABLE [dbo].[User] ALTER COLUMN [RoleId] INT NOT NULL

/* CreateForeignKey FK_User_Role User(RoleId) Role(Id) */
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])

/* -> 1 Insert operations completed in 00:00:00.0010000 taking an average of 00:00:00.0010000 */
INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (2, '2016-04-11T05:08:10', '_2_AddRoles')
/* Committing Transaction */
/* 2: _2_AddRoles migrated */

/* Task completed. */
