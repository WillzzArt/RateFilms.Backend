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
                Text = commentDb.Text
            };

            await _context.Comment.AddAsync(comment);

            if (commentDb.CommentInFilm != null)
            {
                var commentInFilm = new CommentInFilmDbModel { Comment = comment };

                await _context.CommentInFilm.AddAsync(commentInFilm);

                if (commentDb.CommentInFilm.Favorite != null)
                {
                    commentInFilm.Favorite = commentDb.CommentInFilm.Favorite;
                }
                else
                {
                    var favoriteFilm = new FavoriteFilmDbModel
                    {
                        UserId = userId,
                        FilmId = movieId
                    };

                    await _context.FavoriteFilms.AddAsync(favoriteFilm);
                    commentInFilm.Favorite = favoriteFilm;
                }

            }
            else if (commentDb.CommentInSerial != null)
            {
                var commentInSerial = new CommentInSerialDbModel { Comment = comment };

                await _context.CommentInSerial.AddAsync(commentInSerial);

                if (commentDb.CommentInSerial.Favorite != null)
                {
                    commentInSerial.Favorite = commentDb.CommentInSerial.Favorite;
                }
                else
                {
                    var favoriteSerial = new FavoriteSerialDbModel
                    {
                        UserId = userId,
                        SerialId = movieId
                    };

                    await _context.FavoriteSerials.AddAsync(favoriteSerial);
                    commentInSerial.Favorite = favoriteSerial;
                }
            }
            else
            {
                throw new ArgumentException(nameof(commentDb));
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsInFilm(Guid FilmId, int count)
        {
            var commentsInFav = await _context.FavoriteFilms.Where(fav => fav.FilmId == FilmId && fav.Comments != null)
                .Select(x => new { comments = x.Comments!.ToList(), user = x.User }).ToListAsync();

            var comment = from commInFav in commentsInFav
                          from comm in commInFav.comments
                          let c = _context.Comment.Find(comm.CommentId)
                          select new Comment
                          {
                              Id = c.Id,
                              IsEdit = c.IsEdit,
                              User = UserConvertor.UserDbConvertUserDomain(commInFav.user),
                              Date = c.Date,
                              Text = c.Text,
                              CountLike = c.Users?.Count() ?? 0
                          };

            return comment.Take(count);
        }

        public async Task<IEnumerable<Comment>> GetCommentsInSerial(Guid SerialId, int count)
        {
            var commentsInFav = await _context.FavoriteSerials.Where(fav => fav.SerialId == SerialId && fav.Comments != null)
                .Select(x => new { comments = x.Comments!.ToList(), user = x.User }).ToListAsync();

            var comment = from commInFav in commentsInFav
                          from comm in commInFav.comments
                          let c = _context.Comment.Find(comm.CommentId)
                          select new Comment
                          {
                              Id = c.Id,
                              IsEdit = c.IsEdit,
                              User = UserConvertor.UserDbConvertUserDomain(commInFav.user),
                              Date = c.Date,
                              Text = c.Text,
                              CountLike = c.Users?.Count() ?? 0
                          };

            return comment.Take(count);
        }
    }
}
