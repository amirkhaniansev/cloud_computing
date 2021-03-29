using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SalariesSurveyAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalariesSurveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesSurveyController : ControllerBase
    {
        private readonly string connectionString;

        public SalariesSurveyController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("SalariesSurveyDB");
        }

        [HttpGet]
        public async Task<List<SalaryRecord>> Get()
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_getSalaryRecords]";

            using var reader = await command.ExecuteReaderAsync();
            var list = new List<SalaryRecord>();
            while (await reader.ReadAsync())
            {
                var record = new SalaryRecord
                {
                    Id = reader.GetInt32("Id"),
                    CreationDate = reader.GetDateTime("CreationDate"),
                    Company = reader.GetString("Company"),
                    Position = reader.GetString("Position"),
                    Salary = reader.GetInt32("Salary"),
                    Experience = reader.GetInt32("Experience")
                };

                list.Add(record);
            }
            return list;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] SalaryRecord record)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_addSalaryRecord]";
            // command.Parameters.Add("CreationDate", SqlDbType.DateTime).Value = record.CreationDate;
            command.Parameters.Add("Company", SqlDbType.VarChar).Value = record.Company;
            command.Parameters.Add("Position", SqlDbType.VarChar).Value = record.Position;
            command.Parameters.Add("Salary", SqlDbType.Int).Value = record.Salary;
            command.Parameters.Add("Experience", SqlDbType.Int).Value = record.Experience;

            command.Parameters.Add("returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            await command.ExecuteNonQueryAsync();
            var createdId = command.Parameters["returnValue"].Value;

            return CreatedAtAction(nameof(Get), new { id = createdId }, createdId);
        }
    }
}