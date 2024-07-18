// See https://aka.ms/new-console-template for more information
using Azure;
using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateApplicationBuilder(args).Build();
var configuration = host.Services.GetRequiredService<IConfiguration>();
var namespaceEndpoint = configuration["EventGridNSEndpoint"];
var topicKey = configuration["EventGridTopicKey"];
var topicName = configuration["EventGridTopicName"];

if(namespaceEndpoint is null || topicKey is null || topicName is null)
{
    Console.WriteLine("Please provide the EventGrid namespace endpoint, topic key and topic name as environment variables.");
    return;
}
var client = new EventGridSenderClient(new Uri(namespaceEndpoint), topicName, new AzureKeyCredential(topicKey));

await client.SendAsync(
[
    new CloudEvent("employee_source", "type", new TestModel { Name = "Tom", Age = 55 })
]);

Console.WriteLine("Event has been published to the topic.");

await host.RunAsync();


public class TestModel
{
    public string? Name { get; set; }
    public int Age { get; set; }
}