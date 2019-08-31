using System.Linq;

namespace Company.Common.Repository
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> FindAll();
        T Add(T t);
        T Update(T t);
    }
}
