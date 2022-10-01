using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Infrastructure.Persistence;

namespace Infrastructure.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly AppDbContext _dbContext;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression) => _dbContext.Set<T>().Where(expression);

        public T? Get(int id) => _dbContext.Set<T>().Find(id);

        public IQueryable<T> GetAll() => _dbContext.Set<T>();

        public async Task<T> Insert(T entity, bool SaveChange)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            if (SaveChange) await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T[]> InsertRange(T[] entities, bool SaveChange)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            if (SaveChange) await _dbContext.SaveChangesAsync();
            return entities;
        }

        public void Remove(T entity) => _dbContext.Set<T>().Remove(entity);

        public void RemoveRange(T[] entities) => _dbContext.Set<T>().RemoveRange(entities);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);

        public void UpdateRange(T[] entities) => _dbContext.Set<T>().UpdateRange(entities);

        public IQueryable<T>? Get(params int[] idValues)
        {
            var type = typeof(T);
            // x
            var parameter = Expression.Parameter(type);
            // x.[Property] : get key (ID) of entity
            var proprertyInfor = type.GetProperties().Where(p => p.GetCustomAttribute<KeyAttribute>() != null).SingleOrDefault();
            if (proprertyInfor == null) return null;

            var memberExpression = Expression.Property(parameter, proprertyInfor); // parameter.propertyInfor => x.ClassId...
            var expressions = idValues.ToList().Select(
                ID => Expression.Equal(memberExpression, Expression.Constant(ID, typeof(int)))
             );
            var body = expressions.Aggregate((pre, next) => Expression.Or(pre, next));
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            return _dbContext.Set<T>().Where(lambda);
        }

        public bool IsExistes(int id) => Get(id) != null;
    }
}
