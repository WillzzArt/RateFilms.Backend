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
        private readonly IReviewRepository _reviewRepository;

        public CommentService(
            ICommentRepository commentRepository,
            IUserRepository userRepository,
            IFavoriteRepository favoriteRepository,
            IReviewRepository reviewRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsInFilm(Guid filmId, int countComm, string? username)
        {
            IEnumerable<Comment> comments;

            if (username != null)
            {
                var user = await _userRepository.FindUser(username);
                comments = await _commentRepository.GetCommentsInFilm(filmId, user?.Id);
            }
            else
            {
                comments = await _commentRepository.GetCommentsInFilm(filmId, null);
            }

            return comments.Take(countComm).Select(c => new CommentResponse(c));
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsInSerial(Guid serialId, int countComm, string? username)
        {
            IEnumerable<Comment> comments;

            if (username != null)
            {
                var user = await _userRepository.FindUser(username);
                comments = await _commentRepository.GetCommentsInSerial(serialId, user?.Id);
            }
            else
            {
                comments = await _commentRepository.GetCommentsInSerial(serialId, null);
            }

            return comments.Take(countComm).Select(c => new CommentResponse(c));
        }

        public async Task<IEnumerable<ReviewResponse>> GetUncheckedReviewsInMovie(Guid movieId, int count, bool isFilm, string? username)
        {
            IEnumerable<Review> review;

            if (username == null)
            {
                review = await _reviewRepository.GetUncheckedReview(movieId, isFilm,
                    x => new[] { ReviewStatus.Unpublished, ReviewStatus.Canсeled, ReviewStatus.Published }.Contains(x.Status));
            }
            else
            {
                var user = await _userRepository.FindUser(username);

                if (user == null) throw new ArgumentException(nameof(username));

                review = await _reviewRepository.GetUncheckedReview(movieId, isFilm,
                    x => new[] { ReviewStatus.Unsent, ReviewStatus.Canсeled, ReviewStatus.Published }.Contains(x.Status) 
                    && x.User.Id == user.Id);
            }

            return review.Select(r => new ReviewResponse(r));
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
            var review = await _reviewRepository.FindReviewById(reviewId, isFilm);

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
                    case ReviewStatus.Canсeled:
                        {
                            review.Status = ReviewStatus.Unsent;
                            break;
                        }
                }

                await _reviewRepository.SetNewReviewStatus(review.Id, review.Status);
            }
        }

        public async Task PublishReview(AdminNoteRequest adminNote, string username)
        {
            var user = await _userRepository.FindUser(username);

            if (user == null) throw new ArgumentException(nameof(username));

            if (user.Role == Role.Admin)
            {
                var review = await _reviewRepository.FindReviewById(adminNote.ReviewId, adminNote.IsFilm);

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
                                if (string.IsNullOrWhiteSpace(adminNote.Note)) throw new ArgumentNullException(nameof(adminNote.Note));

                                await _reviewRepository.CreateNoteToReview(user.Id, adminNote.ReviewId, adminNote.Note);

                                review.Status = ReviewStatus.Canсeled;
                            }

                            break;
                        }
                    case ReviewStatus.Canсeled:
                        {
                            await _reviewRepository.DeleteNoteToReview(user.Id, adminNote.ReviewId);
                            review.Status = ReviewStatus.Unpublished;
                            break;
                        }
                    case ReviewStatus.Published:
                        {
                            review.Status = ReviewStatus.Unpublished;
                            break;
                        }
                }

                await _reviewRepository.SetNewReviewStatus(review.Id, review.Status);
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
