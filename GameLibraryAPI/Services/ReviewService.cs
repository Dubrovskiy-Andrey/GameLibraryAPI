using GameLibraryAPI.Models;
using GameLibraryAPI.Models.DTO.ReviewDTO;
using GameLibraryAPI.Repositories;
using System;

namespace GameLibraryAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IGameRepository _gameRepo;
        private readonly IUserRepository _userRepo;

        public ReviewService(IReviewRepository reviewRepo, IGameRepository gameRepo, IUserRepository userRepo)
        {
            _reviewRepo = reviewRepo;
            _gameRepo = gameRepo;
            _userRepo = userRepo;
        }

        private static ReviewDto MapDto(Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                GameTitle = review.Game?.Title ?? string.Empty,
                Username = review.User?.Login ?? string.Empty
            };
        }

        public IEnumerable<ReviewDto> GetAll()
        {
            var reviews = _reviewRepo.GetAll();
            return reviews.Select(MapDto);
        }

        public ReviewDto? GetById(int id)
        {
            var review = _reviewRepo.GetById(id);
            return review == null ? null : MapDto(review);
        }

        public IEnumerable<ReviewDto> GetByGame(int gameId)
        {
            var reviews = _reviewRepo.GetReviewsByGame(gameId);
            return reviews.Select(MapDto);
        }

        public IEnumerable<ReviewDto> GetByUser(int userId)
        {
            var reviews = _reviewRepo.GetReviewsByUser(userId);
            return reviews.Select(MapDto);
        }

        public ReviewDto Create(ReviewCreateDto dto)
        {
            if (!_gameRepo.Exists(dto.GameId))
                throw new ArgumentException("Игра с указанным Id не найдена.");

            if (!_userRepo.Exists(dto.UserId))
                throw new ArgumentException("Пользователь с указанным Id не найден.");


            var newReview = new Review
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.Now,
                GameId = dto.GameId,
                UserId = dto.UserId
            };

            var created = _reviewRepo.Create(newReview);
            return MapDto(created);
        }

        public ReviewDto? Update(int id, ReviewUpdateDto dto)
        {
            var review = _reviewRepo.GetById(id);
            if (review == null) return null;

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;

            var updated = _reviewRepo.Update(review);
            return MapDto(updated);
        }

        public bool Delete(int id) => _reviewRepo.Delete(id);
    }
}
