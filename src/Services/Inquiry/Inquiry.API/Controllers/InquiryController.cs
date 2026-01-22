using Microsoft.AspNetCore.Mvc;
using Inquiry.API.Data;
using Inquiry.API.Models;
using MassTransit;
using M4Webapp.Shared.Events;

namespace Inquiry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InquiryController : ControllerBase
{
    private readonly InquiryDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public InquiryController(InquiryDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(ContactMessage message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        await _publishEndpoint.Publish(new ContactReceivedEvent(message.Name, message.Email, message.Subject, message.Content));

        return Ok();
    }
}
