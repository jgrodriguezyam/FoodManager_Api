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

/* CreateIndex SaucerConfiguration (SaucerId, IngredientId) */
CREATE UNIQUE INDEX [IX_SaucerConfiguration] ON [dbo].[SaucerConfiguration] ([SaucerId] ASC, [IngredientId] ASC)

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

/* ExecuteSqlStatement INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Yucatan', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Campeche', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Yucatan', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Campeche', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Company (Name, RegionId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Bepensa Industria', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Bepensa Bebidas', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Company (Name, RegionId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Bepensa Industria', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Bepensa Bebidas', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Branch (Name, Code, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Opesystem', 'CODE1', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Finbe', 'CODE2', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Branch (Name, Code, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Opesystem', 'CODE1', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Finbe', 'CODE2', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Department (Name, BranchId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Desarrollo', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('HelpDesk', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Department (Name, BranchId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Desarrollo', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('HelpDesk', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO [User] (Name, UserName, Password, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('admin', 'admin', 'JRxZJ9O9m6Y=',1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO [User] (Name, UserName, Password, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('admin', 'admin', 'JRxZJ9O9m6Y=',1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Enfermedad cardiaca', 'CODE1',1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Diabetes', 'CODE2',1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Enfermedad cardiaca', 'CODE1',1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Diabetes', 'CODE2',1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Te estas pasando de calorias', 'CODE1', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Cuidado con tu alimentacion', 'CODE2', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Te estas pasando de calorias', 'CODE1', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Cuidado con tu alimentacion', 'CODE2', 1,1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Job (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Secretaria', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Programador', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Job (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Secretaria', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Programador', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Tip (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Nunca olvides que el desayuno es primordial', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Evita catalogar los alimentos', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Tip (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Nunca olvides que el desayuno es primordial', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Evita catalogar los alimentos', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Dealer (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Areca', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Cocina Walter', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Dealer (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Areca', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Cocina Walter', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO BranchDealer (BranchId, DealerId) VALUES (1,1), (1,2) */
INSERT INTO BranchDealer (BranchId, DealerId) VALUES (1,1), (1,2)

/* ExecuteSqlStatement INSERT INTO Saucer (Name, Type, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol con puerco', 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Pechuga asada', 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Saucer (Name, Type, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol con puerco', 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Pechuga asada', 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO DealerSaucer (DealerId, SaucerId) VALUES (1,1), (1,2) */
INSERT INTO DealerSaucer (DealerId, SaucerId) VALUES (1,1), (1,2)

/* ExecuteSqlStatement INSERT INTO SaucerMultimedia (Path, SaucerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol1.jpg', 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Frijol2.jpg', 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO SaucerMultimedia (Path, SaucerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol1.jpg', 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Frijol2.jpg', 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO IngredientGroup (Name, Color, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Carnes y Pescado', 'Rojo', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Verduras y Frutas', 'Verde', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO IngredientGroup (Name, Color, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Carnes y Pescado', 'Rojo', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Verduras y Frutas', 'Verde', 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Ingredient (Name, Amount, Energy, Protein, Carbohydrate, Sugar, Lipid, Sodium, IngredientGroupId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol', 100, 10, 10, 10, 10, 10, 10, 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Puerco', 100, 10, 10, 10, 10, 10, 10, 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Ingredient (Name, Amount, Energy, Protein, Carbohydrate, Sugar, Lipid, Sodium, IngredientGroupId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Frijol', 100, 10, 10, 10, 10, 10, 10, 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('Puerco', 100, 10, 10, 10, 10, 10, 10, 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO SaucerConfiguration (SaucerId, IngredientId, Amount, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (1, 1, 3, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),(1, 2, 3, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO SaucerConfiguration (SaucerId, IngredientId, Amount, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (1, 1, 3, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),(1, 2, 3, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

/* ExecuteSqlStatement INSERT INTO Worker (Code, FirstName, LastName, Email, Imss, Gender, Badge, DepartmentId, JobId, DealerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('1122', 'Juan', 'Martinez', 'juan@gmail.com', 'WV12H78', 1, '010107002113774', 1, 1, 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('3344', 'Luis', 'Hernandez', 'luis@gmail.com', 'kV34H23', 1, '010107002112355', 1, 1, 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1) */
INSERT INTO Worker (Code, FirstName, LastName, Email, Imss, Gender, Badge, DepartmentId, JobId, DealerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('1122', 'Juan', 'Martinez', 'juan@gmail.com', 'WV12H78', 1, '010107002113774', 1, 1, 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1),('3344', 'Luis', 'Hernandez', 'luis@gmail.com', 'kV34H23', 1, '010107002112355', 1, 1, 1, 1, 1, '04/05/2016 17:15:17', '04/05/2016 17:15:17', 1, 1)

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (1, '2016-04-05T22:15:18', '_1_Seed')
/* Committing Transaction */
/* 1: _1_Seed migrated */

/* Task completed. */
