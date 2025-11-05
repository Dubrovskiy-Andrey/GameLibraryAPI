using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role? GetByName(string name);
    }
}
