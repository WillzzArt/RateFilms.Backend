using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Review> FindReviewById(Guid reviewId)
        {
            var comment = new CommentDbModel();
            var review = new Review
            {
                User = new User(),
                Note = new AdminNote()
            };

            if (_context.Comment.FirstOrDefault(x => x.Id == reviewId && x.CommentInFilm != null) != null)
            {
                comment = await _context.Comment
                    .Include(c => c.CommentInFilm)
                        .ThenInclude(c => c.Favorite)
                    .FirstOrDefaultAsync(c => c.Id == reviewId);

                if (comment == null) throw new ArgumentException(nameof(reviewId));

                review.User.Id = comment.CommentInFilm!.Favorite!.UserId;
            }
            else
            {
                comment = await _context.Comment
                   .Include(c => c.CommentInSerial)
                       .ThenInclude(c => c.Favorite)
                   .FirstOrDefaultAsync(c => c.Id == reviewId);

                if (comment == null) throw new ArgumentException(nameof(reviewId));

                review.User.Id = comment.CommentInSerial!.Favorite!.UserId;
            }

            review.Id = comment.Id;
            review.Text = comment.Text;
            review.Date = comment.Date;
            review.Status = comment.Status;

            return review;
        }

        public async Task SetNewReviewStatus(Review review)
        {
            var reviewData = await _context.Comment.FirstOrDefaultAsync(c => c.Id == review.Id);

            if (reviewData == null) throw new ArgumentException(nameof(review));

            reviewData.Status = review.Status;
            reviewData.Date = review.Date;


            await _context.SaveChangesAsync();
        }

        public async Task CreateNoteToReview(Guid userId, Guid reviewId, string note)
        {
            var noteToReview = new AdminNoteDbModel
            {
                UserId = userId,
                ReviewId = reviewId,
                Note = note,
                Date = DateTime.UtcNow
            };

            await _context.AdminNote.AddAsync(noteToReview);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteToReview(Guid userId, Guid reviewId)
        {
            var review = await _context.AdminNote.FirstOrDefaultAsync(x => x.UserId == userId && x.ReviewId == reviewId);

            if (review != null)
            {
                _context.AdminNote.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Review>> GetReviewByStatus(Guid moviewId, Guid? userId, bool isFilm, Func<Review, bool> predicate)
        {
            IQueryable<CommentDbModel> review;

            if (isFilm)
            {
                review = _context.Comment.Where(c => c.CommentInFilm!.Favorite!.FilmId == moviewId);
            }
            else
            {
                review = _context.Comment.Where(c => c.CommentInSerial!.Favorite!.SerialId == moviewId);
            }

            var result = await review.Select(r => new Review
            {
                Id = r.Id,
                Date = r.Date,
                Status = r.Status,
                Text = r.Text,
                User = r.CommentInFilm != null
                        ? UserConvertor.UserDbConvertUserDomain(r.CommentInFilm!.Favorite!.User)
                        : UserConvertor.UserDbConvertUserDomain(r.CommentInSerial!.Favorite!.User),
                CountLike = r.Users.Count(),
                IsLiked = r.Users.Any(u => u.UserId == userId),
                Score = (int)(r.CommentInFilm != null
                        ? r.CommentInFilm!.Favorite!.Score!
                        : r.CommentInSerial!.Favorite!.Score!),

                Note = r.AdminNote != null ? new AdminNote
                {
                    Note = r.AdminNote.Note,
                    Date = r.AdminNote.Date,
                    user = UserConvertor.UserDbConvertUserDomain(r.AdminNote.User)
                } : null
            }).ToListAsync();

            return result.Where(predicate);
        }

        public async Task<bool> UpdateReview(Guid reviewId, string text)
        {
            var reviewData = await _context.Comment.FirstOrDefaultAsync(r => r.Id == reviewId);

            if (reviewData != null && reviewData.Status == ReviewStatus.Unsent)
            {
                reviewData.Text = text;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
