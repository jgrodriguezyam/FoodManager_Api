using System;
using FluentMigrator;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Utils;
using FoodManager.Model.Enums;

namespace FoodManager.Migrations.Sprint_01
{
    [Migration(20160302130210)]
    public class _20160302130210_Seed : Migration
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
                .WithColumn("Code").AsString(250).Unique().NotNullable()
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
                .WithColumn("Code").AsString(250).Unique().NotNullable()

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
                .WithColumn("Code").AsString(250).Unique().NotNullable()
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

            #region User

            Create.Table("User").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("UserName").AsString(250).Unique().NotNullable()
                .WithColumn("Password").AsString(250).NotNullable()
                .WithColumn("PublicKey").AsString(250).Nullable()
                .WithColumn("Time").AsString(250).Nullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            #endregion

            Init();
        }

        public override void Down()
        {
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

            #region User

            Delete.Table("User").InSchema("dbo");

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

            Execute.Sql("INSERT INTO [User] (Name, Type, UserName, Password, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('" + GlobalConstants.AdminUserName + "', " + UserType.Admin.GetValue()  + ", '" + GlobalConstants.AdminUserName + "', '" + Cryptography.Encrypt(GlobalConstants.AdminPassword) + "'," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.ActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Disease (Name, Code, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Enfermedad cardiaca', 'CODE1'," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Diabetes', 'CODE2'," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");

            Execute.Sql("INSERT INTO Warning (Name, Code, DiseaseId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Te estas pasando de calorias', 'CODE1', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")," +
                        "('Cuidado con tu alimentacion', 'CODE2', 1," + GlobalConstants.SystemUserId + ", " + GlobalConstants.SystemUserId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusActivatedMigration + ", " + GlobalConstants.ActivatedMigration + ")");
        }
    }
}