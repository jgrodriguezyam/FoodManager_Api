using System;
using FluentMigrator;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Roles;

namespace FoodManager.Migrations.Sprint_01
{
    [Migration(1)]
    public class _1_SeedRoles : Migration
    {
        private readonly string _today = DateTime.Now.ToDateTimeStringDb();

        public override void Up()
        {
            #region AccessLevel

            Create.Table("AccessLevel").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Description").AsString(250).NotNullable();

            InsertAccessLevels();

            #endregion

            #region Permission

            Create.Table("Permission").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                .WithColumn("Description").AsString(250).NotNullable();

            InsertPermission();

            #endregion

            #region PermissionAccessLevel

            Create.Table("PermissionAccessLevel").InSchema("dbo")
                .WithColumn("PermissionId").AsInt32().NotNullable()
                .WithColumn("AccessLevelId").AsInt32().NotNullable();

            Create.ForeignKey("FK_PermissionAccessLevel_Permission").FromTable("PermissionAccessLevel").InSchema("dbo").ForeignColumn("PermissionId")
                .ToTable("Permission").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_PermissionAccessLevel_AccessLevel").FromTable("PermissionAccessLevel").InSchema("dbo").ForeignColumn("AccessLevelId")
                .ToTable("AccessLevel").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_PermissionAccessLevel").OnTable("PermissionAccessLevel").InSchema("dbo")
                .OnColumn("PermissionId").Ascending().OnColumn("AccessLevelId").Ascending().WithOptions().Unique();

            InsertPermissionAccessLevel();

            #endregion

            #region Role

            Create.Table("Role").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Insert.IntoTable("Role").InSchema("dbo").Row(
                new
                {
                    Name = GlobalConstants.AdminRoleName,
                    CreatedBy = GlobalConstants.SystemUserId,
                    ModifiedBy = GlobalConstants.SystemUserId,
                    CreatedOn = _today,
                    ModifiedOn = _today,
                    Status = GlobalConstants.StatusActivatedMigration,
                    IsActive = GlobalConstants.ActivatedMigration
                });

            #endregion

            #region RoleConfiguration

            Create.Table("RoleConfiguration").InSchema("dbo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable()
                .WithColumn("PermissionId").AsInt32().NotNullable()
                .WithColumn("AccessLevelId").AsInt32().NotNullable()

                .WithColumn("CreatedBy").AsInt32().NotNullable()
                .WithColumn("ModifiedBy").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("ModifiedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean();

            Create.ForeignKey("FK_RoleConfiguration_Role").FromTable("RoleConfiguration").InSchema("dbo").ForeignColumn("RoleId")
                .ToTable("Role").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_RoleConfiguration_Permission").FromTable("RoleConfiguration").InSchema("dbo").ForeignColumn("PermissionId")
                .ToTable("Permission").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_RoleConfiguration_AccessLevel").FromTable("RoleConfiguration").InSchema("dbo").ForeignColumn("AccessLevelId")
                .ToTable("AccessLevel").InSchema("dbo").PrimaryColumn("Id");

            Create.Index("IX_RoleConfiguration").OnTable("RoleConfiguration").InSchema("dbo")
                .OnColumn("RoleId").Ascending().OnColumn("PermissionId").Ascending().OnColumn("AccessLevelId").Ascending().WithOptions().Unique();

            InsertRoleConfiguration();

            #endregion
            
        }

        public override void Down()
        {
            #region RoleConfiguration

            Delete.ForeignKey("FK_RoleConfiguration_Role").OnTable("RoleConfiguration").InSchema("dbo");
            Delete.ForeignKey("FK_RoleConfiguration_Permission").OnTable("RoleConfiguration").InSchema("dbo");
            Delete.ForeignKey("FK_RoleConfiguration_AccessLevel").OnTable("RoleConfiguration").InSchema("dbo");
            Delete.Table("RoleConfiguration").InSchema("dbo");

            #endregion

            #region PermissionAccessLevel

            Delete.ForeignKey("FK_PermissionAccessLevel_Permission").OnTable("PermissionAccessLevel").InSchema("dbo");
            Delete.ForeignKey("FK_PermissionAccessLevel_AccessLevel").OnTable("PermissionAccessLevel").InSchema("dbo");
            Delete.Table("PermissionAccessLevel").InSchema("dbo");

            #endregion

            #region AccessLevel

            Delete.Table("AccessLevel").InSchema("dbo");

            #endregion

            #region Permission

            Delete.Table("Permission").InSchema("dbo");

            #endregion

            #region Role

            Delete.Table("Role").InSchema("dbo");

            #endregion
        }

        #region Custom Methods

        private void InsertAccessLevels()
        {
            Execute.Sql("INSERT INTO AccessLevel (Id, Name, Description) VALUES " +
                        "(" + AccessLevelType.Post.GetValue() + ", '" + AccessLevelType.Post + "', 'Crear')," +
                        "(" + AccessLevelType.Put.GetValue() + ", '" + AccessLevelType.Put + "', 'Actualizar')," +
                        "(" + AccessLevelType.Get.GetValue() + ", '" + AccessLevelType.Get + "', 'Ver')," +
                        "(" + AccessLevelType.Delete.GetValue() + ", '" + AccessLevelType.Delete + "', 'Eliminar')");
        }

        private void InsertPermission()
        {
            Execute.Sql("INSERT INTO Permission (Id, Name, Description) VALUES " +
                        "(" + PermissionType.AccessLevel.GetValue() + ", '" + PermissionType.AccessLevel + "', 'Niveles de acceso')," +
                        "(" + PermissionType.Permission.GetValue() + ", '" + PermissionType.Permission + "', 'Permisos')," +
                        "(" + PermissionType.Role.GetValue() + ", '" + PermissionType.Role + "', 'Roles')," +
                        "(" + PermissionType.RoleConfiguration.GetValue() + ", '" + PermissionType.RoleConfiguration + "', 'Configuracion de roles')," +
                        "(" + PermissionType.User.GetValue() + ", '" + PermissionType.User + "', 'Usuarios')," +
                        "(" + PermissionType.Worker.GetValue() + ", '" + PermissionType.Worker + "', 'Trabajadores')," +
                        "(" + PermissionType.Region.GetValue() + ", '" + PermissionType.Region + "', 'Regiones')," +
                        "(" + PermissionType.Company.GetValue() + ", '" + PermissionType.Company + "', 'Companias')," +
                        "(" + PermissionType.Branch.GetValue() + ", '" + PermissionType.Branch + "', 'Sucursales')," +
                        "(" + PermissionType.Department.GetValue() + ", '" + PermissionType.Department + "', 'Departamentos')," +
                        "(" + PermissionType.Disease.GetValue() + ", '" + PermissionType.Disease + "', 'Enfermedades')," +
                        "(" + PermissionType.Warning.GetValue() + ", '" + PermissionType.Warning + "', 'Alertas')," +
                        "(" + PermissionType.Tip.GetValue() + ", '" + PermissionType.Tip + "', 'Consejos')," +
                        "(" + PermissionType.Job.GetValue() + ", '" + PermissionType.Job + "', 'Puestos')," +
                        "(" + PermissionType.Dealer.GetValue() + ", '" + PermissionType.Dealer + "', 'Distribuidores')," +
                        "(" + PermissionType.Menu.GetValue() + ", '" + PermissionType.Menu + "', 'Menus')," +
                        "(" + PermissionType.Saucer.GetValue() + ", '" + PermissionType.Saucer + "', 'Platillos')," +
                        "(" + PermissionType.SaucerMultimedia.GetValue() + ", '" + PermissionType.SaucerMultimedia + "', 'Multimedias')," +
                        "(" + PermissionType.SaucerConfiguration.GetValue() + ", '" + PermissionType.SaucerConfiguration + "', 'Configuracion de platillos')," +
                        "(" + PermissionType.Ingredient.GetValue() + ", '" + PermissionType.Ingredient + "', 'Ingredientes')," +
                        "(" + PermissionType.IngredientGroup.GetValue() + ", '" + PermissionType.IngredientGroup + "', 'Grupo de ingredientes')," +
                        "(" + PermissionType.Reservation.GetValue() + ", '" + PermissionType.Reservation + "', 'Reservaciones')");
        }

        private void InsertPermissionAccessLevel()
        {
            Execute.Sql("INSERT INTO PermissionAccessLevel (PermissionId, AccessLevelId) VALUES " +
                        "(" + PermissionType.AccessLevel.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +

                        "(" + PermissionType.Permission.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +

                        "(" + PermissionType.Role.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Role.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Role.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Role.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.RoleConfiguration.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.RoleConfiguration.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.RoleConfiguration.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.RoleConfiguration.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.User.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.User.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.User.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +

                        "(" + PermissionType.Worker.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Worker.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Worker.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +

                        "(" + PermissionType.Region.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Region.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Region.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Region.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Company.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Company.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Company.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Company.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Branch.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Branch.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Branch.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Branch.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Department.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Department.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Department.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Department.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Disease.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Disease.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Disease.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Disease.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Warning.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Warning.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Warning.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Warning.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Tip.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Tip.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Tip.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Tip.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Job.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Job.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Job.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Job.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Dealer.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Dealer.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Dealer.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Dealer.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Menu.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Menu.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Menu.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Menu.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Saucer.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Saucer.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Saucer.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Saucer.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.SaucerMultimedia.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.SaucerMultimedia.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.SaucerMultimedia.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.SaucerMultimedia.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.SaucerConfiguration.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.SaucerConfiguration.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.SaucerConfiguration.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.SaucerConfiguration.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Ingredient.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Ingredient.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Ingredient.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Ingredient.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.IngredientGroup.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.IngredientGroup.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.IngredientGroup.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.IngredientGroup.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")," +

                        "(" + PermissionType.Reservation.GetValue() + ", " + AccessLevelType.Post.GetValue() + ")," +
                        "(" + PermissionType.Reservation.GetValue() + ", " + AccessLevelType.Put.GetValue() + ")," +
                        "(" + PermissionType.Reservation.GetValue() + ", " + AccessLevelType.Get.GetValue() + ")," +
                        "(" + PermissionType.Reservation.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ")");
        }

        private void InsertRoleConfiguration()
        {
            Execute.Sql("INSERT INTO RoleConfiguration (RoleId, PermissionId, AccessLevelId, CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive) VALUES " +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.AccessLevel.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Permission.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Role.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Role.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Role.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Role.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.RoleConfiguration.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.RoleConfiguration.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.RoleConfiguration.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.RoleConfiguration.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.User.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.User.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.User.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Worker.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Worker.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Worker.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Region.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Region.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Region.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Region.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Company.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Company.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Company.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Company.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Branch.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Branch.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Branch.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Branch.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Department.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Department.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Department.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Department.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Disease.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Disease.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Disease.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Disease.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Warning.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Warning.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Warning.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Warning.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Tip.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Tip.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Tip.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Tip.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Job.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Job.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Job.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Job.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Dealer.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Dealer.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Dealer.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Dealer.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Menu.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Menu.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Menu.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Menu.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Saucer.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Saucer.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Saucer.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Saucer.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.SaucerMultimedia.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.SaucerMultimedia.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.SaucerMultimedia.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.SaucerMultimedia.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.SaucerConfiguration.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.SaucerConfiguration.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.SaucerConfiguration.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.SaucerConfiguration.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Ingredient.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Ingredient.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Ingredient.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Ingredient.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.IngredientGroup.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.IngredientGroup.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.IngredientGroup.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.IngredientGroup.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +

                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Reservation.GetValue() + ", " + AccessLevelType.Post.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Reservation.GetValue() + ", " + AccessLevelType.Put.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Reservation.GetValue() + ", " + AccessLevelType.Get.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")," +
                        "(" + GlobalConstants.AdminRoleId + ", " + PermissionType.Reservation.GetValue() + ", " + AccessLevelType.Delete.GetValue() + ", " + GlobalConstants.CreatedBySystemUser + ")");
        }

        #endregion
    }
}