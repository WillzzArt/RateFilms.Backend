using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Movies;
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
            

            return comments.Select(c => new CommentResponse(c));
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

            return comments.Select(c => new CommentResponse(c));
        }

        public async Task CreateCommentInFilm(CommentRequest commentRequest, string username)
        {
            var user = await _userRepository.FindUser(username);
            if (user == null) throw new ArgumentException(nameof(username));

            var favorite = await _favoriteRepository.FindFavoriteFilm(commentRequest.MovieId, user.Id);

            var commentDb = new CommentDbModel
            {
                Text = commentRequest.CommentText,
                CommentInFilm = new CommentInFilmDbModel
                {
                    Favorite = favorite
                }
            };

            await _commentRepository.CreateCommentAsync(commentDb, user.Id, commentRequest.MovieId);
        }

        public async Task CreateCommentInSerial(CommentRequest commentRequest, string username)
        {
            var user = await _userRepository.FindUser(username);
            if (user == null) throw new ArgumentException(nameof(username));

            var favorite = await _favoriteRepository.FindFavoriteSerial(commentRequest.MovieId, user.Id);

            var commentDb = new CommentDbModel
            {
                Text = commentRequest.CommentText,
                CommentInSerial = new CommentInSerialDbModel
                {
                    Favorite = favorite
                }
            };

            await _commentRepository.CreateCommentAsync(commentDb, user.Id, commentRequest.MovieId);
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
