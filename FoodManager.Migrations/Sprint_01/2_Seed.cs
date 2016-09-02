using FluentMigrator;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Utils;

namespace FoodManager.Migrations.Sprint_01
{
    [Migration(2)]
    public class _2_Seed : Migration
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

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            #endregion

            #region Branch

            Create.Table("Branch").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Code").AsString(250).NotNullable()
                .WithColumn("RegionId").AsInt32().NotNullable()
                .WithColumn("CompanyId").AsInt32().NotNullable()
                
                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Branch_Region").FromTable("Branch").InSchema("dbo").ForeignColumn("RegionId")
                 .ToTable("Region").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_Branch_Company").FromTable("Branch").InSchema("dbo").ForeignColumn("CompanyId")
                 .ToTable("Company").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_Region").OnTable("Branch").InSchema("dbo").OnColumn("RegionId").Ascending();
            Create.Index("IX_Company").OnTable("Branch").InSchema("dbo").OnColumn("CompanyId").Ascending();

            #endregion

            #region Department

            Create.Table("Department").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                
                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

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

            Create.Index("IX_Disease").OnTable("Warning").InSchema("dbo").OnColumn("DiseaseId").Ascending();

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
                .WithColumn("RoleId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_User_Dealer").FromTable("User").InSchema("dbo").ForeignColumn("DealerId")
                 .ToTable("Dealer").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_User_Role").FromTable("User").InSchema("dbo").ForeignColumn("RoleId")
                .ToTable("Role").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_Dealer").OnTable("User").InSchema("dbo").OnColumn("DealerId").Ascending();
            Create.Index("IX_Role").OnTable("User").InSchema("dbo").OnColumn("RoleId").Ascending();

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

            Create.Index("IX_Saucer").OnTable("SaucerMultimedia").InSchema("dbo").OnColumn("SaucerId").Ascending();

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
                .WithColumn("Energy").AsDecimal(10,2).NotNullable()
                .WithColumn("Protein").AsDecimal(10,2).NotNullable()
                .WithColumn("Carbohydrate").AsDecimal(10,2).NotNullable()
                .WithColumn("Sugar").AsDecimal(10,2).NotNullable()
                .WithColumn("Lipid").AsDecimal(10,2).NotNullable()
                .WithColumn("Sodium").AsDecimal(10,2).NotNullable()
                .WithColumn("NetWeight").AsDecimal(10,2).NotNullable()
                .WithColumn("Unit").AsInt32().NotNullable()
                .WithColumn("IngredientGroupId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_Ingredient_IngredientGroup").FromTable("Ingredient").InSchema("dbo").ForeignColumn("IngredientGroupId")
                 .ToTable("IngredientGroup").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_IngredientGroup").OnTable("Ingredient").InSchema("dbo").OnColumn("IngredientGroupId").Ascending();

            #endregion

            #region SaucerConfiguration

            Create.Table("SaucerConfiguration").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("SaucerId").AsInt32().NotNullable()
                .WithColumn("IngredientId").AsInt32().NotNullable()
                .WithColumn("NetWeight").AsDecimal(10,2).NotNullable()

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

            Create.Index("IX_Saucer").OnTable("SaucerConfiguration").InSchema("dbo").OnColumn("SaucerId").Ascending();
            Create.Index("IX_Ingredient").OnTable("SaucerConfiguration").InSchema("dbo").OnColumn("IngredientId").Ascending();

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
                .WithColumn("RoleId").AsInt32().NotNullable()
                .WithColumn("BranchId").AsInt32().NotNullable()

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
            Create.ForeignKey("FK_Worker_Role").FromTable("Worker").InSchema("dbo").ForeignColumn("RoleId")
                .ToTable("Role").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_Worker_Branch").FromTable("Worker").InSchema("dbo").ForeignColumn("BranchId")
                .ToTable("Branch").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_Department").OnTable("Worker").InSchema("dbo").OnColumn("DepartmentId").Ascending();
            Create.Index("IX_Job").OnTable("Worker").InSchema("dbo").OnColumn("JobId").Ascending();
            Create.Index("IX_Role").OnTable("Worker").InSchema("dbo").OnColumn("RoleId").Ascending();
            Create.Index("IX_Branch").OnTable("Worker").InSchema("dbo").OnColumn("BranchId").Ascending();

            #endregion

            #region Menu

            Create.Table("Menu").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Comment").AsString(250).Nullable()
                .WithColumn("DayWeek").AsInt32().NotNullable()
                .WithColumn("MealType").AsInt32().NotNullable()
                .WithColumn("StartDate").AsDateTime().NotNullable()
                .WithColumn("EndDate").AsDateTime().NotNullable()
                .WithColumn("MaxAmount").AsInt32().NotNullable()
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

            Create.Index("IX_Dealer").OnTable("Menu").InSchema("dbo").OnColumn("DealerId").Ascending();
            Create.Index("IX_Saucer").OnTable("Menu").InSchema("dbo").OnColumn("SaucerId").Ascending();

            #endregion

            #region Reservation

            Create.Table("Reservation").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Portion").AsDecimal(10,2).NotNullable()
                .WithColumn("MealType").AsInt32().NotNullable()
                .WithColumn("WorkerId").AsInt32().NotNullable()
                .WithColumn("SaucerId").AsInt32().NotNullable()
                .WithColumn("DealerId").AsInt32().Nullable()
                .WithColumn("IsPaid").AsBoolean().NotNullable()

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
            Create.ForeignKey("FK_Reservation_Dealer").FromTable("Reservation").InSchema("dbo").ForeignColumn("DealerId")
                 .ToTable("Dealer").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_Worker").OnTable("Reservation").InSchema("dbo").OnColumn("WorkerId").Ascending();
            Create.Index("IX_Saucer").OnTable("Reservation").InSchema("dbo").OnColumn("SaucerId").Ascending();
            Create.Index("IX_Dealer").OnTable("Reservation").InSchema("dbo").OnColumn("DealerId").Ascending();

            #endregion

            #region ReservationDetail

            Create.Table("ReservationDetail").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("ReservationId").AsInt32().NotNullable()
                .WithColumn("IngredientId").AsInt32().NotNullable()
                .WithColumn("NetWeight").AsDecimal(10,2).NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_ReservationDetail_Reservation").FromTable("ReservationDetail").InSchema("dbo").ForeignColumn("ReservationId")
                 .ToTable("Reservation").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_ReservationDetail_Ingredient").FromTable("ReservationDetail").InSchema("dbo").ForeignColumn("IngredientId")
                 .ToTable("Ingredient").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_Reservation").OnTable("ReservationDetail").InSchema("dbo").OnColumn("ReservationId").Ascending();
            Create.Index("IX_Ingredient").OnTable("ReservationDetail").InSchema("dbo").OnColumn("IngredientId").Ascending();

            #endregion

            Init();
        }

        public override void Down()
        {
            #region ReservationDetail
            
            Delete.ForeignKey("FK_ReservationDetail_Reservation").OnTable("ReservationDetail").InSchema("dbo");
            Delete.ForeignKey("FK_ReservationDetail_Ingredient").OnTable("ReservationDetail").InSchema("dbo");
            Delete.Table("ReservationDetail").InSchema("dbo");

            #endregion

            #region BranchDealer

            Delete.ForeignKey("FK_BranchDealer_Branch").OnTable("BranchDealer").InSchema("dbo");
            Delete.ForeignKey("FK_BranchDealer_Dealer").OnTable("BranchDealer").InSchema("dbo");
            Delete.Table("BranchDealer").InSchema("dbo");

            #endregion

            #region Reservation

            Delete.ForeignKey("FK_Reservation_Worker").OnTable("Reservation").InSchema("dbo");
            Delete.ForeignKey("FK_Reservation_Saucer").OnTable("Reservation").InSchema("dbo");
            Delete.ForeignKey("FK_Reservation_Dealer").OnTable("Reservation").InSchema("dbo");
            Delete.Table("Reservation").InSchema("dbo");

            #endregion

            #region Worker

            Delete.ForeignKey("FK_Worker_Department").OnTable("Worker").InSchema("dbo");
            Delete.ForeignKey("FK_Worker_Job").OnTable("Worker").InSchema("dbo");
            Delete.ForeignKey("FK_Worker_Role").OnTable("Worker").InSchema("dbo");
            Delete.ForeignKey("FK_Worker_Branch").OnTable("Worker").InSchema("dbo");
            Delete.Table("Worker").InSchema("dbo");

            #endregion

            #region Department

            Delete.Table("Department").InSchema("dbo");

            #endregion

            #region Branch

            Delete.ForeignKey("FK_Branch_Region").OnTable("Branch").InSchema("dbo");
            Delete.ForeignKey("FK_Branch_Company").OnTable("Branch").InSchema("dbo");
            Delete.Table("Branch").InSchema("dbo");

            #endregion

            #region Company
            
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
            Delete.ForeignKey("FK_User_Role").OnTable("User").InSchema("dbo");
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
            Execute.Sql("INSERT INTO Region (Name, "+ GlobalConstants.AuditFields + ") VALUES " +
                        "('Yucatan', " + GlobalConstants.CreatedBySystemUser + ")," +
                        "('Campeche', " + GlobalConstants.CreatedBySystemUser + ")");

            Execute.Sql("INSERT INTO Company (Name, " + GlobalConstants.AuditFields + ") VALUES " +
                        "('Bepensa Industria', " + GlobalConstants.CreatedBySystemUser + ")," +
                        "('Bepensa Bebidas', " + GlobalConstants.CreatedBySystemUser + ")");

            Execute.Sql("INSERT INTO Branch (Name, Code, RegionId, CompanyId, " + GlobalConstants.AuditFields + ") VALUES " +
                        "('Opesystem', 'CODE1', 1, 1, " + GlobalConstants.CreatedBySystemUser + ")," +
                        "('Finbe', 'CODE2', 1, 1, " + GlobalConstants.CreatedBySystemUser + ")");

            Execute.Sql("INSERT INTO Department (Name, " + GlobalConstants.AuditFields + ") VALUES " +
                        "('Desarrollo', " + GlobalConstants.CreatedBySystemUser + ")," +
                        "('HelpDesk', " + GlobalConstants.CreatedBySystemUser + ")");

            Execute.Sql("INSERT INTO [User] (Name, UserName, Password, RoleId, " + GlobalConstants.AuditFields + ") VALUES " +
                        "('" + GlobalConstants.AdminUserName + "', '" + GlobalConstants.AdminUserName + "', '" + Cryptography.Encrypt(GlobalConstants.AdminPassword) + "', "+ GlobalConstants.AdminRoleId + ", " + GlobalConstants.CreatedBySystemUser + ")");

            Execute.Sql("INSERT INTO Disease (Name, Code, " + GlobalConstants.AuditFields + ") VALUES " +
                        "('Enfermedad cardiaca', 'CODE1', " + GlobalConstants.CreatedBySystemUser + ")," +
                        "('Diabetes', 'CODE2', " + GlobalConstants.CreatedBySystemUser + ")");

            Execute.Sql("INSERT INTO Warning (Name, Code, DiseaseId, " + GlobalConstants.AuditFields + ") VALUES " +
                        "('Te estas pasando de calorias', 'CODE1', 1, " + GlobalConstants.CreatedBySystemUser + ")," +
                        "('Cuidado con tu alimentacion', 'CODE2', 1, " + GlobalConstants.CreatedBySystemUser + ")");

            Execute.Sql("INSERT INTO Job (Name, " + GlobalConstants.AuditFields + ") VALUES " +
                        "('Secretaria', " + GlobalConstants.CreatedBySystemUser + ")," +
                        "('Programador', " + GlobalConstants.CreatedBySystemUser + ")");

            Execute.Sql("INSERT INTO Dealer (Name, " + GlobalConstants.AuditFields + ") VALUES " +
                        "('Areca', " + GlobalConstants.CreatedBySystemUser + ")," +
                        "('Cocina Walter', " + GlobalConstants.CreatedBySystemUser + ")");

            Execute.Sql("INSERT INTO BranchDealer (BranchId, DealerId) VALUES (1,1), (1,2)");

            Execute.Sql("INSERT INTO Worker (Code, FirstName, LastName, Email, Imss, Gender, Badge, DepartmentId, JobId, BranchId, RoleId, " + GlobalConstants.AuditFields + ") VALUES " +
                        "('1122', 'Juan', 'Martinez', 'juan@gmail.com', 'WV12H78', 1, '010107002113774', 1, 1, 1, " + GlobalConstants.WorkerRoleId + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "('3344', 'Luis', 'Hernandez', 'luis@gmail.com', 'kV34H23', 1, '010107002112355', 1, 1, 1, " + GlobalConstants.WorkerRoleId + ", " + GlobalConstants.CreatedBySystemUser + ")");
        }
    }
}