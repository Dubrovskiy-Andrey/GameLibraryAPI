using GameLibraryAPI.Models.DTO.PlatformDTO;

namespace GameLibraryAPI.Services
{
    public interface IPlatformService
    {
        IEnumerable<PlatformDto> GetAll();
        PlatformDto? GetById(int id);
        PlatformDto Create(PlatformCreateDto dto);
        PlatformDto? Update(int id, PlatformUpdateDto dto);
        bool Delete(int id);
    }
}
