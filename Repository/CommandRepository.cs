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
    }
}
