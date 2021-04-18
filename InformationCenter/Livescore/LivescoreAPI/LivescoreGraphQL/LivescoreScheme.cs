using System;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace LivescoreAPI.LivescoreGraphQL
{
    public class LivescoreScheme : Schema
    {
        public LivescoreScheme(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<LivescoreQuery>();
        }
    }
}
