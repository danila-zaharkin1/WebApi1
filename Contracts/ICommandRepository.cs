using Entities.Models;

namespace Contracts
{
    public interface ICommandRepository
    {
        public IEnumerable<Command> GetAllCommands(bool trackChanges);
        Command GetCommand(Guid commandId, bool trackChanges);
        void CreateCommand(Command command);
        IEnumerable<Command> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCommand(Command command);
    }
}
