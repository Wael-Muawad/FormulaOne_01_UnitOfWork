using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repository.Implementations
{
    public class GenericRepo<TEntity, TKey> : IGenericRepo<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {

        private readonly AppDbContext _context;
        internal readonly DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            //_logger = logger;
        }

        public async Task<bool> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(TKey id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<List<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return true;
        }
    }
}
