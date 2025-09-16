using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.ExternalService;
public class ServiceBus
{
    private const string connectionString = "";
    private const string queueName = "processrequest"; // your queue name

    public async Task Execute(object data)
    {
        await using var client = new ServiceBusClient(connectionString);

        // 2. Create a sender for the queue
        ServiceBusSender sender = client.CreateSender(queueName);

        try
        {
            // Example message body as JSON
            var jsonMessage = @"{ ""id"": 101, ""name"": ""Arun"", ""order"": ""Laptop"" }";

            // 3. Create a message
            ServiceBusMessage message = new ServiceBusMessage(JsonConvert.SerializeObject(data));

            // (Optional) Add custom metadata
            message.ApplicationProperties.Add("MessageType", "OrderRequest");

            // 4. Send message
            await sender.SendMessageAsync(message);

            Console.WriteLine($"Message sent to queue: {queueName}");
        }
        catch(Exception ex)
        {

        }
        finally
        {
            // Dispose sender
            await sender.DisposeAsync();
        }
    }
}
