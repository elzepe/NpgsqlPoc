using System.Text.Json.Nodes;

namespace NpgsqlPoc.Models
{
    public class ExampleDto
    {
        public int Id { get; set; }
        public JsonNode Json { get; set; }
    }
}
