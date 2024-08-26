using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCommentAsync(CommentDbModel commentDb, Guid userId, Guid movieId)
        {
            var comment = new CommentDbModel
            {
                Date = DateTimeOffset.UtcNow,
                IsEdit = false,
                Text = commentDb.Text,
                Status = commentDb.Status
            };

            await _context.Comment.AddAsync(comment);

            if (commentDb.Favorite != null)
            {
                comment.Favorite = commentDb.Favorite;
            }
            else
            {
                var favoriteMovie = new FavoriteMovieDbModel
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    MovieId = movieId,
                    Score = 0
                };

                await _context.FavoriteMovie.AddAsync(favoriteMovie);
                comment.Favorite = favoriteMovie;
            }

            await _context.SaveChangesAsync();

        }

        public async Task DeleteComment(Guid commentId)
        {
            if (await _context.Comment.FirstOrDefaultAsync(c => c.Id == commentId) != null)
            {
                var commWithEntity = await _context.Comment.Select(c => new
                {
                    comm = c,
                    adminNote = c.AdminNote,
                    likes = c.Users.ToList()

                }).FirstAsync(c => c.comm.Id == commentId);


                if (commWithEntity.adminNote != null)
                {
                    _context.Remove(commWithEntity.adminNote);
                }

                if (commWithEntity.likes.Any())
                {
                    _context.RemoveRange(commWithEntity.likes);
                }

                _context.Remove(commWithEntity.comm);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentsInMovie(Guid movieId, Guid? userId)
        {
            var commentsInFav = _context.FavoriteMovie.Where(fav => fav.MovieId == movieId && fav.Comments != null);

            var commentRes = from commInFav in commentsInFav.Include(c => c.User).ThenInclude(u => u.Image)
                             from comm in commInFav.Comments!
                             where comm.Status == ReviewStatus.None
                             let isLiked = comm.Users.Any(u => u.UserId == userId)
                             select new Comment
                             {
                                 Id = comm.Id,
                                 IsEdit = comm.IsEdit,
                                 User = UserConvertor.UserDbConvertUserDomain(commInFav.User),
                                 Date = comm.Date,
                                 Text = comm.Text,
                                 CountLike = comm.Users.Count(),
                                 IsLiked = isLiked
                             };

            return commentRes;
        }

        public async Task<bool> SetLikedComment(Guid commentId, Guid userId)
        {
            var commExist = await _context.Comment.AnyAsync(c => c.Id == commentId);
            if (!commExist) return false;

            var comment = await _context.UserComment
                .FirstOrDefaultAsync(c => c.CommentId == commentId && c.UserId == userId);

            if (comment != null)
            {
                _context.UserComment.Remove(comment);
            }
            else
            {
                _context.UserComment.Add(new CommentUserDbModel { CommentId = commentId, UserId = userId });
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
