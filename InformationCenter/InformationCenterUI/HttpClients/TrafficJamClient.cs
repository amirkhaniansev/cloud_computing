﻿using InformationCenterUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InformationCenterUI.HttpClients
{
    public class TrafficJamClient
    {
        public readonly HttpClient client;


        public TrafficJamClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://informationcenter.azurewebsites.net/api/");
            this.client = client;
        }

        public async Task<int> PostTrafficJam(TrafficJam jam)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri("TrafficJams");
            
            request.Content = new StringContent(JsonSerializer.Serialize(jam),Encoding.UTF8);
            var result = await  client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                return int.Parse(await result.Content.ReadAsStringAsync());
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
    }
}