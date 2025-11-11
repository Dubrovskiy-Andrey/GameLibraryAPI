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

        private static PlatformDto MapDto(Platform p) => new PlatformDto { Id = p.Id, Name = p.Name };

        public IEnumerable<PlatformDto> GetAll() => _platformRepo.GetAll().Select(MapDto);
        public PlatformDto? GetById(int id) => _platformRepo.GetById(id) is { } p ? MapDto(p) : null;

        public PlatformDto Create(PlatformCreateDto dto)
        {
            var created = _platformRepo.Create(new Platform { Name = dto.Name });
            return MapDto(created);
        }

        public PlatformDto? Update(int id, PlatformUpdateDto dto)
        {
            var platform = _platformRepo.GetById(id);
            if (platform == null) return null;

            platform.Name = dto.Name;
            var updated = _platformRepo.Update(platform);
            return MapDto(updated);
        }

        public bool Delete(int id) => _platformRepo.Delete(id);
    }
}
