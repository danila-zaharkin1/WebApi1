using Contracts;
using Entities.Models;
using Entities;

namespace Repository
{
    public class CommandRepository : RepositoryBase<Command>, ICommandRepository
    {
        public CommandRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public IEnumerable<Command> GetAllCommands(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();
        public Command GetCommand(Guid commandId, bool trackChanges) => FindByCondition(c => c.Id.Equals(commandId), trackChanges).SingleOrDefault();
    }
}
