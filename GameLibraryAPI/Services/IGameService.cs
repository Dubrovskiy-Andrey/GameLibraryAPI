using GameLibraryAPI.Models.DTO.GameDTO;

namespace GameLibraryAPI.Services
{
    public interface IGameService
    {
        IEnumerable<GameDto> GetAll();
        GameDto? GetById(int id);
        GameDto Create(GameCreateDto dto);
        GameDto? Update(int id, GameUpdateDto dto);
        bool Delete(int id);
    }
}
