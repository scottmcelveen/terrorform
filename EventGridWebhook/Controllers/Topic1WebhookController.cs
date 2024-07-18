using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.AspNetCore.Mvc;

namespace EventGridWebhook.Controllers;

[ApiController]
[Route("[controller]")]
public class Topic1WebhookController : ControllerBase
{
    private readonly ILogger<Topic1WebhookController> _logger;
    private readonly IConfiguration _configuration;

    public Topic1WebhookController(ILogger<Topic1WebhookController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet(Name = "GetHello")]
    public ActionResult Get()
    {
        _logger.LogInformation("GET HELLO");
        return Ok("ok hello");
    }

    [HttpPost(Name = "PostTopic1Message")]
    public async Task<ActionResult> Post()
    {
        _logger.LogInformation("GOT A MESSAGE");
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");
        var connectionString = _configuration["EventHubConnectionString"];
        var eventHubName = _configuration["EventHubName"];
        var producer = new EventHubProducerClient(connectionString, eventHubName);
        try
        {
            var eventBody = new BinaryData($"Event: {DateTime.Now:U}");
            var eventData = new EventData(eventBody);

            await producer.SendAsync([eventData]);
            _logger.LogInformation("SENT TO EVENT HUB");
        }
        catch(Exception ex)
        {
            // Transient failures will be automatically retried as part of the
            // operation. If this block is invoked, then the exception was either
            // fatal or all retries were exhausted without a successful publish.
            _logger.LogInformation(ex.Message);
            throw;
        }
        finally
        {
            await producer.CloseAsync();
        }
        return Ok();
    }
}
