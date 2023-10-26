using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
