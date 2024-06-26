﻿using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class CommentDbModel: IEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsEdit { get; set; }
        public ReviewStatus Status { get; set; }
        public IEnumerable<CommentUserDbModel> Users { get; set; }
        public CommentInFilmDbModel? CommentInFilm { get; set; }
        public CommentInSerialDbModel? CommentInSerial { get; set; }
        public AdminNoteDbModel? AdminNote { get; set; }
    }
}
