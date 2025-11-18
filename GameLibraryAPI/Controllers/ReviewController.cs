using GameLibraryAPI.Models.DTO.ReviewDTO;
using GameLibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public IActionResult GetAll() => Ok(_reviewService.GetAll());
        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var review = _reviewService.GetById(id);
            return review == null
                ? NotFound(new { message = $"Отзыв с Id = {id} не найден" })
                : Ok(review);
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("by-game/{gameId}")]
        public IActionResult GetByGame(int gameId)
        {
            var reviews = _reviewService.GetByGame(gameId);
            return Ok(reviews);
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("by-user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var reviews = _reviewService.GetByUser(userId);
            return Ok(reviews);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(ReviewCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _reviewService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, ReviewUpdateDto dto)
        {
            var updated = _reviewService.Update(id, dto);
            return updated == null
                ? NotFound(new { message = $"Отзыв с Id = {id} не найден" })
                : Ok(updated);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _reviewService.Delete(id)
                ? NoContent()
                : NotFound(new { message = $"Отзыв с Id = {id} не найден" });
        }
    }
}
