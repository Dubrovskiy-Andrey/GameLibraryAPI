using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        IEnumerable<Game> GetGamesByGenre(int genreId);
        IEnumerable<Game> GetGamesByPlatform(int platformId);
    }
}
