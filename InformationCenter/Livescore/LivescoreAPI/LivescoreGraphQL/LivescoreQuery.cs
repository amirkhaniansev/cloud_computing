using GraphQL.Types;
using LivescoreAPI.LivescoreGraphQL.Types;
using LivescoreDAL.Factories;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;
using System.Collections.Generic;

namespace LivescoreAPI.LivescoreGraphQL
{
    public class LivescoreQuery : ObjectGraphType
    {
        public LivescoreQuery(DalFactory factory)
        {
            this.Field<ListGraphType<MatchObjectGraphType>>(
                "Matches",
                resolve: c =>
                {
                    using var dal = factory.GetMatchDAL();
                    var matches = dal.GetMatches(new MatchSearcher()).GetAwaiter().GetResult();
                    return matches;
                });
        }
    }
}
