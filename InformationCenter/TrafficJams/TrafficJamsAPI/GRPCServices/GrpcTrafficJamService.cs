using Grpc.Core;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TrafficJamsAPI.Proto;

namespace TrafficJamsAPI.GRPCSeriveces
{
    public class GrpcTrafficJamService : TrafficJams.TrafficJamsBase
    {
        private readonly string connectionString;
        public GrpcTrafficJamService(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("TrafficJamsDB");
        }
        public override Task<Response> CheckJam(Address request, ServerCallContext context)
        {
            Response res = new();
            using var connection = new SqlConnection(this.connectionString); 
            using var command = new SqlCommand();
            connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_getTrafficJamAddress]";
            command.Parameters.Add("@Street", SqlDbType.NVarChar, 255).Value = request.Address_;
            command.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQueryAsync();

            res.Response_ = (bool)command.Parameters["@ReturnValue"].Value;

            return Task.FromResult(res);
        }
    }
}
