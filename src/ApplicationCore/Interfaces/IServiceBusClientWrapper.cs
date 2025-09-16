using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

/// <summary>
/// Interface for the Service Bus Client.
/// </summary>
public interface IServiceBusClientWrapper
{
    /// <summary>
    /// Send message to service bus queue.
    /// </summary>
    /// <typeparam name="T">Type of the instance</typeparam>
    /// <param name="payload">request object</param>
    /// <returns>void</returns>
    Task SendMessageAsync<T>(T payload);
}
