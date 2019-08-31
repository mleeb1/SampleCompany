using Microsoft.EntityFrameworkCore;

namespace Company.Common
{
    public interface IDbContext
    {
        int SaveChanges();

        DbSet<T> Set<T>() where T : class;
    }
}
