using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BelajarRestApi.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public TEntity Attach(TEntity entity)
        {
            var entry = _context.Set<TEntity>().Attach(entity);
            return entry.Entity;
        }

        public async Task<int> Count()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> criteria)
        {
            return await _context.Set<TEntity>().CountAsync(criteria);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void DeleteAll(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<TEntity?> Find(Expression<Func<TEntity, bool>> criteria)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(criteria);
        }
        public async Task<TEntity?> Find(Expression<Func<TEntity, bool>> criteria, string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<TEntity>> FindAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAll(int page, int size)
        {
            return await _context.Set<TEntity>().Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAll(string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria)
        {
            return await _context.Set<TEntity>().Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(criteria).ToListAsync();

        }

        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, int page, int size)
        {
            return await _context.Set<TEntity>().Where(criteria).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, int page, int size, string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach(var include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(criteria).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, int? page, int? size, string[]? includes, Expression<Func<TEntity, object>> orderBy, string direction)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if(includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if(page.HasValue && size.HasValue)
            {
                query = query.Skip((page.Value - 1) * size.Value).Take(size.Value);
            }
            if(orderBy != null)
            {
                query = direction is "ASC" or "asc" ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }
            return await query.Where(criteria).ToListAsync();
        }

        public async Task<TEntity?> FindById(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public Task<TEntity?> FindById(Guid id, string[] includes)
        {
            return null;
        }

        public async Task<TEntity> Save(TEntity entity)
        {
            var entry = await _context.Set<TEntity>().AddAsync(entity);
            return entry.Entity;
        }

        public async Task<IEnumerable<TEntity>> SaveAll(IEnumerable<TEntity> entities)
        {
            var listEntities = entities.ToList();
            await _context.Set<TEntity>().AddRangeAsync(listEntities);
            return listEntities;

        }

        public TEntity Update(TEntity entity)
        {
            var attach = Attach(entity);
            _context.Set<TEntity>().Update(attach);
            return attach;
        }
    }
}
