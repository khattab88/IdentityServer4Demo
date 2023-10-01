using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class MovieService : IMovieService
    {
        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Movie> GetMovie(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movies = new List<Movie>() 
            {
                new Movie
                {
                    Id = 1,
                    Title = "Test 1",
                    Genre = "Test 1",
                    ImageUrl = null,
                    Owner = null,
                    Rating = null,
                    ReleaseDate = DateTime.Now
                },
                new Movie
                {
                    Id = 2,
                    Title = "Test 2",
                    Genre = "Test 2",
                    ImageUrl = null,
                    Owner = null,
                    Rating = null,
                    ReleaseDate = DateTime.Now
                }
            };  

            return await Task.FromResult(movies);
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }
    }
}
