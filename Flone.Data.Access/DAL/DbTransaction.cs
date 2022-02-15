using Microsoft.EntityFrameworkCore.Storage;

namespace Flone.Data.Repository.DAL
{
    public class DbTransaction : ITransaction
    {
        private readonly IDbContextTransaction _efTransaction;
        public DbTransaction(IDbContextTransaction efTransaction)
        {
            _efTransaction = efTransaction;
        }
        public void Commit()
        {
            _efTransaction.Commit();
        }

        public void Rollback()
        {
           _efTransaction.Rollback();
        }

        public void Dispose()
        {
            _efTransaction.Dispose();
        }
    }
}
