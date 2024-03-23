using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repository.Interfaces
{
    public interface IGenericRepo<TEntity, TKey> 
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> predicate);        
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TKey id);
    }
}
