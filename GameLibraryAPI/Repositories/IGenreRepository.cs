using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Genre? GetByName(string name);
    }
}
