﻿using TwoDayDemoBank.Common;
using TwoDayDemoBank.Worker.Notifications.ApiClients.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TwoDayDemoBank.Worker.Notifications.ApiClients
{
    public class CustomersApiClient : ICustomersApiClient
    {
        private readonly HttpClient _client;
        
        public CustomersApiClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CustomerDetails> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {            
            using var response = await _client.GetStreamAsync($"customers/{customerId}");
            var result = await JsonSerializer.DeserializeAsync<CustomerDetails>(response, JsonSerializerDefaultOptions.Defaults, cancellationToken: cancellationToken);
            return result;
        } 
    }
}