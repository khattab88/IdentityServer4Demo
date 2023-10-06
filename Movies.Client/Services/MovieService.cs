using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class MovieService : IMovieService
    {
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            // 1. get token from identity server
            // 2. send request to protected api
            // 3. deserialize response 

            // 1.1 retrieve our client credientials
            var apiClientCredentials = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5005/connect/token",
                ClientId = "movieClient",
                ClientSecret = "secret",
                Scope = "movieAPI"
            };

            var client = new HttpClient();

            // 1.2 check if identity server discovery document is available
            var discov = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
            if (discov.IsError)
            {
                return null; // throw 500 error
            }

            // 1.3 authenticate and get access token 
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
            if (tokenResponse.IsError)
            {
                return null;
            }


            // 2.1 create another http client for api
            var apiClient = new HttpClient();

            // 2.2 set access_token in request Authorization: Bearer <token>
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // 2.3 send request to api
            var apiResponse = await apiClient.GetAsync("https://localhost:5001/api/movies");
            apiResponse.EnsureSuccessStatusCode();

            var content = await apiResponse.Content.ReadAsStringAsync();


            // 3.1 deserialize response content
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(content);
            return movies;
        }


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
        
        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }
    }
}
