using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InformationCenter.Models;

namespace InformationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficJamsController : ControllerBase
    {
        private readonly string connectionString;

        public TrafficJamsController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("InformationCenter");
        }

        [HttpGet]
        public async Task<List<TrafficJam>> Get()
        {
            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand())
            {
                await connection.OpenAsync();

                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[usp_getTrafficJams]";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var list = new List<TrafficJam>();
                    while (await reader.ReadAsync())
                    {
                        var jam = new TrafficJam();
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

                        list.Add(jam);
                    }

                    return list;
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<TrafficJam> Get(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand())
            {
                await connection.OpenAsync();

                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[usp_getTrafficJamById]";
                command.Parameters.Add("id", SqlDbType.Int).Value = id;

                using (var reader = await command.ExecuteReaderAsync())
                {
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
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] TrafficJam jam)
        {
            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand())
            {
                await connection.OpenAsync();

                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[usp_addTrafficJam]";
                command.Parameters.Add("degree", SqlDbType.Int).Value = jam.Degree;
                command.Parameters.Add("street", SqlDbType.NVarChar).Value = jam.Street;
                
                // TODO: Find a way to initialize SQL GEOGRAPHY type
                // command.Parameters.Add("startLocation", SqlDbType.Udt).Value = jam.StartLocation;
                // command.Parameters.Add("endLocation", SqlDbType.Udt).Value = jam.EndLocation;
                
                command.Parameters.Add("returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                await command.ExecuteNonQueryAsync();

                var idd = command.Parameters["returnValue"].Value;

                return CreatedAtAction(nameof(Get), new { id = idd }, idd);
            }
        } 
    }
}