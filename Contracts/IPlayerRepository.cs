using Entities.Models;

namespace Contracts
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetPlayers(Guid commandId, bool trackChanges);
        Player GetPlayer(Guid commandId, Guid id, bool trackChanges);
        void CreatePlayerForCommand(Guid commandId, Player player);
        void DeletePlayer(Player player);

    }
}
