using InformationCenterUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InformationCenterUI.HttpClients
{
    public class NewsClient
    {
        private readonly HttpClient client;

        public NewsClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<News>> Get()
        {
            var response = client.GetAsync(new Uri("news", UriKind.Relative));
            var result = await response.Result.Content.ReadAsStringAsync();

            if (response.IsCompletedSuccessfully)
            {
                return JsonSerializer.Deserialize<List<News>>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            throw new Exception(result);
        }

        public async Task<News> Get(int id)
        {
            var response = client.GetAsync(new Uri($"news/{id}", UriKind.Relative));
            var result = await response.Result.Content.ReadAsStringAsync();

            if (response.IsCompletedSuccessfully)
            {
                return JsonSerializer.Deserialize<News>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            throw new Exception(result);
        }

        public async Task<int> Post(News news)
        {
            var response = client.PostAsync(new Uri("news", UriKind.Relative), new StringContent(JsonSerializer.Serialize(news), Encoding.UTF8, "application/json"));
            var result = await response.Result.Content.ReadAsStringAsync();

            if (response.IsCompletedSuccessfully)
            {
                return int.Parse(result);
            }

            throw new Exception(result);
        }
    }
}
