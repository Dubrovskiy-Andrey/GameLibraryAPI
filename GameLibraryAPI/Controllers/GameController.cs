using GameLibraryAPI.Models.DTO.GameDTO;
using GameLibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var games = _gameService.GetAll();
            return Ok(games);
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var game = _gameService.GetById(id);
            if (game == null)
                return NotFound(new { message = $"Игра с Id = {id} не найдена" });

            return Ok(game);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(GameCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _gameService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, GameUpdateDto dto)
        {
            var updated = _gameService.Update(id, dto);
            if (updated == null)
                return NotFound(new { message = $"Игра с Id = {id} не найдена" });

            return Ok(updated);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _gameService.Delete(id);
            if (!result)
                return NotFound(new { message = $"Игра с Id = {id} не найдена" });

            return NoContent();
        }
    }
}
