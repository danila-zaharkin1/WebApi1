using Contracts;
using Entities.Models;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CommandRepository : RepositoryBase<Command>, ICommandRepository
    {
        public CommandRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Command>> GetAllCommandsAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        public async Task<Command> GetCommandAsync(Guid commandId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(commandId), trackChanges).SingleOrDefaultAsync();
        public void CreateCommand(Command command) => Create(command);
        public async Task<IEnumerable<Command>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void DeleteCommand(Command command) => Delete(command);

    }
}
