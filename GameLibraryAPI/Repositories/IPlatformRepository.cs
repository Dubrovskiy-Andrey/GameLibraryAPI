using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public interface IPlatformRepository : IRepository<Platform>
    {
        Platform? GetByName(string name);
    }
}
