using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.ExternalService;
public class AzureFunctionClient : IAzureFunctionClient
{
    private readonly HttpClient _httpClient;
    private readonly string _functionUrl;

    public AzureFunctionClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _functionUrl = config["AzureFunctions:OrderFunctionUrl"] ?? throw new InvalidOperationException("AzureFunctions:OrderFunctionUrl is missing in configuration.");
    }

  
    public async Task ExecuteAsync(object payload)
    {
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_functionUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Azure Function call failed: {response.StatusCode} - {body}");
        }
    }
}
