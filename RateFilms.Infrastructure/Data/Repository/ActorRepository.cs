using RateFilms.Domain.Models;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class ActorRepository : IActorRepository
    {
        private ApplicationDbContext _context;

        public ActorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Film?> FindActorByFilmId()
        {
            var films = _context.Set<Film>().ToList();
            foreach (var film in films)
            {
                _context.Entry(film).Collection(f => f.Actors).Load();
                _context.Entry(film).Collection(f => f.Genre).Load();
                _context.Entry(film).Collection(f => f.Images).Load();
            }
            
            return films;
        }
    }
}
