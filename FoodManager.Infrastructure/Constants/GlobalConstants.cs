using System;
using FoodManager.Infrastructure.Dates;

namespace FoodManager.Infrastructure.Constants
{
    public static class GlobalConstants
    {
        public static readonly int AdminRoleId = 1;
        public static readonly string AdminRoleName = "Administrador";

        public static readonly int DealerRoleId = 2;
        public static readonly string DealerRoleName = "Concesionario";

        public static readonly int WorkerRoleId = 3;
        public static readonly string WorkerRoleName = "Empleado";

        public static readonly int SystemUserId = 1;

        public static readonly string AdminUserName = "admin";
        public static readonly string AdminPassword = "admin";

        public static readonly string PublicKey = "PublicKey";
        public static readonly string PrivateKey = "PrivateKey";
        public static readonly string Timespan = "Timespan";
        public static readonly string LoginType = "LoginType";

        public static readonly string CryptographyKey = "FoodManagerKey";
        public static readonly string CompanyKey = "BEPENSA";

        public static readonly int ActivatedMigration = 1;
        public static readonly int StatusActivatedMigration = 1;

        public static readonly bool Activated = true;
        public static readonly bool Deactivated = false;

        public static readonly bool StatusActivated = true;
        public static readonly bool StatusDeactivated = false;

        public static readonly string CreatedBySystemUser =  SystemUserId + ", " + SystemUserId + ", '" + DateTime.Now.ToDateTimeStringDb() + "', '" + DateTime.Now.ToDateTimeStringDb() + "', " + StatusActivatedMigration + ", " + ActivatedMigration;
        public static readonly string AuditFields = "CreatedBy, ModifiedBy, CreatedOn, ModifiedOn, Status, IsActive";
    }
}
