using FluentMigrator;

namespace FoodManager.Migrations.Sprint_01
{
    [Migration(3)]
    public class _3_AddLimitEnergyToWorker : Migration
    {
        public override void Up()
        {
            Alter.Table("Worker").AddColumn("LimitEnergy").AsInt32().Nullable();
            Execute.Sql("Update Worker SET LimitEnergy = 2000");
            Alter.Table("Worker").AlterColumn("LimitEnergy").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("LimitEnergy").FromTable("Worker").InSchema("dbo");
        }
    }
}