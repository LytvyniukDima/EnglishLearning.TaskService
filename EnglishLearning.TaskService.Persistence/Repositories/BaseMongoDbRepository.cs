using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Persistence.Abstract;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    public abstract class BaseMongoDbRepository<T> : IMongoDbRepository<T> where T : IEntity
    {
        protected readonly ITaskDbContext _dbContext;
        protected readonly IMongoCollection<T> _collection;

        protected BaseMongoDbRepository(ITaskDbContext dbContext, string collectionName)
        {
            _dbContext = dbContext;
            _collection = dbContext.GetCollection<T>(collectionName);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public virtual async Task AddAsync(T item)
        {
            await _collection.InsertOneAsync(item);
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            DeleteResult actionResult = await _collection.DeleteManyAsync(filter);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public virtual async Task<bool> UpdateAsync(string id, T item)
        {
            var actionResult = await _collection.ReplaceOneAsync(x => x.Id == id, item);
            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }

        public virtual async Task<bool> DeleteAllAsync()
        {
            DeleteResult actionResult = await _collection.DeleteManyAsync(_ => true);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }
    }
}