namespace Infrastructure.Common.ConfigModels;

public record MongoDbConnectionConfig
{
    public string ConnectionString { get; init; } = string.Empty;
    public string Database { get; init; } = string.Empty;
    public string EmailLogIndex { get; init; } = string.Empty;
    public string EmailTemplatesIndex { get; set; } = string.Empty;
}