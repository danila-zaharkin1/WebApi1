using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Player>> GetPlayersAsync(Guid commandId, bool trackChanges) => await FindByCondition(e => e.CommandId.Equals(commandId), trackChanges).OrderBy(e => e.Name).ToListAsync();
        public async Task<Player> GetPlayerAsync(Guid commandId, Guid id, bool trackChanges) => await FindByCondition(e => e.CommandId.Equals(commandId) &&
                e.Id.Equals(id),trackChanges).SingleOrDefaultAsync();
        public void CreatePlayerForCommand(Guid commandId, Player player)
        {
            player.CommandId = commandId;
            Create(player);
        }
        public void DeletePlayer(Player player) => Delete(player);
    }
}
