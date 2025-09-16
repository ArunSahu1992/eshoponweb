using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

/// <summary>
/// Interface for the Azure Function Client.
/// </summary>
public interface IAzureFunctionClient
{
    /// <summary>
    /// Execute Order Function Async
    /// </summary>
    /// <param name="payload">request object</param>
    /// <returns>void</returns>
    Task ExecuteAsync(object payload);
}

