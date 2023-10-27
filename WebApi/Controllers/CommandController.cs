using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CommandController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCommands()
        {
            var commands = _repository.Command.GetAllCommands(trackChanges: false);
            var commandsDto = _mapper.Map<IEnumerable<CommandDto>>(commands);
            return Ok(commandsDto);
        }

        [HttpGet("{id}", Name = "CommandById")]
        public IActionResult GetCommand(Guid id)
        {
            var command = _repository.Command.GetCommand(id, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var commandDto = _mapper.Map<CommandDto>(command);
                return Ok(commandDto);
            }
        }
    }
}
