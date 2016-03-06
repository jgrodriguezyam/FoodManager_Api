using System;
using FluentMigrator;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates;

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

            //#region User

            //Create.Table("User").InSchema("dbo")
            //    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
            //    .WithColumn("Name").AsString(250).NotNullable()
            //    .WithColumn("Code").AsString(250).NotNullable()
            //    .WithColumn("Status").AsBoolean().NotNullable()
            //    .WithColumn("UserName").AsString(250).Unique().NotNullable()
            //    .WithColumn("Password").AsString(250).NotNullable()
            //    .WithColumn("PublicKey").AsString(250).Nullable()
            //    .WithColumn("Time").AsString(250).Nullable();

            //#endregion

            Init();
        }

        public override void Down()
        {
            #region Region

            Delete.Table("Region").InSchema("dbo");

            #endregion

            #region Company

            Delete.ForeignKey("FK_Company_Region").OnTable("Company").InSchema("dbo");
            Delete.Table("Company").InSchema("dbo");

            #endregion

            #region Branch

            Delete.ForeignKey("FK_Branch_Company").OnTable("Branch").InSchema("dbo");
            Delete.Table("Branch").InSchema("dbo");

            #endregion

            #region Department

            Delete.ForeignKey("FK_Department_Branch").OnTable("Department").InSchema("dbo");
            Delete.Table("Department").InSchema("dbo");

            #endregion

            //#region User

            //Delete.ForeignKey("FK_User_Branch").OnTable("User").InSchema("dbo");
            //Delete.Table("User").InSchema("dbo");

            //#endregion
        }

        private void Init()
        {
            var today = DateTime.Now.ToDateTimeStringDb();

            Execute.Sql("INSERT INTO Region (Name, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Yucatan', " + GlobalConstants.AdminRoleId + ", " + GlobalConstants.AdminRoleId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusDefault + ", " + GlobalConstants.IsActiveDefault + ")," +
                        "('Campeche', " + GlobalConstants.AdminRoleId + ", " + GlobalConstants.AdminRoleId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusDefault + ", " + GlobalConstants.IsActiveDefault + ")");

            Execute.Sql("INSERT INTO Company (Name, RegionId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Bepensa Industria', 1," + GlobalConstants.AdminRoleId + ", " + GlobalConstants.AdminRoleId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusDefault + ", " + GlobalConstants.IsActiveDefault + ")," +
                        "('Bepensa Bebidas', 1," + GlobalConstants.AdminRoleId + ", " + GlobalConstants.AdminRoleId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusDefault + ", " + GlobalConstants.IsActiveDefault + ")");

            Execute.Sql("INSERT INTO Branch (Name, Code, CompanyId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Opesystem', 'CODE1', 1," + GlobalConstants.AdminRoleId + ", " + GlobalConstants.AdminRoleId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusDefault + ", " + GlobalConstants.IsActiveDefault + ")," +
                        "('Finbe', 'CODE2', 1," + GlobalConstants.AdminRoleId + ", " + GlobalConstants.AdminRoleId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusDefault + ", " + GlobalConstants.IsActiveDefault + ")");

            Execute.Sql("INSERT INTO Department (Name, BranchId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "('Desarrollo', 1," + GlobalConstants.AdminRoleId + ", " + GlobalConstants.AdminRoleId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusDefault + ", " + GlobalConstants.IsActiveDefault + ")," +
                        "('HelpDesk', 1," + GlobalConstants.AdminRoleId + ", " + GlobalConstants.AdminRoleId + ", '" + today + "', '" + today + "', " + GlobalConstants.StatusDefault + ", " + GlobalConstants.IsActiveDefault + ")");
        }
    }
}