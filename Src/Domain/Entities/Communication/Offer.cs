namespace Domain.Entities;

public record Offer
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string PositionType { get; init; } = string.Empty;
    public double SalaryRangeMin { get; init; }
    public double SalaryRangeMax { get; init; }
    public string Description { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}