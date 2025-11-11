using GameLibraryAPI.Models.DTO.ReviewDTO;

namespace GameLibraryAPI.Services
{
    public interface IReviewService
    {
        IEnumerable<ReviewDto> GetAll();
        ReviewDto? GetById(int id);
        IEnumerable<ReviewDto> GetByGame(int gameId);
        IEnumerable<ReviewDto> GetByUser(int userId);
        ReviewDto Create(ReviewCreateDto dto);
        ReviewDto? Update(int id, ReviewUpdateDto dto);
        bool Delete(int id);
    }
}
