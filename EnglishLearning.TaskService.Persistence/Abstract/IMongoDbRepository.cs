using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface IMongoDbRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> FinAllAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T item);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> filter);
        Task<bool> UpdateAsync(string id, T item);
        Task<bool> DeleteAll();
    }
}