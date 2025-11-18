using AutoMapper;
using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.UserDTO;
using GameLibraryAPI.Repositories;

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

        public User GetById(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null)
                throw new ArgumentException($"Пользователь с Id = {id} не найден");

            return user;
        }

        public User Update(int id, User user)
        {
            var existing = _userRepo.GetById(id);
            if (existing == null)
                throw new ArgumentException($"Пользователь с Id = {id} не найден");

            if (string.IsNullOrWhiteSpace(user.Login))
                throw new ArgumentException("Логин не может быть пустым");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email не может быть пустым");

            var role = _roleRepo.GetById(user.RoleId);
            if (role == null)
                throw new ArgumentException($"Роль с Id = {user.RoleId} не найдена");

            existing.Login = user.Login;
            existing.Email = user.Email;
            existing.RoleId = user.RoleId;

            return _userRepo.Update(existing);
        }

        public bool Delete(int id)
        {
            var deleted = _userRepo.Delete(id);
            if (!deleted)
                throw new ArgumentException($"Пользователь с Id = {id} не найден");

            return true;
        }
    }
}
