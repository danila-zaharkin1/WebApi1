using Entities.Models;

namespace Contracts
{
    public interface ICommandRepository
    {
        public IEnumerable<Command> GetAllCommands(bool trackChanges);
        Command GetCommand(Guid commandId, bool trackChanges);

    }
}
