using Application.DTOs.EmailTemplate;
using Domain.Enums;
using Domain.Interfaces.Communication;
using Infrastructure.Communication.Broker;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/emailTemplate")]
public class EmailTemplateController : ControllerBase
{
    private IEmailSender _emailSender;
    public EmailTemplateController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost("uploadEmail")]
    public async Task<IActionResult> UploadEmailTemplate(IFormFile emailTemplateDto)
    {
        
        return Ok();
    }
}