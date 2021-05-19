using FilmsAPI.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TrafficJamsAPI.Proto;

namespace InformationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilmsController : ControllerBase
    {
        private readonly string connectionString;
        public FilmsController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("CinemaDB");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Film>> Get()
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_getFilms]";

            using var reader = await command.ExecuteReaderAsync();
            var list = new List<Film>();
            while (await reader.ReadAsync())
            {
                var film = new Film
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Stars = reader.GetString("Stars"),
                    Category = reader.GetString("Category"),
                    Cinema = reader.GetString("Cinema"),
                    CinemaAddress = reader.GetString("CinemaAddress")
                };

                list.Add(film);
            }
            //GrpcChannel channel = GrpcChannel.ForAddress("https://informationcenter.azurewebsites.net/");
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001/");
            TrafficJams.TrafficJamsClient client = new TrafficJams.TrafficJamsClient(channel);
            Response res;
            foreach (var item in list)
            {
                res = await client.CheckJamAsync(new Address { Address_ = item.CinemaAddress, DateTime = Timestamp.FromDateTime(DateTime.UtcNow) });
                item.IsJam = res.Response_;
            }
            return list;
        }

        [HttpGet("{id}")]
        public async Task<Film> Get(int id)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_getFilmById]";
            command.Parameters.Add("id", SqlDbType.Int).Value = id;

            using var reader = await command.ExecuteReaderAsync();
            var film = new Film();
            while (await reader.ReadAsync())
            {
                film.Id = reader.GetInt32("Id");
                film.Name = reader.GetString("Name");
                film.Stars = reader.GetString("Stars");
                film.Category = reader.GetString("Category");
                film.Cinema = reader.GetString("Cinema");
            }

            return film;
        }

        [HttpGet("{cinema}/GetFilms")]
        public async Task<List<Film>> GetFilms(string cinema)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_getFilmsByCinema]";
            command.Parameters.Add("@cinema", SqlDbType.NVarChar, 255).Value = cinema;

            using var reader = await command.ExecuteReaderAsync();
            var list = new List<Film>();
            while (await reader.ReadAsync())
            {
                var film = new Film
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Stars = reader.GetString("Stars"),
                    Category = reader.GetString("Category"),
                };
                list.Add(film);
            }
            return list;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Film film)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_addFilm]";
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = film.Name;
            command.Parameters.Add("@Category", SqlDbType.NVarChar,50).Value = film.Category;
            command.Parameters.Add("@Cinema", SqlDbType.NVarChar, 255).Value = film.Cinema;
            command.Parameters.Add("@Stars", SqlDbType.NVarChar,500).Value = film.Stars;

            command.Parameters.Add("@returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            await command.ExecuteNonQueryAsync();
            var idd = command.Parameters["@returnValue"].Value;

            return CreatedAtAction(nameof(Get), new { id = idd }, idd);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_deleteFilmById]";
            command.Parameters.Add("id", SqlDbType.Int).Value = id;
            command.Parameters.Add("@returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            await command.ExecuteNonQueryAsync();
            var idd = command.Parameters["@returnValue"].Value;

            return CreatedAtAction(nameof(Delete), new { id = idd }, idd);
        }

       

    }
}
