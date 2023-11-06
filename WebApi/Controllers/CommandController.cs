using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.ActionFilters;
using WebApi.ModelBinders;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
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
        public async Task<IActionResult> GetCommands()
        {
            var commands = await _repository.Command.GetAllCommandsAsync(trackChanges: false);
            var commandsDto = _mapper.Map<IEnumerable<CommandDto>>(commands);
            return Ok(commandsDto);
        }

        [HttpGet("{id}", Name = "CommandById")]
        public async Task<IActionResult> GetCommand(Guid id)
        {
            var command = await _repository.Command.GetCommandAsync(id, trackChanges: false);
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CommandForCreationDto command)
        {         
            var commandEntity = _mapper.Map<Command>(command);
            _repository.Command.CreateCommand(commandEntity);
            await _repository.SaveAsync();
            var commandToReturn = _mapper.Map<CommandDto>(commandEntity);
            return CreatedAtRoute("CommandById", new { id = commandToReturn.Id },
            commandToReturn);
        }

        [HttpGet("collection/({ids})", Name = "CommandCollection")]
        public async Task<IActionResult> GetCommandCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var commandEntities = await _repository.Command.GetByIdsAsync(ids, trackChanges: false);
            if (ids.Count() != commandEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var commandsToReturn = _mapper.Map<IEnumerable<CommandDto>>(commandEntities);
            return Ok(commandsToReturn);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCommandCollection([FromBody] IEnumerable<CommandForCreationDto> commmandCollection)
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
            await _repository.SaveAsync();
            var commandCollectionToReturn = _mapper.Map<IEnumerable<CommandDto>>(commandEntities);
            var ids = string.Join(",", commandCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("CommandCollection", new { ids },
            commandCollectionToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommand(Guid id)
        {
            var command = await _repository.Command.GetCommandAsync(id, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Command.DeleteCommand(command);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCommand(Guid id, [FromBody] CommandForUpdateDto command)
        {          
            var commandEntity = await _repository.Command.GetCommandAsync(id, trackChanges: true);
            if (commandEntity == null)
            {
                _logger.LogInfo($"Command with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(command, commandEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpOptions]   
        public IActionResult GetCommandsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
