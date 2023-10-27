using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.ModelBinders;

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

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CommandForCreationDto command)
        {
            if (command == null)
            {
                _logger.LogError("CommandForCreationDto object sent from client is null.");
                return BadRequest("CommandForCreationDto object is null");
            }
            var commandEntity = _mapper.Map<Command>(command);
            _repository.Command.CreateCommand(commandEntity);
            _repository.Save();
            var commandToReturn = _mapper.Map<CommandDto>(commandEntity);
            return CreatedAtRoute("CommandById", new { id = commandToReturn.Id },
            commandToReturn);
        }

        [HttpGet("collection/({ids})", Name = "CommandCollection")]
        public IActionResult GetCommandCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var commandEntities = _repository.Command.GetByIds(ids, trackChanges: false);
            if (ids.Count() != commandEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var commandsToReturn = _mapper.Map<IEnumerable<CommandDto>>(commandEntities);
            return Ok(commandsToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateCommandCollection([FromBody] IEnumerable<CommandForCreationDto> commmandCollection)
        {
            if (commmandCollection == null)
            {
                _logger.LogError("Command collection sent from client is null.");
                return BadRequest("Command collection is null");
            }
            var commandEntities = _mapper.Map<IEnumerable<Command>>(commmandCollection);
            foreach (var command in commandEntities)
            {
                _repository.Command.CreateCommand(command);
            }
            _repository.Save();
            var commandCollectionToReturn = _mapper.Map<IEnumerable<CommandDto>>(commandEntities);
            var ids = string.Join(",", commandCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("CommandCollection", new { ids },
            commandCollectionToReturn);
        }
    }
}
