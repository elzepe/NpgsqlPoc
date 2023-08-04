using Npgsql;
using NpgsqlPoc.Database.Interface;
using NpgsqlPoc.Models;
using NpgsqlTypes;
using System.Text.Json.Nodes;

namespace NpgsqlPoc.Database
{
    public class PostgresCommand : IDatabaseCommand
    {
        private readonly NpgsqlConnection _connection;
        public PostgresCommand()
        {
            _connection = new NpgsqlConnection(
            //connectionString: "Server=localhost;Port=5432;User Id=qpr;Password=example;Database=qpr_db;");
            connectionString: "Server=npgsqlpoc-db-1;Port=5432;User Id=qpr;Password=example;Database=qpr_db;");

        }
        public async Task<int> CreateOrUpdateDto(ExampleDto exampleDto)
        {
            _connection.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = _connection;
            if (exampleDto.Id == 0)
            {
                cmd.CommandText = "INSERT INTO ExampleJson (Json) VALUES (@json)";
                cmd.Parameters.AddWithValue("json", NpgsqlDbType.Jsonb, exampleDto.Json.ToJsonString());
            }
            else
            {
                cmd.CommandText = $"UPDATE ExampleJson" +
                $" SET Json=@json" +
                $" WHERE id = @id";

                cmd.Parameters.AddWithValue("id", exampleDto.Id);
                cmd.Parameters.AddWithValue("json", NpgsqlDbType.Jsonb, exampleDto.Json.ToJsonString());
            }
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> DeleteDto(int id)
        {
            _connection.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = _connection;
            cmd.CommandText = $"DELETE FROM ExampleJson WHERE id = @id";
            cmd.Parameters.AddWithValue("id", id);
            return (await cmd.ExecuteNonQueryAsync()) > 0;
        }

        public async Task<List<ExampleDto>> GetDtos()
        {
            _connection.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = _connection;
            cmd.CommandText = $"SELECT id, json FROM ExampleJson";
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            var result = new List<ExampleDto>();
            while (await reader.ReadAsync())
            {
                var json = reader["json"] as string;
                result.Add(new ExampleDto
                {
                    Id = (int)reader["id"],
                    Json = string.IsNullOrEmpty(json) ? JsonNode.Parse("") : JsonObject.Parse(json)

                });
            }
            return result;
        }

        public async Task<bool> CreateExampleJsonTable() 
        {
            bool ok = true;
            try
            {
                _connection.Open();
                using var cmd = new NpgsqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = $"CREATE TABLE public.examplejson (id SERIAL PRIMARY KEY,json jsonb NULL);";
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception) 
            {
                ok = false;
            }
            
            return ok;
        }

        public bool CanConnectToDocker() 
        {
            try {
                var connection = new NpgsqlConnection(
                connectionString: "Server=localhost;Port=5432;User Id=qpr;Password=example;Database=qpr_db;");
                connection.Open();
                return true;
            }
            catch { 
                return false;
            }
        }
    }
}
