using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TrafficJamsAPI.Models;
using Microsoft.SqlServer.Types;
//using Azure.Messaging.ServiceBus;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;
using Azure.Messaging.ServiceBus;
using System;
using System.Text;

namespace InformationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficJamsController : ControllerBase
    {
        private readonly string connectionString;
        private readonly string connectionStringBus;
        private readonly string queueName;
        public TrafficJamsController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("TrafficJamsDB");
            this.connectionStringBus = configuration.GetConnectionString("ServiceBusCnn");
            this.queueName = configuration.GetConnectionString("QueueName"); 
        }

        [HttpGet]
        public async Task<List<TrafficJam>> Get()
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_getTrafficJams]";

            using var reader = await command.ExecuteReaderAsync();
            var list = new List<TrafficJam>();
            while (await reader.ReadAsync())
            {
                var jam = new TrafficJam
                {
                    Id = reader.GetInt32("Id"),
                    Degree = reader.GetInt32("Degree"),
                    Street = reader.GetString("Street")
                };

                if (!reader.IsDBNull("StartLocationLong"))
                {
                    jam.StartLocation = new Location
                    {
                        Longitude = reader.GetDouble("StartLocationLong"),
                        Lattitude = reader.GetDouble("StartLocationLat")
                    };
                }

                if (!reader.IsDBNull("EndLocationLong"))
                {
                    jam.EndLocation = new Location
                    {
                        Longitude = reader.GetDouble("EndLocationLong"),
                        Lattitude = reader.GetDouble("EndLocationLat")
                    };
                }

                list.Add(jam);
            }

            return list;
        }

        [HttpGet("{id}")]
        public async Task<TrafficJam> Get(int id)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_getTrafficJamById]";
            command.Parameters.Add("id", SqlDbType.Int).Value = id;

            using var reader = await command.ExecuteReaderAsync();
            var jam = new TrafficJam();
            while (await reader.ReadAsync())
            {
                jam.Id = reader.GetInt32("Id");
                jam.Degree = reader.GetInt32("Degree");
                jam.Street = reader.GetString("Street");

                if (!reader.IsDBNull("StartLocationLong"))
                {
                    jam.StartLocation = new Location
                    {
                        Longitude = reader.GetDouble("StartLocationLong"),
                        Lattitude = reader.GetDouble("StartLocationLat")
                    };
                }

                if (!reader.IsDBNull("EndLocationLong"))
                {
                    jam.EndLocation = new Location
                    {
                        Longitude = reader.GetDouble("EndLocationLong"),
                        Lattitude = reader.GetDouble("EndLocationLat")
                    };
                }
            }

            return jam;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] TrafficJam jam)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_addTrafficJam]";
            command.Parameters.Add("degree", SqlDbType.Int).Value = jam.Degree;
            command.Parameters.Add("street", SqlDbType.NVarChar).Value = jam.Street;

            command.Parameters.Add(this.ConstructSqlGeography("startLocation", jam.StartLocation));
            command.Parameters.Add(this.ConstructSqlGeography("endLocation", jam.EndLocation));

            command.Parameters.Add("returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            await command.ExecuteNonQueryAsync();

            var idd = command.Parameters["returnValue"].Value;

            return CreatedAtAction(nameof(Get), new { id = idd }, idd);
        }
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] TrafficJam jam)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "Update [dbo].[TrafficJam] Set [Degree] = @degree Where [Id] = @id";
            command.Parameters.Add("@degree", SqlDbType.Int).Value = jam.Degree;
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            await command.ExecuteNonQueryAsync();


            await using ServiceBusClient busClient = new ServiceBusClient(this.connectionStringBus);
            //var busClient = new QueueClient(this.connectionStringBus, this.queueName);
            string json = JsonSerializer.Serialize(jam);
            ServiceBusMessage busMessage = new ServiceBusMessage(json);
            Message message = new Message(Encoding.ASCII.GetBytes(json));

            var sender = busClient.CreateSender(this.queueName);
            await sender.SendMessageAsync(busMessage);
        }
        private SqlParameter ConstructSqlGeography(string name, Location location)
        {
            var geo = SqlGeography.Point(location.Lattitude, location.Longitude, 4326);
            var parameter = new SqlParameter
            {
                ParameterName = name,
                Value = geo,
                SqlDbType = SqlDbType.Udt,
                UdtTypeName = "Geography"
            };

            return parameter;
        }
    }
}