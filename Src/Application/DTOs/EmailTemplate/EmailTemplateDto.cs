using Microsoft.AspNetCore.Http;

namespace Application.DTOs.EmailTemplate;

public record EmailTemplateDto
{
    public string Name { get; init; } = string.Empty;
    public IFormFile? Template { get; init; }
}