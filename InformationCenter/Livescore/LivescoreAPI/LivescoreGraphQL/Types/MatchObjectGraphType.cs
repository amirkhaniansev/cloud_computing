using GraphQL.Types;
using LivescoreDAL.Models;

namespace LivescoreAPI.LivescoreGraphQL.Types
{
    public class MatchObjectGraphType : ObjectGraphType<Match>
    {
        public MatchObjectGraphType()
        {
            this.Field(m => m.Id);
            this.Field(m => m.StartTime);
            this.Field(m => m.Description);
            this.Field()
        }
    }
}
