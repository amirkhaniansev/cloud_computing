using InformationCenterUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InformationCenterUI.HttpClients
{
    public class SurveyClient
    {
        public readonly HttpClient client;
        public SurveyClient(HttpClient client)
        {
            this.client = client;
        }
        public async Task<int> PostSalaryRecord(SalaryRecord salary)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("SalariesSurvey", UriKind.Relative),

                Content = new StringContent(JsonSerializer.Serialize(salary), Encoding.UTF8, "application/json")
            };
            var result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                return int.Parse(await result.Content.ReadAsStringAsync());
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        public async Task<List<SalaryRecord>> GetSalaryRecords()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("SalariesSurvey", UriKind.Relative)
            };

            var result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                string cont = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SalaryRecord>>(cont, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        public async Task<SalaryRecord> GetSalaryRecordById(int id)
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("SalariesSurvey/" + id.ToString(), UriKind.Relative),
            };

            var result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                string cont = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SalaryRecord>(cont, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        
        public async Task<int> DeleteSalaryRecordById(int id)
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("SalariesSurvey/" + id.ToString(), UriKind.Relative),
            };
            var result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                return int.Parse(await result.Content.ReadAsStringAsync());
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
    }
}
