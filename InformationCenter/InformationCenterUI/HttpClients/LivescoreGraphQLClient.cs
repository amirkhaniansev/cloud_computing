using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using InformationCenterUI.Models;

namespace InformationCenterUI.HttpClients
{
    public class LivescoreGraphQLClient
    {
        private readonly string baseUrl;
        private readonly GraphQLHttpClient graphClient;

        public LivescoreGraphQLClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
            this.graphClient = new GraphQLHttpClient(new GraphQLHttpClientOptions() { EndPoint = new Uri(baseUrl + "graphql") }, new SystemTextJsonSerializer());
        }

        public async Task<MatchList> GetMatches()
        {
            var request = new GraphQLHttpRequest("query GetMatches { matches { id, startTime } }");
            var response = await this.graphClient.SendQueryAsync<MatchList>(request);

            return response.Data;
        }
    }
}
