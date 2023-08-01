using NpgsqlPoc.Models;

namespace NpgsqlPoc.Database.Interface
{
    public interface IDatabaseCommand
    {
        public Task<List<ExampleDto>> GetDtos();
        public Task<int> CreateOrUpdateDto(ExampleDto exampleDto);
        public Task<bool> DeleteDto(int id);
        bool CanConnectToDocker();
    }
}
