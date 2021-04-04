using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private readonly string connectionString;

        public NewsController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("NewsDB");
        }

        [HttpGet]
        public async Task<IEnumerable<News>> Get()
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_getAllNews]";

            var collection = new List<News>();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                collection.Add(new News
                {
                    Id = reader.GetInt32("Id"),
                    Title = reader.GetString("Title"),
                    Author = reader.GetString("Author"),
                    Category = reader.GetString("Category"),
                    Content = reader.GetString("Content"),
                    Created = reader.GetDateTime("Created"),
                    FileUrl = reader.GetString("FileUrl")
                });
            }

            return collection;
        }

        [HttpGet("{id}")]
        public async Task<News> Get(int id)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_getNews]";
            command.Parameters.Add("id", SqlDbType.Int).Value = id;

            var news = new News();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                news.Id = reader.GetInt32("Id");
                news.Title = reader.GetString("Title");
                news.Author = reader.GetString("Author");
                news.Category = reader.GetString("Category");
                news.Content = reader.GetString("Content");
                news.Created = reader.GetDateTime("Created");
                news.FileUrl = reader.GetString("FileUrl");
            }

            return news;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] News news)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_addNews]";
            command.Parameters.Add("title", SqlDbType.NVarChar).Value = news.Title ?? string.Empty;
            command.Parameters.Add("author", SqlDbType.NVarChar).Value = news.Author ?? string.Empty;
            command.Parameters.Add("category", SqlDbType.NVarChar).Value = news.Category ?? string.Empty;
            command.Parameters.Add("content", SqlDbType.NVarChar).Value = news.Content ?? string.Empty;
            command.Parameters.Add("fileurl", SqlDbType.NVarChar).Value = news.FileUrl ?? string.Empty;
            command.Parameters.Add("created", SqlDbType.DateTime2).Value = DateTime.Now;
            command.Parameters.Add("returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            await command.ExecuteNonQueryAsync();
            var newsId = command.Parameters["returnValue"].Value;

            return CreatedAtAction(nameof(Get), new { id = newsId }, newsId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand();
            await connection.OpenAsync();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[usp_deleteNews]";
            command.Parameters.Add("id", SqlDbType.Int).Value = id;
            command.Parameters.Add("returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            await command.ExecuteNonQueryAsync();
            var newsId = command.Parameters["returnValue"].Value;

            return CreatedAtAction(nameof(Delete), new { id = newsId }, newsId);
        }
    }
}
