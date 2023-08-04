using NpgsqlPoc.Models;

namespace NpgsqlPoc.Database.Interface
{
    public interface IDatabaseCommand
    {
        Task<List<ExampleDto>> GetDtos();
        Task<int> CreateOrUpdateDto(ExampleDto exampleDto);
        Task<bool> DeleteDto(int id);
        Task<bool> CreateExampleJsonTable();
        bool CanConnectToDocker();
    }
}
