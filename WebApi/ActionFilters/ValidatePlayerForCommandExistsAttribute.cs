using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.ActionFilters
{
    public class ValidatePlayerForCommandExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidatePlayerForCommandExistsAttribute(IRepositoryManager repository,
        ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var commandId = (Guid)context.ActionArguments["commandId"];
            var command = await _repository.Command.GetCommandAsync(commandId,
           false);
            if (command == null)
            {
                _logger.LogInfo($"Command with id: {commandId} doesn't exist in the database.");
                return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var player = await _repository.Player.GetPlayerAsync(commandId, id, trackChanges);
            if (player == null)
            {
                _logger.LogInfo($"Player with id: {id} doesn't exist in the database.");

                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("player", player);
                await next();
            }
        }
    }
}
