using GameLibraryAPI.Models;
using GameLibraryAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _repo.GetById(id);
            if (user == null)
                return NotFound("Пользователь не найден");
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _repo.GetAll();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Объект пользователя пуст");

            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Email))
                return BadRequest("Поля логина и email обязательны");

            var created = _repo.Create(user);

            return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
                return BadRequest("Некорректные данные");
            if (id != user.Id)
                return BadRequest("Несовпадение ID");

            var existing = _repo.GetById(id);
            if (existing == null)
                return NotFound("Пользователь не найден");

            existing.Login = user.Login;
            existing.Email = user.Email;
            existing.RoleId = user.RoleId;

            var updated = _repo.Update(existing);
            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var deleted = _repo.Delete(id);
            if (!deleted)
                return NotFound("Пользователь не найден");

            return NoContent();
        }
    }
}
