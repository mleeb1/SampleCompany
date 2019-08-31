using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Company.Common.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _db;
        private readonly DbSet<T> _set;

        // IReadable
        //
        //public T FindById(long id)
        //{
        //    return FindAll().FirstOrDefault(t => t.Id == id);
        //}

        public Repository(IDbContext context)
        {
            _set = context.Set<T>();
            _db = context;
        }

        public IQueryable<T> FindAll()
        {
            return _set;
        }

        public T Add(T t)
        {
            _set.Add(t);
            _db.SaveChanges();
            return t;
        }

        public T Update(T t)
        {
            _db.SaveChanges();
            return t;
        }
    }
}
