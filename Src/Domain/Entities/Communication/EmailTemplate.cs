namespace Domain.Entities;

public record EmailTemplate 
{
    public string Type { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
}