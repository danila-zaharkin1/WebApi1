using Entities.Models;

namespace Contracts
{
    public interface ICommandRepository
    {
        public Task<IEnumerable<Command>> GetAllCommandsAsync(bool trackChanges);
        Task<Command> GetCommandAsync(Guid commandId, bool trackChanges);
        void CreateCommand(Command command);
        Task<IEnumerable<Command>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCommand(Command command);
    }
}
