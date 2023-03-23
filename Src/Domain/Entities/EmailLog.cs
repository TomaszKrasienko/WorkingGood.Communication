namespace Domain.Entities;

public class EmailLog
{
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
}