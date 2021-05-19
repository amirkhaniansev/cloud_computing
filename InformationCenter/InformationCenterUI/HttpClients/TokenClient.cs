using InformationCenterUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace InformationCenterUI.HttpClients
{
    public class TokenClient
    {
        public readonly HttpClient client;

        public TokenClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> GetToken()
        {
            string parameters = "{\"client_id\": \"UHdOXD22NSbDOF7WapQHHwHlV4gAkabT\",\"client_secret\": \"g-B7_forjsNLlT1oxPW9oGGhKyzrlEYIrt_otqvEYDsmjBUnHqoVlEfcQSKAtP0_\",\"grant_type\": \"client_credentials\",\"audience\": \"https://informationcentertokens/api\"}";
            var response = await client.PostAsync("https://dev-jtbl2015.eu.auth0.com/oauth/token", new StringContent(parameters));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                return (await JsonSerializer.DeserializeAsync<TokenResponse>(result)).Access_token;
            }
            throw new InvalidOperationException();
        }
    }
}
