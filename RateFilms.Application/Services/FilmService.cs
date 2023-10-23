using RateFilms.Domain.Models;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Application.Services
{
    public class FilmService: IFilmService
    {
        private readonly IBaseRepository _repository;
        private readonly IActorRepository _actorRepository;

        public FilmService(IBaseRepository baseRepository, IActorRepository actorRepository)
        {
            _repository = baseRepository;
            _actorRepository = actorRepository;
        }

        public async Task CreateFilmsAsync(Film film)
        {
            await _repository.CreateAsync(film);
            /*var actors = film.Actors;
            
            var genres = film.Genre;

            var img = film.Images;

            if (actors != null)
            {

                foreach (var actor in actors)
                {
                    var image = actor.Image;
                    if (image != null)
                        await _repository.CreateAsync(image);

                    await _repository.CreateAsync(actor);
                    
                }
                
            }

            if (genres != null)
                foreach (var genre in genres)
                    await _repository.CreateAsync(genre);

            if (img != null)
                foreach(var image in img)
                    await _repository.CreateAsync(image);

            await _repository.CreateAsync(film);*/
        }

        public async Task<IEnumerable<Film>> GetFilms()
        {
            
            return _actorRepository.FindActorByFilmId();
        }
    }
}
