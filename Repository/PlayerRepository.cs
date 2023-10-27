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

        public IEnumerable<Player> GetPlayers(Guid commandId, bool trackChanges) => FindByCondition(e => e.CommandId.Equals(commandId), trackChanges).OrderBy(e => e.Name);
        public Player GetPlayer(Guid commandId, Guid id, bool trackChanges) => FindByCondition(e => e.CommandId.Equals(commandId) &&
                e.Id.Equals(id),trackChanges).SingleOrDefault();
        public void CreatePlayerForCommand(Guid commandId, Player player)
        {
            player.CommandId = commandId;
            Create(player);
        }
        public void DeletePlayer(Player player) => Delete(player);
    }
}
