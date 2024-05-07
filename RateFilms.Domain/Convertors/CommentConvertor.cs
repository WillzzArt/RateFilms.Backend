using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Convertors
{
    public static class CommentConvertor
    {
        public static Comment CommentDbConvertCommentDomain(CommentDbModel commentDb)
        {
            if (commentDb == null) throw new ArgumentNullException(nameof(commentDb));

            var comment = new Comment
            {
                Id = commentDb.Id,
                Text = commentDb.Text,
                Date = commentDb.Date,
                IsEdit = commentDb.IsEdit
            };

            return comment;
        }
    }
}
