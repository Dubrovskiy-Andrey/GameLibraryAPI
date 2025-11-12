using GameLibraryAPI.Models.DTO.GenreDTO;
using GameLibraryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_genreService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var genre = _genreService.GetById(id);
            return genre == null ? NotFound(new { message = $"Жанр с Id = {id} не найден" }) : Ok(genre);
        }

        [HttpPost]
        public IActionResult Create(GenreCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _genreService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, GenreUpdateDto dto)
        {
            var updated = _genreService.Update(id, dto);
            return updated == null
                ? NotFound(new { message = $"Жанр с Id = {id} не найден" })
                : Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _genreService.Delete(id)
                ? NoContent()
                : NotFound(new { message = $"Жанр с Id = {id} не найден" });
        }
    }
}
