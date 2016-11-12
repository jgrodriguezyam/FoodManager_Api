/* Using Database sqlserver2008 and Connection String Server=link_jorge_HP\localhost; Database=FoodManager; User Id=sa; Password=********; */
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

INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (3, '2016-11-12T05:44:48', '_3_AddLimitEnergyToWorker')
/* Committing Transaction */
/* 3: _3_AddLimitEnergyToWorker migrated */

/* Task completed. */
