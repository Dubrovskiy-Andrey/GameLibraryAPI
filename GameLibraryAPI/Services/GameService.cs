using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.GameDTO;
using GameLibraryAPI.Repositories;

namespace GameLibraryAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IPlatformRepository _platformRepo;

        public GameService(IGameRepository gameRepo, IGenreRepository genreRepo, IPlatformRepository platformRepo)
        {
            _gameRepo = gameRepo;
            _genreRepo = genreRepo;
            _platformRepo = platformRepo;
        }

        private static GameDto MapDto(Game game)
        {
            return new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Tag = game.Tag,
                GenreName = game.Genre?.Name ?? string.Empty,
                PlatformName = game.Platform?.Name ?? string.Empty,
                ReleaseDate = game.ReleaseDate
            };
        }

        public IEnumerable<GameDto> GetAll() => _gameRepo.GetAll().Select(MapDto);

        public GameDto? GetById(int id)
        {
            var game = _gameRepo.GetById(id);
            return game == null ? null : MapDto(game);
        }

        public GameDto Create(GameCreateDto dto)
        {
            if (!_genreRepo.Exists(dto.GenreId))
                throw new ArgumentException("Жанр с таким Id не найден");
            if (!_platformRepo.Exists(dto.PlatformId))
                throw new ArgumentException("Платформа с таким Id не найдена");

            var game = new Game
            {
                Title = dto.Title,
                Description = dto.Description,
                Tag = dto.Tag,
                ReleaseDate = dto.ReleaseDate,
                GenreId = dto.GenreId,
                PlatformId = dto.PlatformId
            };

            var created = _gameRepo.Create(game);
            return MapDto(created);
        }

        public GameDto? Update(int id, GameUpdateDto dto)
        {
            var game = _gameRepo.GetById(id);
            if (game == null) return null;

            if (!_genreRepo.Exists(dto.GenreId) || !_platformRepo.Exists(dto.PlatformId))
                throw new ArgumentException("Некорректный GenreId или PlatformId");

            game.Title = dto.Title;
            game.Description = dto.Description;
            game.Tag = dto.Tag;
            game.ReleaseDate = dto.ReleaseDate;
            game.GenreId = dto.GenreId;
            game.PlatformId = dto.PlatformId;

            var updated = _gameRepo.Update(game);
            return MapDto(updated);
        }

        public bool Delete(int id) => _gameRepo.Delete(id);
    }
}
