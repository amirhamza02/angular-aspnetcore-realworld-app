using System.Data;
using Microsoft.EntityFrameworkCore;
namespace Flone.Data.Repository.DAL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DbContext _context; 
        public EFUnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Snapshot)
        {
            return new DbTransaction(_context.Database.BeginTransaction(isolationLevel));
        }
        public void Add<T>(T obj) where T : class
        {
           var set = _context.Set<T>();
            set.Add(obj);
        }

        public void Attach<T>(T newUser) where T : class
        {
            var set = _context.Set<T>();
            set.Attach(newUser);
        }
        public void Delete<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Remove(obj);
        }
        public IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Update<T>(T obj) where T : class
        {
            var set = _context.Set<T>();
            set.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
          await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context = null;
        }

        public IQueryable<T> Query<T>()
          where T : class
        {
            return _context.Set<T>();
        }
    }
}
