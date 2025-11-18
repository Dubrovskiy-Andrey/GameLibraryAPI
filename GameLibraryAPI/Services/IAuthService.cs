using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.UserDTO;

namespace GameLibraryAPI.Services
{
    public interface IAuthService
    {
        AuthResponse Register(CreateUserRequest request);
        AuthResponse Login(LoginRequest request);
    }
}
