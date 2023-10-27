using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetPlayersForCommand(Guid commandId)
        {
            var command = await _repository.Command.GetCommandAsync(commandId, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {commandId} doesn't exist in the  database.");
                return NotFound();
            }
            var playersFromDb = await _repository.Player.GetPlayersAsync(commandId, trackChanges: false);
            var playersDto = _mapper.Map<IEnumerable<PlayerDto>>(playersFromDb);
            return Ok(playersDto);
        }

        [HttpGet("{id}", Name = "GetPlayerForCommand")]
        public async Task<IActionResult> GetPlayerForCommand(Guid commandId, Guid id)
        {
            var command = await _repository.Command.GetCommandAsync(commandId, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {commandId} doesn't exist in the database.");
                return NotFound();
            }
            var playerDb = await _repository.Player.GetPlayerAsync(commandId, id, trackChanges: false);
            if (playerDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var player = _mapper.Map<PlayerDto>(playerDb);
            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayerForCommand(Guid commandId, [FromBody] PlayerForCreationDto player)
        {
            if (player == null)
            {
                _logger.LogError("PlayerForCreationDto object sent from client is null.");
                return BadRequest("PlayerForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var command = await _repository.Command.GetCommandAsync(commandId, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {commandId} doesn't exist in the database.");
                return NotFound();
            }
            var playerEntity = _mapper.Map<Player>(player);
            _repository.Player.CreatePlayerForCommand(commandId, playerEntity);
            await _repository.SaveAsync();
            var playerToReturn = _mapper.Map<PlayerDto>(playerEntity);
            return CreatedAtRoute("GetPlayerForCommand", new
            {
                commandId,
                id = playerToReturn.Id
            }, playerToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerForCommand(Guid commandId, Guid id)
        {
            var command = await _repository.Command.GetCommandAsync(commandId, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {commandId} doesn't exist in the database.");
                return NotFound();
            }
            var playerForCommand = await _repository.Player.GetPlayerAsync(commandId, id, trackChanges: false);
            if (playerForCommand == null)
            {
                _logger.LogInfo($"Command with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Player.DeletePlayer(playerForCommand);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayerForCommand(Guid commandId, Guid id, [FromBody] PlayerForUpdateDto player)
        {
            if (player == null)
            {
                _logger.LogError("PlayerForUpdateDto object sent from client is null.");
                return BadRequest("PlayerForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var command = await _repository.Command.GetCommandAsync(commandId, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {commandId} doesn't exist in the database.");
                return NotFound();
            }
            var playerEntity = await _repository.Player.GetPlayerAsync(commandId, id, trackChanges: true);
            if (playerEntity == null)
            {
                _logger.LogInfo($"Player with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(player, playerEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdatePlayerForCommand(Guid commandId, Guid id, [FromBody] JsonPatchDocument<PlayerForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var command = await _repository.Command.GetCommandAsync(commandId, trackChanges: false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {commandId} doesn't exist in the database.");
                return NotFound();
            }
            var playerEntity = await _repository.Player.GetPlayerAsync(commandId, id, trackChanges: true);
            if (playerEntity == null)
            {
                _logger.LogInfo($"Player with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var playerToPatch = _mapper.Map<PlayerForUpdateDto>(playerEntity);
            patchDoc.ApplyTo(playerToPatch);
            TryValidateModel(playerToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(playerToPatch, playerEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
