using MongoDB.Driver;

namespace Infrastructure.Persistance;

public interface IMongoDbContext
{
    IMongoDatabase GetDatabase();
}