using InformationCenterUI.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InformationCenterUI.HttpClients
{
    public class FilmClient
    {
        public readonly HttpClient client;
        public FilmClient(HttpClient client)
        {
            this.client = client;
        }
        public async Task<int> PostFilm(Film film)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("Films", UriKind.Relative),

                Content = new StringContent(JsonSerializer.Serialize(film), Encoding.UTF8, "application/json")
            };
            var result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                return int.Parse(await result.Content.ReadAsStringAsync());
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        public async Task<List<Film>> GetFilms()
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("Films", UriKind.Relative)
            };

            var result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                string cont = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Film>>(cont, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        public async Task<Film> GetFilmById(int id)
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("Films/"+ id.ToString(), UriKind.Relative),
            };
            
            var result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                string cont = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Film>(cont, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        public async Task<List<Film>> GetFilmsByCinema(string name)
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("Films/" + name + "/GetFilms", UriKind.Relative),
            };

            var result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                string cont = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Film>>(cont, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        public async Task<int> DeleteFilmById(int id)
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("Films/" + id.ToString(), UriKind.Relative),
            };            
            var result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                return int.Parse(await result.Content.ReadAsStringAsync());
            }
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        public async Task AddTrailer(int id, byte[] content)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=webservicecoursestorage;AccountKey=kgF8aqe6mI/yQKycwg68HLPcNDVFhPlQlsgGMY6Vu4S9jjbQ5Z74tVGh7NSDMgTQNkXrd20xHZ8gB7G0ZibD3g==;EndpointSuffix=core.windows.net");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("trailers");
            CloudBlockBlob blob = container.GetBlockBlobReference(id.ToString() + "film");
            await blob.UploadFromByteArrayAsync(content, 0, content.Length);
            var url = blob.Uri.ToString();
        }
    }
}
