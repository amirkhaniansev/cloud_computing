using InformationCenterUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("TrafficJams", UriKind.Relative),

                Content = new StringContent(JsonSerializer.Serialize(jam), Encoding.UTF8, "application/json")
            };
            var result = await  client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                return int.Parse(await result.Content.ReadAsStringAsync());
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        public async Task<IEnumerable<TrafficJam>> GetTrafficJams()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("TrafficJams", UriKind.Relative)
            };

            var result = await client.SendAsync(request);
            //client.GetAsJson...
            //client.PostAsJsonAsync() ///.Net 5
            if (result.IsSuccessStatusCode)
            {
                string cont = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<TrafficJam>>(cont, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
    }
}
