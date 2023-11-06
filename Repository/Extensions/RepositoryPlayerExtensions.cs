using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Repository.Extensions
{
    public static class RepositoryPlaExtensions
    {
        public static IQueryable<Player> FilterPlayers(this IQueryable<Player> players, uint minAge, uint maxAge) =>
                                                                        players.Where(e => (e.Age >= minAge && e.Age <= maxAge));
        public static IQueryable<Player> SearchPlayer(this IQueryable<Player> players,  string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return players;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return players.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
        public static IQueryable<Player> SortPlayer(this IQueryable<Player> players, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return players.OrderBy(e => e.Name);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Player>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return players.OrderBy(e => e.Name);
            return players.OrderBy(orderQuery);
        }
    }
}
