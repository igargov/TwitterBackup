namespace TwitterBackup.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        void SaveChangesAsync();
    }
}