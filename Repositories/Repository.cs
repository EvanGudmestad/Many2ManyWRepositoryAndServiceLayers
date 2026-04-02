using BookAuthors.Data;
using BookAuthors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookAuthors.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options);
        Task AddAsync(T entity);

        Task<T?> GetByIdAsync(int id);
        Task UpdateAsync(T entity);


    }


    /// <summary>
    /// Generic repository that handles CRUD operations for any entity.
    /// 
    /// WHY USE THIS?
    /// • One place for all database code (easier to maintain)
    /// • Pages don't talk to the database directly (easier to test)
    /// • Add logging or caching here once, works everywhere
    /// 
    /// WHEN TO SKIP THIS?
    /// • Small apps where DbContext is simpler
    /// </summary>

    public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(QueryOptions<T> options)
        {
            return await _dbSet.ApplyOptions(options).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
           return await _dbSet.FindAsync(id);
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
           _dbSet.Update(entity);
            await SaveAsync();
        }
    }
}
