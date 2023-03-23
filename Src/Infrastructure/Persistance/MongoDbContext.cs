using Infrastructure.Common.ConfigModels;
using MongoDB.Driver;

namespace Infrastructure.Persistance;

public class MongoDbContext : IMongoDbContext
{
    private readonly MongoDbConnectionConfig _mongoDbConnectionConfig;
    public MongoDbContext(MongoDbConnectionConfig mongoDbConnectionConfig)
    {
        _mongoDbConnectionConfig = mongoDbConnectionConfig;
    }
    public IMongoDatabase GetDatabase()
    {
        var client = new MongoClient(_mongoDbConnectionConfig.ConnectionString);
        return client.GetDatabase(_mongoDbConnectionConfig.Database);
    }
}