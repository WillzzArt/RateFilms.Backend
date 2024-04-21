using RateFilms.Common.Helpers;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;
using System.Xml.Linq;

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

            return comments.Where(c => c.Status == ReviewStatus.None || c.Status == ReviewStatus.Published)
                .Select(c => new CommentResponse(c));
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsInSerial(Guid serialId, int count, string? username)
        {
            IEnumerable<Comment> comments;
            if (username != null)
            {
                var user = await _userRepository.FindUser(username);
                comments = await _commentRepository.GetCommentsInSerial(serialId, count, user?.Id);
            }
            else
            {
                comments = await _commentRepository.GetCommentsInSerial(serialId, count, null);
            }

            return comments.Where(c => c.Status == ReviewStatus.None || c.Status == ReviewStatus.Published)
                .Select(c => new CommentResponse(c));
        }

        public async Task<IEnumerable<CommentResponse>> GetReviewsInMovie(Guid movieId, int count, string status, bool isFilm, string? username = null)
        {
            IEnumerable<Comment> review;

            if (isFilm)
                review = await _commentRepository.GetCommentsInFilm(movieId, count, null);
            else
                review = await _commentRepository.GetCommentsInSerial(movieId, count, null);

            if (username == null)
            {
                return review.Where(c => c.Status == status.ToEnum(ReviewStatus.None)).Select(c => new CommentResponse(c));
            }
            else
            {
                var user = await _userRepository.FindUser(username);

                if (user == null) throw new ArgumentException(nameof(username));

                return review.Where(c => (c.Status == ReviewStatus.Unsent || c.Status == ReviewStatus.Canseled) && c.User.Id == user.Id)
                .Select(c => new CommentResponse(c));
            }
            
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

        public async Task ChangeReviewStatus(Guid reviewId, string username, bool isFilm)
        {
            var review = await _commentRepository.FindCommentById(reviewId, isFilm);

            var user = await _userRepository.FindUser(username);

            if (user == null) throw new ArgumentException(nameof(username));

            if (review.User.Id == user.Id)
            {
                switch (review.Status)
                {
                    case ReviewStatus.None:
                        {
                            throw new ArgumentException(nameof(reviewId));
                        }
                    case ReviewStatus.Unsent:
                        {
                            review.Status = ReviewStatus.Unpublished;
                            break;
                        }
                    case ReviewStatus.Canseled:
                        {
                            review.Status = ReviewStatus.Unsent;
                            break;
                        }
                }

                await _commentRepository.SetNewReviewStatus(review);
            }
        }

        public async Task PublishReview(AdminNote adminNote, string username)
        {
            var user = await _userRepository.FindUser(username);

            if (user == null) throw new ArgumentException(nameof(username));

            if (user.Role == Role.Admin)
            {
                var review = await _commentRepository.FindCommentById(adminNote.ReviewId, adminNote.IsFilm);

                switch (review.Status)
                {
                    case ReviewStatus.None:
                        {
                            throw new ArgumentException(nameof(adminNote.ReviewId));
                        }
                    case ReviewStatus.Unpublished:
                        {
                            if (adminNote.IsPublish)
                                review.Status = ReviewStatus.Published;
                            else
                            {
                                if (string.IsNullOrWhiteSpace(adminNote.Note)) throw new ArgumentNullException(adminNote.Note);

                                await _commentRepository.CreateNoteToReview(user.Id, adminNote.ReviewId, adminNote.Note);

                                review.Status = ReviewStatus.Canseled;
                            }

                            break;
                        }
                    case ReviewStatus.Canseled:
                        {
                            await _commentRepository.DeleteNoteToReview(user.Id, adminNote.ReviewId);
                            review.Status = ReviewStatus.Published;
                            break;
                        }
                    case ReviewStatus.Published:
                        {
                            if (string.IsNullOrWhiteSpace(adminNote.Note)) throw new ArgumentNullException(adminNote.Note);

                            await _commentRepository.CreateNoteToReview(user.Id, adminNote.ReviewId, adminNote.Note);

                            review.Status = ReviewStatus.Canseled;
                            break;
                        }
                }

                await _commentRepository.SetNewReviewStatus(review);
            }
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
