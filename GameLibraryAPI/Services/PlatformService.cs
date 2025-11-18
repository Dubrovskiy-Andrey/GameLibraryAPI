using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.PlatformDTO;
using GameLibraryAPI.Repositories;

namespace GameLibraryAPI.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository _platformRepo;

        public PlatformService(IPlatformRepository repo)
        {
            _platformRepo = repo;
        }

        private static PlatformDto MapDto(Platform p) =>
            new PlatformDto { Id = p.Id, Name = p.Name };

        public IEnumerable<PlatformDto> GetAll() =>
            _platformRepo.GetAll().Select(MapDto);

        public PlatformDto GetById(int id)
        {
            var platform = _platformRepo.GetById(id);
            if (platform == null)
                throw new ArgumentException($"Платформа с Id = {id} не найдена");

            return MapDto(platform);
        }

        public PlatformDto Create(PlatformCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Название платформы не может быть пустым");

            var created = _platformRepo.Create(new Platform { Name = dto.Name });
            return MapDto(created);
        }

        public PlatformDto Update(int id, PlatformUpdateDto dto)
        {
            var platform = _platformRepo.GetById(id);
            if (platform == null)
                throw new ArgumentException($"Платформа с Id = {id} не найдена");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Название платформы не может быть пустым");

            platform.Name = dto.Name;
            var updated = _platformRepo.Update(platform);

            return MapDto(updated);
        }

        public bool Delete(int id)
        {
            var deleted = _platformRepo.Delete(id);
            if (!deleted)
                throw new ArgumentException($"Платформа с Id = {id} не найдена");

            return true;
        }
    }
}
