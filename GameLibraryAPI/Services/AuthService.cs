using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.UserDTO;
using GameLibraryAPI.Repositories;
using Microsoft.Extensions.Configuration;

namespace GameLibraryAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IConfiguration _config;

        private readonly TimeSpan _tokenLifetime = TimeSpan.FromMinutes(60);
        private readonly TimeSpan _refreshTokenLifetime = TimeSpan.FromDays(7);

        public AuthService(IUserRepository userRepo, IRoleRepository roleRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _config = config;
        }

        public AuthResponse Register(CreateUserRequest request)
        {
            if (_userRepo.GetByEmail(request.Email) != null)
                return new AuthResponse { Success = false, ErrorMessage = "Пользователь с таким email уже существует" };

            var existingByLogin = _userRepo.GetAll().FirstOrDefault(u => u.Login == request.Login);
            if (existingByLogin != null)
                return new AuthResponse { Success = false, ErrorMessage = "Пользователь с таким логином уже существует" };

            var role = _roleRepo.GetById(request.RoleId) ?? _roleRepo.GetByName("User") ?? new Role { Id = 2, Name = "User" };

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Login = request.Login,
                Email = request.Email,
                PasswordHash = passwordHash,
                RoleId = role.Id,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var created = _userRepo.Create(user);

            var auth = GenerateAuthResponse(created);

            return auth;
        }

        public AuthResponse Login(LoginRequest request)
        {
            var user = _userRepo.GetAll()
                .FirstOrDefault(u => u.Email == request.LoginOrEmail || u.Login == request.LoginOrEmail);

            if (user == null)
                return new AuthResponse { Success = false, ErrorMessage = "Неверные учетные данные" };

            bool ok = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!ok)
                return new AuthResponse { Success = false, ErrorMessage = "Неверные учетные данные" };

            return GenerateAuthResponse(user);
        }


        private AuthResponse GenerateAuthResponse(User user)
        {
            var jwtKey = _config["Jwt:Key"] ?? "MySuperSecretKey";
            var jwtIssuer = _config["Jwt:Issuer"] ?? "GameLibraryAPI";

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            string roleName = user.Role?.Name;
            if (string.IsNullOrEmpty(roleName))
            {
                var role = _roleRepo.GetById(user.RoleId);
                roleName = role?.Name ?? "User";
            }
            claims.Add(new Claim(ClaimTypes.Role, roleName));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var expires = now.Add(_tokenLifetime);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: null,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            return new AuthResponse
            {
                Success = true,
                Token = tokenString,
                RefreshToken = refreshToken,
                ValidTo = expires,
                User = new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    Email = user.Email,
                    RoleName = roleName
                }
            };
        }
    }
}
