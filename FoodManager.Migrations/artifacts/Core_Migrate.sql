/* Using Database sqlserver2008 and Connection String Server=link_jorge_HP\localhost; Database=FoodManager; User Id=sa; Password=********; */
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

/* 1: _1_SeedRoles migrating ================================================= */

/* Beginning Transaction */
/* CreateTable AccessLevel */
CREATE TABLE [dbo].[AccessLevel] ([Id] INT NOT NULL, [Name] NVARCHAR(250) NOT NULL, [Description] NVARCHAR(250) NOT NULL, CONSTRAINT [PK_AccessLevel] PRIMARY KEY ([Id]))

/* ExecuteSqlStatement INSERT INTO AccessLevel (Id, Name, Description) VALUES (1, 'Post', 'Crear'),(2, 'Put', 'Actualizar'),(3, 'Get', 'Ver'),(4, 'Delete', 'Eliminar'),(5, 'ChangeStatus', 'Actualizar Estado'),(6, 'Assign', 'Asignar'),(7, 'Unassign', 'Desasignar'),(8, 'ChangePassword', 'Actualizar Contraseña'),(9, 'Report', 'Reportes') */
INSERT INTO AccessLevel (Id, Name, Description) VALUES (1, 'Post', 'Crear'),(2, 'Put', 'Actualizar'),(3, 'Get', 'Ver'),(4, 'Delete', 'Eliminar'),(5, 'ChangeStatus', 'Actualizar Estado'),(6, 'Assign', 'Asignar'),(7, 'Unassign', 'Desasignar'),(8, 'ChangePassword', 'Actualizar Contraseña'),(9, 'Report', 'Reportes')

/* CreateTable Permission */
CREATE TABLE [dbo].[Permission] ([Id] INT NOT NULL, [Name] NVARCHAR(250) NOT NULL, [Description] NVARCHAR(250) NOT NULL, CONSTRAINT [PK_Permission] PRIMARY KEY ([Id]))

/* ExecuteSqlStatement INSERT INTO Permission (Id, Name, Description) VALUES (1, 'AccessLevel', 'Niveles de acceso'),(2, 'Permission', 'Permisos'),(3, 'Role', 'Roles'),(4, 'RoleConfiguration', 'Configuracion de roles'),(5, 'User', 'Usuarios'),(6, 'Worker', 'Trabajadores'),(7, 'Region', 'Regiones'),(8, 'Company', 'Companias'),(9, 'Branch', 'Sucursales'),(10, 'Department', 'Departamentos'),(11, 'Disease', 'Enfermedades'),(12, 'Warning', 'Alertas'),(13, 'Tip', 'Consejos'),(14, 'Job', 'Puestos'),(15, 'Dealer', 'Distribuidores'),(16, 'Menu', 'Menus'),(17, 'Saucer', 'Platillos'),(18, 'SaucerMultimedia', 'Multimedias'),(19, 'SaucerConfiguration', 'Configuracion de platillos'),(20, 'Ingredient', 'Ingredientes'),(21, 'IngredientGroup', 'Grupo de ingredientes'),(22, 'Reservation', 'Reservaciones') */
INSERT INTO Permission (Id, Name, Description) VALUES (1, 'AccessLevel', 'Niveles de acceso'),(2, 'Permission', 'Permisos'),(3, 'Role', 'Roles'),(4, 'RoleConfiguration', 'Configuracion de roles'),(5, 'User', 'Usuarios'),(6, 'Worker', 'Trabajadores'),(7, 'Region', 'Regiones'),(8, 'Company', 'Companias'),(9, 'Branch', 'Sucursales'),(10, 'Department', 'Departamentos'),(11, 'Disease', 'Enfermedades'),(12, 'Warning', 'Alertas'),(13, 'Tip', 'Consejos'),(14, 'Job', 'Puestos'),(15, 'Dealer', 'Distribuidores'),(16, 'Menu', 'Menus'),(17, 'Saucer', 'Platillos'),(18, 'SaucerMultimedia', 'Multimedias'),(19, 'SaucerConfiguration', 'Configuracion de platillos'),(20, 'Ingredient', 'Ingredientes'),(21, 'IngredientGroup', 'Grupo de ingredientes'),(22, 'Reservation', 'Reservaciones')

/* CreateTable PermissionAccessLevel */
CREATE TABLE [dbo].[PermissionAccessLevel] ([PermissionId] INT NOT NULL, [AccessLevelId] INT NOT NULL)

/* CreateForeignKey FK_PermissionAccessLevel_Permission PermissionAccessLevel(PermissionId) Permission(Id) */
ALTER TABLE [dbo].[PermissionAccessLevel] ADD CONSTRAINT [FK_PermissionAccessLevel_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([Id])

/* CreateForeignKey FK_PermissionAccessLevel_AccessLevel PermissionAccessLevel(AccessLevelId) AccessLevel(Id) */
ALTER TABLE [dbo].[PermissionAccessLevel] ADD CONSTRAINT [FK_PermissionAccessLevel_AccessLevel] FOREIGN KEY ([AccessLevelId]) REFERENCES [dbo].[AccessLevel] ([Id])

/* CreateIndex PermissionAccessLevel (PermissionId, AccessLevelId) */
CREATE UNIQUE INDEX [IX_PermissionAccessLevel] ON [dbo].[PermissionAccessLevel] ([PermissionId] ASC, [AccessLevelId] ASC)

/* ExecuteSqlStatement INSERT INTO PermissionAccessLevel (PermissionId, AccessLevelId) VALUES (1, 3),(2, 3),(3, 1),(3, 2),(3, 3),(3, 4),(3, 5),(4, 1),(4, 2),(4, 3),(4, 4),(5, 1),(5, 2),(5, 3),(5, 5),(5, 8),(6, 1),(6, 2),(6, 3),(6, 5),(6, 9),(7, 1),(7, 2),(7, 3),(7, 4),(7, 5),(8, 1),(8, 2),(8, 3),(8, 4),(8, 5),(9, 1),(9, 2),(9, 3),(9, 4),(9, 5),(9, 6),(9, 7),(10, 1),(10, 2),(10, 3),(10, 4),(10, 5),(11, 1),(11, 2),(11, 3),(11, 4),(11, 5),(12, 1),(12, 2),(12, 3),(12, 4),(12, 5),(13, 1),(13, 2),(13, 3),(13, 4),(13, 5),(14, 1),(14, 2),(14, 3),(14, 4),(14, 5),(15, 1),(15, 2),(15, 3),(15, 4),(15, 5),(16, 1),(16, 2),(16, 3),(16, 4),(16, 5),(17, 1),(17, 2),(17, 3),(17, 4),(17, 5),(17, 9),(18, 1),(18, 2),(18, 3),(18, 4),(18, 5),(19, 1),(19, 2),(19, 3),(19, 4),(19, 5),(20, 1),(20, 2),(20, 3),(20, 4),(20, 5),(21, 1),(21, 2),(21, 3),(21, 4),(21, 5),(22, 1),(22, 2),(22, 3),(22, 4),(22, 5),(22, 9) */
INSERT INTO PermissionAccessLevel (PermissionId, AccessLevelId) VALUES (1, 3),(2, 3),(3, 1),(3, 2),(3, 3),(3, 4),(3, 5),(4, 1),(4, 2),(4, 3),(4, 4),(5, 1),(5, 2),(5, 3),(5, 5),(5, 8),(6, 1),(6, 2),(6, 3),(6, 5),(6, 9),(7, 1),(7, 2),(7, 3),(7, 4),(7, 5),(8, 1),(8, 2),(8, 3),(8, 4),(8, 5),(9, 1),(9, 2),(9, 3),(9, 4),(9, 5),(9, 6),(9, 7),(10, 1),(10, 2),(10, 3),(10, 4),(10, 5),(11, 1),(11, 2),(11, 3),(11, 4),(11, 5),(12, 1),(12, 2),(12, 3),(12, 4),(12, 5),(13, 1),(13, 2),(13, 3),(13, 4),(13, 5),(14, 1),(14, 2),(14, 3),(14, 4),(14, 5),(15, 1),(15, 2),(15, 3),(15, 4),(15, 5),(16, 1),(16, 2),(16, 3),(16, 4),(16, 5),(17, 1),(17, 2),(17, 3),(17, 4),(17, 5),(17, 9),(18, 1),(18, 2),(18, 3),(18, 4),(18, 5),(19, 1),(19, 2),(19, 3),(19, 4),(19, 5),(20, 1),(20, 2),(20, 3),(20, 4),(20, 5),(21, 1),(21, 2),(21, 3),(21, 4),(21, 5),(22, 1),(22, 2),(22, 3),(22, 4),(22, 5),(22, 9)

/* CreateTable Role */
CREATE TABLE [dbo].[Role] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Role] PRIMARY KEY ([Id]))

INSERT INTO [dbo].[Role] ([Name], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [Status], [IsActive]) VALUES ('Administrador', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)
INSERT INTO [dbo].[Role] ([Name], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [Status], [IsActive]) VALUES ('Concesionario', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)
INSERT INTO [dbo].[Role] ([Name], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [Status], [IsActive]) VALUES ('Empleado', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)
INSERT INTO [dbo].[Role] ([Name], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [Status], [IsActive]) VALUES ('Médico', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)
INSERT INTO [dbo].[Role] ([Name], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [Status], [IsActive]) VALUES ('Nutriólogo', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)
/* CreateTable RoleConfiguration */
CREATE TABLE [dbo].[RoleConfiguration] ([Id] INT NOT NULL IDENTITY(1,1), [RoleId] INT NOT NULL, [PermissionId] INT NOT NULL, [AccessLevelId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_RoleConfiguration] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_RoleConfiguration_Role RoleConfiguration(RoleId) Role(Id) */
ALTER TABLE [dbo].[RoleConfiguration] ADD CONSTRAINT [FK_RoleConfiguration_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])

/* CreateForeignKey FK_RoleConfiguration_Permission RoleConfiguration(PermissionId) Permission(Id) */
ALTER TABLE [dbo].[RoleConfiguration] ADD CONSTRAINT [FK_RoleConfiguration_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([Id])

/* CreateForeignKey FK_RoleConfiguration_AccessLevel RoleConfiguration(AccessLevelId) AccessLevel(Id) */
ALTER TABLE [dbo].[RoleConfiguration] ADD CONSTRAINT [FK_RoleConfiguration_AccessLevel] FOREIGN KEY ([AccessLevelId]) REFERENCES [dbo].[AccessLevel] ([Id])

/* CreateIndex RoleConfiguration (RoleId, PermissionId, AccessLevelId) */
CREATE UNIQUE INDEX [IX_RoleConfiguration] ON [dbo].[RoleConfiguration] ([RoleId] ASC, [PermissionId] ASC, [AccessLevelId] ASC)

/* ExecuteSqlStatement INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (1, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 4, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 4, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 4, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 8, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 6, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 7, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (1, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 3, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 4, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 4, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 4, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 5, 8, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 6, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 7, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 8, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 6, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 9, 7, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 10, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 11, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 12, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 13, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 14, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 15, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 16, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 17, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 18, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 19, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 20, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 21, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 5, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(1, 22, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (2, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 5, 8, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 16, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 16, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 16, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 16, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 17, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 17, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 18, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 19, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 20, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 21, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (2, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 5, 8, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 16, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 16, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 16, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 16, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 17, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 17, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 18, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 19, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 20, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(2, 21, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (3, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 6, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 10, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 11, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 12, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 13, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 14, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 16, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 17, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 18, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 19, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 20, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 21, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (3, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 6, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 10, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 11, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 12, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 13, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 14, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 16, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 17, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 18, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 19, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 20, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 21, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 2, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 4, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(3, 22, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (4, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 5, 8, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 10, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 11, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 14, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (4, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 5, 8, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 10, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 11, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 14, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(4, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (5, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 5, 8, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 6, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 6, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 9, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 16, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 17, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 17, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 18, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 19, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 20, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 21, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES (5, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 2, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 3, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 4, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 5, 8, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 6, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 6, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 9, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 15, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 16, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 17, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 17, 9, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 18, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 19, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 20, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),(5, 21, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* -> 5 Insert operations completed in 00:00:00.0050003 taking an average of 00:00:00.0010000 */
INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (1, '2016-12-31T16:40:28', '_1_SeedRoles')
/* Committing Transaction */
/* 1: _1_SeedRoles migrated */

/* 2: _2_Seed migrating ====================================================== */

/* Beginning Transaction */
/* CreateTable Region */
CREATE TABLE [dbo].[Region] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Region] PRIMARY KEY ([Id]))

/* CreateTable Company */
CREATE TABLE [dbo].[Company] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Company] PRIMARY KEY ([Id]))

/* CreateTable Branch */
CREATE TABLE [dbo].[Branch] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Code] NVARCHAR(250) NOT NULL, [RegionId] INT NOT NULL, [CompanyId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Branch] PRIMARY KEY ([Id]))

/* CreateIndex Branch (Code) */
CREATE UNIQUE INDEX [IX_Branch_Code] ON [dbo].[Branch] ([Code] ASC)

/* CreateForeignKey FK_Branch_Region Branch(RegionId) Region(Id) */
ALTER TABLE [dbo].[Branch] ADD CONSTRAINT [FK_Branch_Region] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([Id])

/* CreateForeignKey FK_Branch_Company Branch(CompanyId) Company(Id) */
ALTER TABLE [dbo].[Branch] ADD CONSTRAINT [FK_Branch_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])

/* CreateIndex Branch (RegionId) */
CREATE INDEX [IX_Region] ON [dbo].[Branch] ([RegionId] ASC)

/* CreateIndex Branch (CompanyId) */
CREATE INDEX [IX_Company] ON [dbo].[Branch] ([CompanyId] ASC)

/* CreateTable Department */
CREATE TABLE [dbo].[Department] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Department] PRIMARY KEY ([Id]))

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

/* CreateIndex Warning (DiseaseId) */
CREATE INDEX [IX_Disease] ON [dbo].[Warning] ([DiseaseId] ASC)

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
CREATE TABLE [dbo].[User] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [UserName] NVARCHAR(250) NOT NULL, [Password] NVARCHAR(250) NOT NULL, [PublicKey] NVARCHAR(250), [Time] NVARCHAR(250), [DealerId] INT, [RoleId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_User] PRIMARY KEY ([Id]))

/* CreateIndex User (UserName) */
CREATE UNIQUE INDEX [IX_User_UserName] ON [dbo].[User] ([UserName] ASC)

/* CreateForeignKey FK_User_Dealer User(DealerId) Dealer(Id) */
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Dealer] FOREIGN KEY ([DealerId]) REFERENCES [dbo].[Dealer] ([Id])

/* CreateForeignKey FK_User_Role User(RoleId) Role(Id) */
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])

/* CreateIndex User (DealerId) */
CREATE INDEX [IX_Dealer] ON [dbo].[User] ([DealerId] ASC)

/* CreateIndex User (RoleId) */
CREATE INDEX [IX_Role] ON [dbo].[User] ([RoleId] ASC)

/* CreateTable Saucer */
CREATE TABLE [dbo].[Saucer] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Type] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Saucer] PRIMARY KEY ([Id]))

/* CreateTable SaucerMultimedia */
CREATE TABLE [dbo].[SaucerMultimedia] ([Id] INT NOT NULL IDENTITY(1,1), [Path] NVARCHAR(MAX) NOT NULL, [SaucerId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_SaucerMultimedia] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_SaucerMultimedia_Saucer SaucerMultimedia(SaucerId) Saucer(Id) */
ALTER TABLE [dbo].[SaucerMultimedia] ADD CONSTRAINT [FK_SaucerMultimedia_Saucer] FOREIGN KEY ([SaucerId]) REFERENCES [dbo].[Saucer] ([Id])

/* CreateIndex SaucerMultimedia (SaucerId) */
CREATE INDEX [IX_Saucer] ON [dbo].[SaucerMultimedia] ([SaucerId] ASC)

/* CreateTable IngredientGroup */
CREATE TABLE [dbo].[IngredientGroup] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Color] NVARCHAR(250) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_IngredientGroup] PRIMARY KEY ([Id]))

/* CreateTable Ingredient */
CREATE TABLE [dbo].[Ingredient] ([Id] INT NOT NULL IDENTITY(1,1), [Name] NVARCHAR(250) NOT NULL, [Energy] DECIMAL(10,2) NOT NULL, [Protein] DECIMAL(10,2) NOT NULL, [Carbohydrate] DECIMAL(10,2) NOT NULL, [Sugar] DECIMAL(10,2) NOT NULL, [Lipid] DECIMAL(10,2) NOT NULL, [Sodium] DECIMAL(10,2) NOT NULL, [NetWeight] DECIMAL(10,2) NOT NULL, [Unit] INT NOT NULL, [IngredientGroupId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Ingredient] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Ingredient_IngredientGroup Ingredient(IngredientGroupId) IngredientGroup(Id) */
ALTER TABLE [dbo].[Ingredient] ADD CONSTRAINT [FK_Ingredient_IngredientGroup] FOREIGN KEY ([IngredientGroupId]) REFERENCES [dbo].[IngredientGroup] ([Id])

/* CreateIndex Ingredient (IngredientGroupId) */
CREATE INDEX [IX_IngredientGroup] ON [dbo].[Ingredient] ([IngredientGroupId] ASC)

/* CreateTable SaucerConfiguration */
CREATE TABLE [dbo].[SaucerConfiguration] ([Id] INT NOT NULL IDENTITY(1,1), [SaucerId] INT NOT NULL, [IngredientId] INT NOT NULL, [NetWeight] DECIMAL(10,2) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_SaucerConfiguration] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_SaucerConfiguration_Saucer SaucerConfiguration(SaucerId) Saucer(Id) */
ALTER TABLE [dbo].[SaucerConfiguration] ADD CONSTRAINT [FK_SaucerConfiguration_Saucer] FOREIGN KEY ([SaucerId]) REFERENCES [dbo].[Saucer] ([Id])

/* CreateForeignKey FK_SaucerConfiguration_Ingredient SaucerConfiguration(IngredientId) Ingredient(Id) */
ALTER TABLE [dbo].[SaucerConfiguration] ADD CONSTRAINT [FK_SaucerConfiguration_Ingredient] FOREIGN KEY ([IngredientId]) REFERENCES [dbo].[Ingredient] ([Id])

/* CreateIndex SaucerConfiguration (SaucerId) */
CREATE INDEX [IX_Saucer] ON [dbo].[SaucerConfiguration] ([SaucerId] ASC)

/* CreateIndex SaucerConfiguration (IngredientId) */
CREATE INDEX [IX_Ingredient] ON [dbo].[SaucerConfiguration] ([IngredientId] ASC)

/* CreateTable Worker */
CREATE TABLE [dbo].[Worker] ([Id] INT NOT NULL IDENTITY(1,1), [Code] NVARCHAR(250) NOT NULL, [FirstName] NVARCHAR(250) NOT NULL, [LastName] NVARCHAR(250) NOT NULL, [Email] NVARCHAR(250), [Imss] NVARCHAR(250) NOT NULL, [Gender] INT NOT NULL, [Badge] NVARCHAR(250) NOT NULL, [PublicKey] NVARCHAR(250), [Time] NVARCHAR(250), [DepartmentId] INT NOT NULL, [JobId] INT NOT NULL, [RoleId] INT NOT NULL, [BranchId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Worker] PRIMARY KEY ([Id]))

/* CreateIndex Worker (Code) */
CREATE UNIQUE INDEX [IX_Worker_Code] ON [dbo].[Worker] ([Code] ASC)

/* CreateIndex Worker (Imss) */
CREATE UNIQUE INDEX [IX_Worker_Imss] ON [dbo].[Worker] ([Imss] ASC)

/* CreateIndex Worker (Badge) */
CREATE UNIQUE INDEX [IX_Worker_Badge] ON [dbo].[Worker] ([Badge] ASC)

/* CreateForeignKey FK_Worker_Department Worker(DepartmentId) Department(Id) */
ALTER TABLE [dbo].[Worker] ADD CONSTRAINT [FK_Worker_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id])

/* CreateForeignKey FK_Worker_Job Worker(JobId) Job(Id) */
ALTER TABLE [dbo].[Worker] ADD CONSTRAINT [FK_Worker_Job] FOREIGN KEY ([JobId]) REFERENCES [dbo].[Job] ([Id])

/* CreateForeignKey FK_Worker_Role Worker(RoleId) Role(Id) */
ALTER TABLE [dbo].[Worker] ADD CONSTRAINT [FK_Worker_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])

/* CreateForeignKey FK_Worker_Branch Worker(BranchId) Branch(Id) */
ALTER TABLE [dbo].[Worker] ADD CONSTRAINT [FK_Worker_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([Id])

/* CreateIndex Worker (DepartmentId) */
CREATE INDEX [IX_Department] ON [dbo].[Worker] ([DepartmentId] ASC)

/* CreateIndex Worker (JobId) */
CREATE INDEX [IX_Job] ON [dbo].[Worker] ([JobId] ASC)

/* CreateIndex Worker (RoleId) */
CREATE INDEX [IX_Role] ON [dbo].[Worker] ([RoleId] ASC)

/* CreateIndex Worker (BranchId) */
CREATE INDEX [IX_Branch] ON [dbo].[Worker] ([BranchId] ASC)

/* CreateTable Menu */
CREATE TABLE [dbo].[Menu] ([Id] INT NOT NULL IDENTITY(1,1), [Comment] NVARCHAR(250), [DayWeek] INT NOT NULL, [MealType] INT NOT NULL, [StartDate] DATETIME NOT NULL, [EndDate] DATETIME NOT NULL, [MaxAmount] INT NOT NULL, [DealerId] INT NOT NULL, [SaucerId] INT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Menu] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Menu_Dealer Menu(DealerId) Dealer(Id) */
ALTER TABLE [dbo].[Menu] ADD CONSTRAINT [FK_Menu_Dealer] FOREIGN KEY ([DealerId]) REFERENCES [dbo].[Dealer] ([Id])

/* CreateForeignKey FK_Menu_Saucer Menu(SaucerId) Saucer(Id) */
ALTER TABLE [dbo].[Menu] ADD CONSTRAINT [FK_Menu_Saucer] FOREIGN KEY ([SaucerId]) REFERENCES [dbo].[Saucer] ([Id])

/* CreateIndex Menu (DealerId) */
CREATE INDEX [IX_Dealer] ON [dbo].[Menu] ([DealerId] ASC)

/* CreateIndex Menu (SaucerId) */
CREATE INDEX [IX_Saucer] ON [dbo].[Menu] ([SaucerId] ASC)

/* CreateTable Reservation */
CREATE TABLE [dbo].[Reservation] ([Id] INT NOT NULL IDENTITY(1,1), [Date] DATETIME NOT NULL, [Portion] DECIMAL(10,2) NOT NULL, [MealType] INT NOT NULL, [WorkerId] INT NOT NULL, [SaucerId] INT NOT NULL, [DealerId] INT, [IsPaid] BIT NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_Reservation] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_Reservation_Worker Reservation(WorkerId) Worker(Id) */
ALTER TABLE [dbo].[Reservation] ADD CONSTRAINT [FK_Reservation_Worker] FOREIGN KEY ([WorkerId]) REFERENCES [dbo].[Worker] ([Id])

/* CreateForeignKey FK_Reservation_Saucer Reservation(SaucerId) Saucer(Id) */
ALTER TABLE [dbo].[Reservation] ADD CONSTRAINT [FK_Reservation_Saucer] FOREIGN KEY ([SaucerId]) REFERENCES [dbo].[Saucer] ([Id])

/* CreateForeignKey FK_Reservation_Dealer Reservation(DealerId) Dealer(Id) */
ALTER TABLE [dbo].[Reservation] ADD CONSTRAINT [FK_Reservation_Dealer] FOREIGN KEY ([DealerId]) REFERENCES [dbo].[Dealer] ([Id])

/* CreateIndex Reservation (WorkerId) */
CREATE INDEX [IX_Worker] ON [dbo].[Reservation] ([WorkerId] ASC)

/* CreateIndex Reservation (SaucerId) */
CREATE INDEX [IX_Saucer] ON [dbo].[Reservation] ([SaucerId] ASC)

/* CreateIndex Reservation (DealerId) */
CREATE INDEX [IX_Dealer] ON [dbo].[Reservation] ([DealerId] ASC)

/* CreateTable ReservationDetail */
CREATE TABLE [dbo].[ReservationDetail] ([Id] INT NOT NULL IDENTITY(1,1), [ReservationId] INT NOT NULL, [IngredientId] INT NOT NULL, [NetWeight] DECIMAL(10,2) NOT NULL, [CreatedBy] INT NOT NULL, [ModifiedBy] INT NOT NULL, [CreatedOn] DATETIME NOT NULL, [ModifiedOn] DATETIME NOT NULL, [Status] BIT NOT NULL, [IsActive] BIT NOT NULL, CONSTRAINT [PK_ReservationDetail] PRIMARY KEY ([Id]))

/* CreateForeignKey FK_ReservationDetail_Reservation ReservationDetail(ReservationId) Reservation(Id) */
ALTER TABLE [dbo].[ReservationDetail] ADD CONSTRAINT [FK_ReservationDetail_Reservation] FOREIGN KEY ([ReservationId]) REFERENCES [dbo].[Reservation] ([Id])

/* CreateForeignKey FK_ReservationDetail_Ingredient ReservationDetail(IngredientId) Ingredient(Id) */
ALTER TABLE [dbo].[ReservationDetail] ADD CONSTRAINT [FK_ReservationDetail_Ingredient] FOREIGN KEY ([IngredientId]) REFERENCES [dbo].[Ingredient] ([Id])

/* CreateIndex ReservationDetail (ReservationId) */
CREATE INDEX [IX_Reservation] ON [dbo].[ReservationDetail] ([ReservationId] ASC)

/* CreateIndex ReservationDetail (IngredientId) */
CREATE INDEX [IX_Ingredient] ON [dbo].[ReservationDetail] ([IngredientId] ASC)

/* ExecuteSqlStatement INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Yucatan', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Tabasco', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Yucatan', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Tabasco', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO Company (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('EmBe', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Capsa', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO Company (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('EmBe', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Capsa', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO Branch (Name, Code, RegionId, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('CORPORATIVO 3', '95', 1, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('TIZIMIN', '11', 1, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('VILLAHERMOSA', '79', 2, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('PACABTUN VENTAS', '104', 1, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO Branch (Name, Code, RegionId, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('CORPORATIVO 3', '95', 1, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('TIZIMIN', '11', 1, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('VILLAHERMOSA', '79', 2, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('PACABTUN VENTAS', '104', 1, 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO Department (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('AD Administracion y Finanzas', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('CO Ventas', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('CO Administracion', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('CO Cuentas clave y CM ejecució', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO Department (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('AD Administracion y Finanzas', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('CO Ventas', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('CO Administracion', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('CO Cuentas clave y CM ejecució', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO [User] (Name, UserName, Password, RoleId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('admin', 'admin', 'JRxZJ9O9m6Y=', 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO [User] (Name, UserName, Password, RoleId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('admin', 'admin', 'JRxZJ9O9m6Y=', 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Enfermedad cardiaca', 'CODE1', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Diabetes', 'CODE2', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Enfermedad cardiaca', 'CODE1', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Diabetes', 'CODE2', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Te estas pasando de calorias', 'CODE1', 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Cuidado con tu alimentacion', 'CODE2', 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Te estas pasando de calorias', 'CODE1', 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Cuidado con tu alimentacion', 'CODE2', 1, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO Job (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('ADMINISTRACION PROYECTOS ADMON', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('VENDEDOR AGUA HOGAR', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('ENCARGADO DE CARTERA', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('GESTION DE AUTOSERVICIOS', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO Job (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('ADMINISTRACION PROYECTOS ADMON', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('VENDEDOR AGUA HOGAR', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('ENCARGADO DE CARTERA', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('GESTION DE AUTOSERVICIOS', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO Dealer (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Areca', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Cocina Walter', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO Dealer (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('Areca', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('Cocina Walter', 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

/* ExecuteSqlStatement INSERT INTO BranchDealer (BranchId, DealerId) VALUES (1,1), (1,2) */
INSERT INTO BranchDealer (BranchId, DealerId) VALUES (1,1), (1,2)

/* ExecuteSqlStatement INSERT INTO Worker (Code, FirstName, LastName, Email, Imss, Gender, Badge, DepartmentId, JobId, BranchId, RoleId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('10987', 'GABRIELA MAGDALENA', 'TUN CHALE', '', '84-06-86-1123-9', 2, '010000951098776', 1, 1, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('10996', 'CARLOS FRANCISCO', 'CHAN CHI', '', '84-10-91-2141-2', 1, '010000111099616', 2, 2, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1) */
INSERT INTO Worker (Code, FirstName, LastName, Email, Imss, Gender, Badge, DepartmentId, JobId, BranchId, RoleId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES ('10987', 'GABRIELA MAGDALENA', 'TUN CHALE', '', '84-06-86-1123-9', 2, '010000951098776', 1, 1, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1),('10996', 'CARLOS FRANCISCO', 'CHAN CHI', '', '84-10-91-2141-2', 1, '010000111099616', 2, 2, 1, 3, 1, 1, '12/31/2016 10:40:28', '12/31/2016 10:40:28', 1, 1)

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (2, '2016-12-31T16:40:28', '_2_Seed')
/* Committing Transaction */
/* 2: _2_Seed migrated */

/* 3: _3_AddLimitEnergyToWorker migrating ==================================== */

/* Beginning Transaction */
/* AlterTable Worker */
/* No SQL statement executed. */

/* CreateColumn Worker LimitEnergy Int32 */
ALTER TABLE [dbo].[Worker] ADD [LimitEnergy] INT

/* ExecuteSqlStatement Update Worker SET LimitEnergy = 2000 */
Update Worker SET LimitEnergy = 2000

/* AlterTable Worker */
/* No SQL statement executed. */

/* AlterColumn Worker LimitEnergy Int32 */
ALTER TABLE [dbo].[Worker] ALTER COLUMN [LimitEnergy] INT NOT NULL

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (3, '2016-12-31T16:40:28', '_3_AddLimitEnergyToWorker')
/* Committing Transaction */
/* 3: _3_AddLimitEnergyToWorker migrated */

/* Task completed. */
