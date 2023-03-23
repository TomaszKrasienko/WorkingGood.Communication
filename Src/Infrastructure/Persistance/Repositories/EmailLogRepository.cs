using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Common.ConfigModels;
using Microsoft.Extensions.Logging;
using ILogger = DnsClient.Internal.ILogger;

namespace Infrastructure.Persistance.Repositories;

public class EmailLogRepository : IEmailLogRepository
{
    private readonly ILogger<EmailLogConfig> _logger;
    private readonly EmailLogConfig _emailLogConfig;
    private readonly IMongoDbContext _mongoDbContext;
    public EmailLogRepository(ILogger<EmailLogConfig> logger, EmailLogConfig emailLogConfig, IMongoDbContext mongoDbContext)
    {
        _logger = logger;
        _emailLogConfig = emailLogConfig;
        _mongoDbContext = mongoDbContext;
    }
    public async Task AddLog(EmailLog emailLog)
    {
        try
        {
            var db = _mongoDbContext.GetDatabase();
            var collection = db.GetCollection<EmailLog>(_emailLogConfig.Name);
            await collection.InsertOneAsync(emailLog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}