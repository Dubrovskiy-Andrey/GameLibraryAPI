using GameLibraryAPI.Models.DTO.GenreDTO;

namespace GameLibraryAPI.Services
{
    public interface IGenreService
    {
        IEnumerable<GenreDto> GetAll();
        GenreDto? GetById(int id);
        GenreDto Create(GenreCreateDto dto);
        GenreDto? Update(int id, GenreUpdateDto dto);
        bool Delete(int id);
    }
}
