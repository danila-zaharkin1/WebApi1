using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/commands/{commandId}/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PlayersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPlayersForCommand(Guid commandId)
        {
            var command = _repository.Command.GetCommand(commandId, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {commandId} doesn't exist in the  database.");
                return NotFound();
            }
            var playersFromDb = _repository.Player.GetPlayers(commandId, trackChanges: false);
            var playersDto = _mapper.Map<IEnumerable<PlayerDto>>(playersFromDb);
            return Ok(playersDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayerForCommand(Guid commandId, Guid id)
        {
            var command = _repository.Command.GetCommand(commandId, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {commandId} doesn't exist in the database.");
                return NotFound();
            }
            var playerDb = _repository.Player.GetPlayer(commandId, id, trackChanges: false);
            if (playerDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var player = _mapper.Map<PlayerDto>(playerDb);
            return Ok(player);
        }
    }
}
