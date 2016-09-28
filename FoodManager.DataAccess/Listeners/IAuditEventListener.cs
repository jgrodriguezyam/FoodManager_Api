namespace FoodManager.DataAccess.Listeners
{
    public interface IAuditEventListener
    {
        void OnPreInsert(object entity);
        void OnPreUpdate(object entity);
        void OnPreDelete(object entity);

        void OnPreInsertForSystem(object entity);
        void OnPreUpdateForSystem(object entity);
    }
}