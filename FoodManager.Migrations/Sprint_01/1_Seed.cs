﻿using System;
using FluentMigrator;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Utils;
using FoodManager.Model.Enums;

namespace FoodManager.Migrations.Sprint_01
{
    [Migration(1)]
    public class _1_Seed : Migration
    {
        public override void Up()
        {
            #region Region

            Create.Table("Region").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            #endregion

            #region Company

            Create.Table("Company").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("RegionId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Company_Region").FromTable("Company").InSchema("dbo").ForeignColumn("RegionId")
                 .ToTable("Region").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region Branch

            Create.Table("Branch").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Code").AsString(250).NotNullable()
                .WithColumn("CompanyId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Branch_Company").FromTable("Branch").InSchema("dbo").ForeignColumn("CompanyId")
                 .ToTable("Company").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region Department

            Create.Table("Department").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("BranchId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Department_Branch").FromTable("Department").InSchema("dbo").ForeignColumn("BranchId")
                 .ToTable("Branch").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region Disease

            Create.Table("Disease").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Code").AsString(250).NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            #endregion

            #region Warning

            Create.Table("Warning").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Code").AsString(250).NotNullable()
                .WithColumn("DiseaseId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Warning_Disease").FromTable("Warning").InSchema("dbo").ForeignColumn("DiseaseId")
                 .ToTable("Disease").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region Job

            Create.Table("Job").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            #endregion

            #region Tip

            Create.Table("Tip").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            #endregion

            #region Dealer

            Create.Table("Dealer").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            #endregion

            #region BranchDealer

            Create.Table("BranchDealer").InSchema("dbo")
                .WithColumn("BranchId").AsInt32().NotNullable()
                .WithColumn("DealerId").AsInt32().NotNullable();

            Create.ForeignKey("FK_BranchDealer_Branch").FromTable("BranchDealer").InSchema("dbo").ForeignColumn("BranchId")
                 .ToTable("Branch").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_BranchDealer_Dealer").FromTable("BranchDealer").InSchema("dbo").ForeignColumn("DealerId")
                 .ToTable("Dealer").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_BranchDealer").OnTable("BranchDealer").InSchema("dbo")
                .OnColumn("BranchId").Ascending().OnColumn("DealerId").Ascending().WithOptions().Unique();

            #endregion

            #region User

            Create.Table("User").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("UserName").AsString(250).Unique().NotNullable()
                .WithColumn("Password").AsString(250).NotNullable()
                .WithColumn("PublicKey").AsString(250).Nullable()
                .WithColumn("Time").AsString(250).Nullable()
                .WithColumn("DealerId").AsInt32().Nullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_User_Dealer").FromTable("User").InSchema("dbo").ForeignColumn("DealerId")
                 .ToTable("Dealer").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region Saucer

            Create.Table("Saucer").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Type").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            #endregion

            #region DealerSaucer

            Create.Table("DealerSaucer").InSchema("dbo")
                .WithColumn("DealerId").AsInt32().NotNullable()
                .WithColumn("SaucerId").AsInt32().NotNullable();

            Create.ForeignKey("FK_DealerSaucer_Dealer").FromTable("DealerSaucer").InSchema("dbo").ForeignColumn("DealerId")
                 .ToTable("Dealer").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_DealerSaucer_Saucer").FromTable("DealerSaucer").InSchema("dbo").ForeignColumn("SaucerId")
                 .ToTable("Saucer").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_DealerSaucer").OnTable("DealerSaucer").InSchema("dbo")
                .OnColumn("DealerId").Ascending().OnColumn("SaucerId").Ascending().WithOptions().Unique();

            #endregion

            #region SaucerMultimedia

            Create.Table("SaucerMultimedia").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Path").AsString(int.MaxValue).NotNullable()
                .WithColumn("SaucerId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_SaucerMultimedia_Saucer").FromTable("SaucerMultimedia").InSchema("dbo").ForeignColumn("SaucerId")
                 .ToTable("Saucer").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region IngredientGroup

            Create.Table("IngredientGroup").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Color").AsString(250).NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            #endregion

            #region Ingredient

            Create.Table("Ingredient").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Amount").AsDecimal().NotNullable()
                .WithColumn("Energy").AsDecimal().NotNullable()
                .WithColumn("Protein").AsDecimal().NotNullable()
                .WithColumn("Carbohydrate").AsDecimal().NotNullable()
                .WithColumn("Sugar").AsDecimal().NotNullable()
                .WithColumn("Lipid").AsDecimal().NotNullable()
                .WithColumn("Sodium").AsDecimal().NotNullable()
                .WithColumn("IngredientGroupId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Ingredient_IngredientGroup").FromTable("Ingredient").InSchema("dbo").ForeignColumn("IngredientGroupId")
                 .ToTable("IngredientGroup").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region SaucerConfiguration

            Create.Table("SaucerConfiguration").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("SaucerId").AsInt32().NotNullable()
                .WithColumn("IngredientId").AsInt32().NotNullable()
                .WithColumn("Amount").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_SaucerConfiguration_Saucer").FromTable("SaucerConfiguration").InSchema("dbo").ForeignColumn("SaucerId")
                 .ToTable("Saucer").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_SaucerConfiguration_Ingredient").FromTable("SaucerConfiguration").InSchema("dbo").ForeignColumn("IngredientId")
                 .ToTable("Ingredient").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region Worker

            Create.Table("Worker").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Code").AsString(250).NotNullable()
                .WithColumn("FirstName").AsString(250).NotNullable()
                .WithColumn("LastName").AsString(250).NotNullable()
                .WithColumn("Email").AsString(250).Unique().NotNullable()
                .WithColumn("Imss").AsString(250).Unique().NotNullable()
                .WithColumn("Gender").AsInt32().NotNullable()
                .WithColumn("Badge").AsString(250).Unique().NotNullable()
                .WithColumn("PublicKey").AsString(250).Nullable()
                .WithColumn("Time").AsString(250).Nullable()
                .WithColumn("DepartmentId").AsInt32().NotNullable()
                .WithColumn("JobId").AsInt32().NotNullable()
                .WithColumn("DealerId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Worker_Department").FromTable("Worker").InSchema("dbo").ForeignColumn("DepartmentId")
                 .ToTable("Department").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_Worker_Job").FromTable("Worker").InSchema("dbo").ForeignColumn("JobId")
                 .ToTable("Job").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_Worker_Dealer").FromTable("Worker").InSchema("dbo").ForeignColumn("DealerId")
                 .ToTable("Dealer").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region Menu

            Create.Table("Menu").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("DayWeek").AsInt32().NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("Limit").AsInt32().Nullable()
                .WithColumn("StartDate").AsDateTime().NotNullable()
                .WithColumn("EndDate").AsDateTime().NotNullable()
                .WithColumn("MaxAmount").AsInt32().Nullable()
                .WithColumn("DealerId").AsInt32().NotNullable()
                .WithColumn("SaucerId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Menu_Dealer").FromTable("Menu").InSchema("dbo").ForeignColumn("DealerId")
                 .ToTable("Dealer").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_Menu_Saucer").FromTable("Menu").InSchema("dbo").ForeignColumn("SaucerId")
                 .ToTable("Saucer").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            #region Reservation

            Create.Table("Reservation").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Energy").AsDecimal().NotNullable()
                .WithColumn("Protein").AsDecimal().NotNullable()
                .WithColumn("Carbohydrate").AsDecimal().NotNullable()
                .WithColumn("Sugar").AsDecimal().NotNullable()
                .WithColumn("Lipid").AsDecimal().NotNullable()
                .WithColumn("Sodium").AsDecimal().NotNullable()
                .WithColumn("WorkerId").AsInt32().NotNullable()
                .WithColumn("SaucerId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Reservation_Worker").FromTable("Reservation").InSchema("dbo").ForeignColumn("WorkerId")
                 .ToTable("Worker").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_Reservation_Saucer").FromTable("Reservation").InSchema("dbo").ForeignColumn("SaucerId")
                 .ToTable("Saucer").InSchema("dbo").PrimaryColumn("Id");

            #endregion

            Init();
        }

        public override void Down()
        {
            #region BranchDealer

            Delete.ForeignKey("FK_BranchDealer_Branch").OnTable("BranchDealer").InSchema("dbo");
            Delete.ForeignKey("FK_BranchDealer_Dealer").OnTable("BranchDealer").InSchema("dbo");
            Delete.Table("BranchDealer").InSchema("dbo");

            #endregion

            #region Reservation

            Delete.ForeignKey("FK_Reservation_Worker").OnTable("Reservation").InSchema("dbo");
            Delete.ForeignKey("FK_Reservation_Saucer").OnTable("Reservation").InSchema("dbo");
            Delete.Table("Reservation").InSchema("dbo");

            #endregion

            #region Worker

            Delete.ForeignKey("FK_Worker_Department").OnTable("Worker").InSchema("dbo");
            Delete.ForeignKey("FK_Worker_Job").OnTable("Worker").InSchema("dbo");
            Delete.ForeignKey("FK_Worker_Dealer").OnTable("Worker").InSchema("dbo");
            Delete.Table("Worker").InSchema("dbo");

            #endregion

            #region Department

            Delete.ForeignKey("FK_Department_Branch").OnTable("Department").InSchema("dbo");
            Delete.Table("Department").InSchema("dbo");

            #endregion

            #region Branch

            Delete.ForeignKey("FK_Branch_Company").OnTable("Branch").InSchema("dbo");
            Delete.Table("Branch").InSchema("dbo");

            #endregion

            #region Company

            Delete.ForeignKey("FK_Company_Region").OnTable("Company").InSchema("dbo");
            Delete.Table("Company").InSchema("dbo");

            #endregion

            #region Region

            Delete.Table("Region").InSchema("dbo");

            #endregion

            #region Warning

            Delete.ForeignKey("FK_Warning_Disease").OnTable("Warning").InSchema("dbo");
            Delete.Table("Warning").InSchema("dbo");

            #endregion

            #region Disease

            Delete.Table("Disease").InSchema("dbo");

            #endregion

            #region Job

            Delete.Table("Job").InSchema("dbo");

            #endregion

            #region User

            Delete.ForeignKey("FK_User_Dealer").OnTable("User").InSchema("dbo");
            Delete.Table("User").InSchema("dbo");

            #endregion

            #region Tip

            Delete.Table("Tip").InSchema("dbo");

            #endregion

            #region Menu

            Delete.ForeignKey("FK_Menu_Dealer").OnTable("Menu").InSchema("dbo");
            Delete.ForeignKey("FK_Menu_Saucer").OnTable("Menu").InSchema("dbo");
            Delete.Table("Menu").InSchema("dbo");

            #endregion

            #region DealerSaucer

            Delete.ForeignKey("FK_DealerSaucer_Dealer").OnTable("DealerSaucer").InSchema("dbo");
            Delete.ForeignKey("FK_DealerSaucer_Saucer").OnTable("DealerSaucer").InSchema("dbo");
            Delete.Table("DealerSaucer").InSchema("dbo");

            #endregion

            #region Dealer

            Delete.Table("Dealer").InSchema("dbo");

            #endregion

            #region SaucerMultimedia

            Delete.ForeignKey("FK_SaucerMultimedia_Saucer").OnTable("SaucerMultimedia").InSchema("dbo");
            Delete.Table("SaucerMultimedia").InSchema("dbo");

            #endregion

            #region Saucer

            Delete.Table("Saucer").InSchema("dbo");

            #endregion

            #region IngredientGroup

            Delete.Table("IngredientGroup").InSchema("dbo");

            #endregion

            #region Ingredient

            Delete.ForeignKey("FK_Ingredient_IngredientGroup").OnTable("Ingredient").InSchema("dbo");
            Delete.Table("Ingredient").InSchema("dbo");

            #endregion

            #region SaucerConfiguration

            Delete.ForeignKey("FK_SaucerConfiguration_Saucer").OnTable("SaucerConfiguration").InSchema("dbo");
            Delete.ForeignKey("FK_SaucerConfiguration_Ingredient").OnTable("SaucerConfiguration").InSchema("dbo");
            Delete.Table("SaucerConfiguration").InSchema("dbo");

            #endregion
        }

        private void Init()
        {
            var today = DateTime.Now.ToDateTimeStringDb();

            Execute.Sql("INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Yucatan', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Campeche', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Company (Name, RegionId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Bepensa Industria', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Bepensa Bebidas', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Branch (Name, Code, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Opesystem', 'CODE1', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Finbe', 'CODE2', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Department (Name, BranchId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Desarrollo', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('HelpDesk', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO [User] (Name, UserName, Password, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('" + GlobalConstants.AdminUserName + "', '" + GlobalConstants.AdminUserName + "', '" + Cryptography.Encrypt(GlobalConstants.AdminPassword) + "'," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.ActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Enfermedad cardiaca', 'CODE1'," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Diabetes', 'CODE2'," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Te estas pasando de calorias', 'CODE1', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Cuidado con tu alimentacion', 'CODE2', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Job (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Secretaria', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Programador', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Tip (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Nunca olvides que el desayuno es primordial', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Evita catalogar los alimentos', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Dealer (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Areca', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Cocina Walter', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO BranchDealer (BranchId, DealerId) VALUES (1,1), (1,2)");

            Execute.Sql("INSERT INTO Saucer (Name, Type, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Frijol con puerco', " + SaucerType.Main.GetValue() + ", " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Pechuga asada', " + SaucerType.Main.GetValue() + ", " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO DealerSaucer (DealerId, SaucerId) VALUES (1,1), (1,2)");

            Execute.Sql("INSERT INTO SaucerMultimedia (Path, SaucerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Frijol1.jpg', 1, " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Frijol2.jpg', 1, " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO IngredientGroup (Name, Color, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Carnes y Pescado', 'Rojo', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Verduras y Frutas', 'Verde', " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Ingredient (Name, Amount, Energy, Protein, Carbohydrate, Sugar, Lipid, Sodium, IngredientGroupId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Frijol', 100, 10, 10, 10, 10, 10, 10, 1, " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Puerco', 100, 10, 10, 10, 10, 10, 10, 1, " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO SaucerConfiguration (SaucerId, IngredientId, Amount, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "(1, 1, 3, " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "(1, 2, 3, " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Worker (Code, FirstName, LastName, Email, Imss, Gender, Badge, DepartmentId, JobId, DealerId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('1122', 'Juan', 'Martinez', 'juan@gmail.com', 'WV12H78', 1, '010107002113774', 1, 1, 1, " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('3344', 'Luis', 'Hernandez', 'luis@gmail.com', 'kV34H23', 1, '010107002112355', 1, 1, 1, " + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");
        }
    }
}