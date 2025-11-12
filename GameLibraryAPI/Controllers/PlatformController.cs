using GameLibraryAPI.Models.DTO.PlatformDTO;
using GameLibraryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;

        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_platformService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var platform = _platformService.GetById(id);
            return platform == null
                ? NotFound(new { message = $"Платформа с Id = {id} не найдена" })
                : Ok(platform);
        }

        [HttpPost]
        public IActionResult Create(PlatformCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = _platformService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PlatformUpdateDto dto)
        {
            var updated = _platformService.Update(id, dto);
            return updated == null
                ? NotFound(new { message = $"Платформа с Id = {id} не найдена" })
                : Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _platformService.Delete(id)
                ? NoContent()
                : NotFound(new { message = $"Платформа с Id = {id} не найдена" });
        }
    }
}
