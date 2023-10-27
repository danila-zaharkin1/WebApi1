using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IPlayerRepository
    {
        Task<PagedList<Player>> GetPlayersAsync(Guid commandId, PlayerParameters playerParameters, bool trackChanges);
        Task<Player> GetPlayerAsync(Guid commandId, Guid id, bool trackChanges);
        void CreatePlayerForCommand(Guid commandId, Player player);
        void DeletePlayer(Player player);

    }
}
