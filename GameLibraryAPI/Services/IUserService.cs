using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.UserDTO;

namespace GameLibraryAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User? Update(int id, User user);
        bool Delete(int id);
    }
}
