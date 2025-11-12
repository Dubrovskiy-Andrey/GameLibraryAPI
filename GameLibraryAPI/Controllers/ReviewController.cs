using GameLibraryAPI.Models.DTO.ReviewDTO;
using GameLibraryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_reviewService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var review = _reviewService.GetById(id);
            return review == null
                ? NotFound(new { message = $"Отзыв с Id = {id} не найден" })
                : Ok(review);
        }

        [HttpGet("by-game/{gameId}")]
        public IActionResult GetByGame(int gameId)
        {
            var reviews = _reviewService.GetByGame(gameId);
            return Ok(reviews);
        }

        [HttpGet("by-user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var reviews = _reviewService.GetByUser(userId);
            return Ok(reviews);
        }

        [HttpPost]
        public IActionResult Create(ReviewCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _reviewService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ReviewUpdateDto dto)
        {
            var updated = _reviewService.Update(id, dto);
            return updated == null
                ? NotFound(new { message = $"Отзыв с Id = {id} не найден" })
                : Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _reviewService.Delete(id)
                ? NoContent()
                : NotFound(new { message = $"Отзыв с Id = {id} не найден" });
        }
    }
}
