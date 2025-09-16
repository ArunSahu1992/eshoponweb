using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Microsoft.eShopWeb.ApplicationCore.ExternalService;
public class ServiceBusClientWrapper : IServiceBusClientWrapper
{
    private readonly ServiceBusClient _client;
    private readonly string _queueName;

    public ServiceBusClientWrapper(IConfiguration config)
    {
        if (config == null) throw new ArgumentNullException(nameof(config));

        var connectionString = config["ServiceBus:ConnectionString"]
                               ?? throw new InvalidOperationException("Missing ServiceBus:ConnectionString in config");

        _queueName = config["ServiceBus:QueueName"]
                     ?? throw new InvalidOperationException("Missing ServiceBus:QueueName in config");

        _client = new ServiceBusClient(connectionString);
    }

    /// <summary>
    /// Send message to service bus queue.
    /// </summary>
    /// <typeparam name="T">Type of the instance</typeparam>
    /// <param name="payload">request object</param>
    /// <returns>void</returns>
    public async Task SendMessageAsync<T>(T payload)
    {
        var sender = _client.CreateSender(_queueName);
        string body = JsonSerializer.Serialize(payload);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(body));
        try
        {
            await sender.SendMessageAsync(message);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Azure Function call failed: {ex.Message}");
        }
    }
}
