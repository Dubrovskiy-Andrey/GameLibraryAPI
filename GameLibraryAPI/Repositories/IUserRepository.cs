using GameLibraryAPI.Models;

namespace GameLibraryAPI.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByEmail(string email);
    }
}
