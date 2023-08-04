using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NpgsqlPoc.Database.Interface;
using NpgsqlPoc.Models;
using System.Text.Json.Nodes;

namespace NpgsqlPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonExampleController : ControllerBase
    {
        private IDatabaseCommand _command;
        public JsonExampleController(IDatabaseCommand command)
        {
            _command = command;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hi");
        }

        [HttpGet]
        [Route("CanConnect")]
        public IActionResult CanConnect() 
        {
            return Ok(_command.CanConnectToDocker());
        }

        [HttpPost]
        [Route("/CreateOrUpdateDto")]
        public async Task<IActionResult> CreateOrUpdateDto(ExampleDto example)
        {
            return Ok(await _command.CreateOrUpdateDto(example));
        }

        [HttpDelete]
        [Route("/DeleteDto/{id}")]
        public async Task<IActionResult> CreateOrUpdate(int id)
        {
            return Ok(await _command.DeleteDto(id));
        }

        [HttpGet]
        [Route("/GetDtos")]
        public async Task<IActionResult> GetDtos()
        {
            return Ok(await _command.GetDtos());
        }

        [HttpGet]
        [Route("/CreateTableExampleJson")]
        public async Task<IActionResult> CreateExampleJsonTable() 
        {
            return Ok(await _command.CreateExampleJsonTable());
        }

    }
}
