using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        
        public async Task<PagedList<Player>> GetPlayersAsync(Guid commandId, PlayerParameters playerParameters, bool trackChanges)
        {
            var players = await FindByCondition(e => e.CommandId.Equals(commandId), trackChanges).FilterPlayers(playerParameters.MinAge, playerParameters.MaxAge)
                                                    .SearchPlayer(playerParameters.SearchTerm).SortPlayer(playerParameters.OrderBy).ToListAsync();
            return PagedList<Player>.ToPagedList(players, playerParameters.PageNumber, playerParameters.PageSize);
        }
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
