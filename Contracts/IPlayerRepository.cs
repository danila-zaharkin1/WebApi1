using Entities.Models;

namespace Contracts
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayersAsync(Guid commandId, bool trackChanges);
        Task<Player> GetPlayerAsync(Guid commandId, Guid id, bool trackChanges);
        void CreatePlayerForCommand(Guid commandId, Player player);
        void DeletePlayer(Player player);

    }
}
