using Domain.Entities;
using Domain.Interfaces.Repository;
using MongoDB.Driver;
using WorkingGood.Log;

namespace Infrastructure.Persistance.Repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly IWgLog<Repository<T>> _logger; 
    private readonly string _collectionName;
    private readonly IMongoDbContext MongoDbContext;
    public Repository(IWgLog<Repository<T>> logger, IMongoDbContext mongoDbContext, string collectionName)
    {
        _logger = logger;
        _collectionName = collectionName;
        MongoDbContext = mongoDbContext;
    }
    public async Task<T> AddAsync(T entity)
    {
        _logger.Info($"Executing {nameof(AddAsync)} of type {typeof(T)}");
        IMongoCollection<T> collection = GetCollection();
        await collection.InsertOneAsync(entity);
        return entity;
    }

    protected IMongoCollection<T> GetCollection()
    {
        IMongoDatabase mongoDatabase = MongoDbContext.GetDatabase();
        return mongoDatabase.GetCollection<T>(_collectionName);
    }
}