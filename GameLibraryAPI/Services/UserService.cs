using AutoMapper;
using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.UserDTO;
using GameLibraryAPI.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GameLibraryAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IRoleRepository roleRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepo.GetAll();
        }

        public User? GetById(int id)
        {
            return _userRepo.GetById(id);
        }

        public User? Update(int id, User user)
        {
            var existing = _userRepo.GetById(id);
            if (existing == null)
                return null;

            existing.Login = user.Login;
            existing.Email = user.Email;
            existing.RoleId = user.RoleId;

            return _userRepo.Update(existing);
        }

        public bool Delete(int id)
        {
            return _userRepo.Delete(id);
        }
    }
}
