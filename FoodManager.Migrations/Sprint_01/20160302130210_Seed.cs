using FluentMigrator;

namespace FoodManager.Migrations.Sprint_01
{
    [Migration(20160302130210)]
    public class _20160302130210_Seed : Migration
    {
        public override void Up()
        {
            #region Branch

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

            #region User

            Create.Table("User").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Code").AsString(250).NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("UserName").AsString(250).Unique().NotNullable()
                .WithColumn("Password").AsString(250).NotNullable()
                .WithColumn("PublicKey").AsString(250).Nullable()
                .WithColumn("Time").AsString(250).Nullable();

            #endregion
        }

        public override void Down()
        {
            #region RouteUnit

            #endregion

            #region User

            Delete.ForeignKey("FK_User_Branch").OnTable("User").InSchema("dbo");
            Delete.Table("User").InSchema("dbo");

            #endregion
        }
    }
}