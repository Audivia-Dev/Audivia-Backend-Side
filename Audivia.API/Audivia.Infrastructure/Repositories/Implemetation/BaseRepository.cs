using MongoDB.Driver;
using System.Linq.Expressions;
using Audivia.Infrastructure.Interface;
using MongoDB.Bson;

namespace Audivia.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseRepository(IMongoDatabase database)
        {
            var collectionName = typeof(T).Name;
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetById<TId>(TId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> Create(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task Update(T entity)
        {
            var id = GetEntityId(entity);
            if (id == null)
                throw new Exception("Entity must have an Id or _id property.");

            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id.ToString()));

            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task Delete(T entity)
        {
            var id = GetEntityId(entity);
            if (id == null)
                throw new Exception("Entity must have an Id or _id property.");

            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id.ToString()));

            await _collection.DeleteOneAsync(filter);
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).AnyAsync();
        }

        public async Task<T?> FindFirst(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            // Nothing to dispose in this case
        }

        private object? GetEntityId(T entity)
        {
            var idProp = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty("_id");
            return idProp?.GetValue(entity);
        }

        public async Task<IEnumerable<T>> Search(
            FilterDefinition<T>? filter = null,
            SortDefinition<T>? sortCondition = null,
            int? top = null,
            int? pageIndex = null,
            int? pageSize = null
        )
        {
            var finalFilter = filter ?? Builders<T>.Filter.Empty;
            var query = _collection.Find(finalFilter);

            if (sortCondition != null)
            {
                query = query.Sort(sortCondition);
            }

            if (top.HasValue)
            {
                // no pagination for top
                return await query.Limit(top.Value).ToListAsync();
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                int skip = (pageIndex.Value - 1) * pageSize.Value;
                return await query.Skip(skip).Limit(pageSize.Value).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> Search(
            FilterDefinition<T>? filter = null
        )
        {
            var finalFilter = filter ?? Builders<T>.Filter.Empty;
            var query = _collection.Find(finalFilter);
            return await query.ToListAsync();
        }

        public async Task<int> Count(FilterDefinition<T>? filter)
        {
            var finalFilter = filter ?? Builders<T>.Filter.Empty;
            var query = _collection.Find(finalFilter);

            return (int)await query.CountDocumentsAsync();
        }
    }
}
