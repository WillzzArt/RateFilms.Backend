using RateFilms.Common.Helpers;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Application.Services.Movies
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public CommentService(
            ICommentRepository commentRepository,
            IUserRepository userRepository,
            IFavoriteRepository favoriteRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsInFilm(Guid filmId, int count, string? username)
        {
            IEnumerable<Comment> comments;
            if (username != null)
            {
                var user = await _userRepository.FindUser(username);
                comments = await _commentRepository.GetCommentsInFilm(filmId, count, user?.Id);
            }
            else
            {
                comments = await _commentRepository.GetCommentsInFilm(filmId, count, null);
            }


            return comments.Where(c => c.Status == ReviewStatus.None).Select(c => new CommentResponse(c));
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsInSerial(Guid filmId, int count, string? username)
        {
            IEnumerable<Comment> comments;
            if (username != null)
            {
                var user = await _userRepository.FindUser(username);
                comments = await _commentRepository.GetCommentsInSerial(filmId, count, user?.Id);
            }
            else
            {
                comments = await _commentRepository.GetCommentsInSerial(filmId, count, null);
            }

            return comments.Where(c => c.Status == ReviewStatus.None).Select(c => new CommentResponse(c));
        }

        public async Task CreateComment(CommentRequest commentRequest, string username, bool isFilm)
        {
            var user = await _userRepository.FindUser(username);
            if (user == null) throw new ArgumentException(nameof(username));

            var commentDb = new CommentDbModel
            {
                Text = commentRequest.CommentText,
                Status = commentRequest.Status.ToEnum(ReviewStatus.None)
            };

            if (isFilm)
            {
                commentDb.CommentInFilm = new CommentInFilmDbModel
                {
                    Favorite = await _favoriteRepository.FindFavoriteFilm(commentRequest.MovieId, user.Id)
                };
            }
            else
            {
                commentDb.CommentInSerial = new CommentInSerialDbModel
                {
                    Favorite = await _favoriteRepository.FindFavoriteSerial(commentRequest.MovieId, user.Id)
                };
            }

            await _commentRepository.CreateCommentAsync(commentDb, user.Id, commentRequest.MovieId);
        }

        public async Task PublishReview(Guid reviewId, string username, bool isFilm, bool isPublish = false)
        {
            var review = await _commentRepository.FindCommentById(reviewId, isFilm);

            var user = await _userRepository.FindUser(username);

            if (user == null) throw new ArgumentException(nameof(username));

            switch (review.Status)
            {
                case ReviewStatus.None:
                    {
                        throw new ArgumentException(nameof(reviewId));
                    }
                case ReviewStatus.Unsent:
                    {
                        if (review.User.Id == user.Id)
                            review.Status = ReviewStatus.Unpublished;
                        break;
                    }
                case ReviewStatus.Unpublished:
                    {
                        if (user.Role == Role.Admin)
                        {
                            if (isPublish)
                                review.Status = ReviewStatus.Published;
                            else
                                review.Status = ReviewStatus.Canseled;
                        }

                        break;
                    }
                case ReviewStatus.Canseled:
                    {
                        if (review.User.Id == user.Id)
                            review.Status = ReviewStatus.Unsent;
                        break;
                    }
            }

            await _commentRepository.SetNewReviewStatus(review);
        }

        public Task DeleteComment(CommentRequest commentRequest, string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateComment(CommentRequest commentRequest, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangeLikeOnComment(Guid commentId, string username)
        {
            var user = await _userRepository.FindUser(username);

            if (user == null) throw new ArgumentException(nameof(username));

            return await _commentRepository.SetLikedComment(commentId, user.Id);
        }
    }
}
