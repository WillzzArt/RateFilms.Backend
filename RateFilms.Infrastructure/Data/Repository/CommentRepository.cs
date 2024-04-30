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

        public async Task<IEnumerable<Comment>> GetCommentsInFilm(Guid filmId, Guid? userId)
        {
            var commentsInFav = await _context.FavoriteFilms.Where(fav => fav.FilmId == filmId && fav.Comments != null)
                .Select(x => new
                {
                    comments = x.Comments!.Select(c => new CommentDbModel
                    {
                        Id = c.Comment.Id,
                        Date = c.Comment.Date,
                        IsEdit = c.Comment.IsEdit,
                        Text = c.Comment.Text,
                        Users = c.Comment.Users.ToList()
                    }).ToList(),
                    user = new UserDbModel
                    {
                        Id = x.User.Id,
                        UserName = x.User.UserName,
                        Image = x.User.Image

                    }
                }).ToListAsync();

            var comment = from commInFav in commentsInFav
                          from comm in commInFav.comments
                          where comm.Status == ReviewStatus.None
                          let isLiked = comm.Users.Any(u => u.UserId == userId)
                          select new Comment
                          {
                              Id = comm.Id,
                              IsEdit = comm.IsEdit,
                              User = UserConvertor.UserDbConvertUserDomain(commInFav.user),
                              Date = comm.Date,
                              Text = comm.Text,
                              CountLike = comm.Users?.Count() ?? 0,
                              IsLiked = isLiked
                          };

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsInSerial(Guid serialId, Guid? userId)
        {
            var commentsInFav = await _context.FavoriteSerials.Where(fav => fav.SerialId == serialId && fav.Comments != null)
                .Select(x => new
                {
                    comments = x.Comments!.Select(c => new CommentDbModel
                    {
                        Id = c.Comment.Id,
                        Date = c.Comment.Date,
                        IsEdit = c.Comment.IsEdit,
                        Text = c.Comment.Text,
                        Users = c.Comment.Users.ToList()
                    }).ToList(),
                    user = new UserDbModel
                    {
                        Id = x.User.Id,
                        UserName = x.User.UserName,
                        Image = x.User.Image
                    }
                }).ToListAsync();

            var comment = from commInFav in commentsInFav
                          from comm in commInFav.comments
                          where comm.Status == ReviewStatus.None
                          let isLiked = comm.Users.Any(u => u.UserId == userId)
                          select new Comment
                          {
                              Id = comm.Id,
                              IsEdit = comm.IsEdit,
                              User = UserConvertor.UserDbConvertUserDomain(commInFav.user),
                              Date = comm.Date,
                              Text = comm.Text,
                              CountLike = comm.Users?.Count() ?? 0,
                              IsLiked = isLiked
                          };

            return comment;
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
