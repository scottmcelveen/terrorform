// See https://aka.ms/new-console-template for more information
using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateApplicationBuilder(args).Build();
var configuration = host.Services.GetRequiredService<IConfiguration>();
var connectionString = configuration["EventHubConnectionString"];
var eventHubName = configuration["EventHubName"];

if(connectionString is null || eventHubName is null)
{
    Console.WriteLine("Please provide the connection string and event hub name as environment variables.");
    return;
}

Console.WriteLine("Hello, World!");
Console.WriteLine($"Connecting to {connectionString} and reading from {eventHubName}");


var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
var consumer = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName);
try
{
    // To ensure that we do not wait for an indeterminate length of time, we'll
    // stop reading after we receive five events.  For a fresh Event Hub, those
    // will be the first five that we had published.  We'll also ask for
    // cancellation after 90 seconds, just to be safe.

    using var cancellationSource = new CancellationTokenSource();
    cancellationSource.CancelAfter(TimeSpan.FromSeconds(90));

    var maximumEvents = 1;
    var eventDataRead = new List<string>();

    await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(cancellationSource.Token))
    {
        eventDataRead.Add(partitionEvent.Data.EventBody.ToString());
        if (eventDataRead.Count >= maximumEvents)
        {
            break;
        }

    }

    // At this point, the data sent as the body of each event is held
    // in the eventDataRead set.
    eventDataRead.ForEach(m => Console.WriteLine(m));
}
catch
{
    // Transient failures will be automatically retried as part of the
    // operation. If this block is invoked, then the exception was either
    // fatal or all retries were exhausted without a successful read.
}
finally
{
   await consumer.CloseAsync();
}