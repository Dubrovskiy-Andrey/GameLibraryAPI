using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.GenreDTO;
using GameLibraryAPI.Repositories;

namespace GameLibraryAPI.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepo;

        public GenreService(IGenreRepository genreRepo)
        {
            _genreRepo = genreRepo;
        }

        private static GenreDto MapDto(Genre genre) => new GenreDto { Id = genre.Id, Name = genre.Name };

        public IEnumerable<GenreDto> GetAll() => _genreRepo.GetAll().Select(MapDto);

        public GenreDto? GetById(int id)
        {
            var genre = _genreRepo.GetById(id);
            return genre == null ? null : MapDto(genre);
        }

        public GenreDto Create(GenreCreateDto dto)
        {
            var created = _genreRepo.Create(new Genre { Name = dto.Name });
            return MapDto(created);
        }

        public GenreDto? Update(int id, GenreUpdateDto dto)
        {
            var genre = _genreRepo.GetById(id);
            if (genre == null) return null;

            genre.Name = dto.Name;
            var updated = _genreRepo.Update(genre);
            return MapDto(updated);
        }

        public bool Delete(int id) => _genreRepo.Delete(id);
    }
}
