using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using LivescoreAPI.Constants;
using LivescoreDAL.Factories;

namespace LivescoreAPI.Controllers
{
    [Route(Routes.Competition)]
    public class CompetitionController : LivescoreSwaggerController
    {
        public CompetitionController(DalFactory factory) : base(factory)
        {
        }

        public async Task<ApiResponse> Get(int id)
        {

        }
    }
}
