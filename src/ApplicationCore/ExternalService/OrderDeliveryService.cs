using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.ExternalService;
public class OrderDeliveryService
{
    public async Task Execute(object data)
    {
        using var client = new HttpClient();

        // Function endpoint with key
        string functionUrl = "https://orderitemreserve.azurewebsites.net/api/Function1?code=hZNPIhZQOqqXiTIH9MJm6YUJuPZbYWEmAUBiCYyZ2iPZAzFutw-8Qw==";

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(functionUrl, content);

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Function Response: {result}");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }
}
