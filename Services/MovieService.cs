using Newtonsoft.Json;
using otonoMovieAppTask.Entities;

namespace otonoMovieAppTask.Services
{
    public class MovieService : IMovieService
    {
        private readonly string _apiKey;
        private static List<string> searchHistory = new List<string>();
        // Get your API key from http://www.omdbapi.com

        public MovieService(IConfiguration configuration)
        {
            // Get the API key from appSettings.json via IConfiguration
            _apiKey = configuration.GetValue<string>("OmdbApi:ApiKey");
        }
        public async Task<Movie> MovieSearch(string movieTitle)
        {
            // Saving the search query into the search history
            searchHistory.Insert(0, movieTitle);
            if (searchHistory.Count > 5)
                searchHistory.RemoveAt(5);

            // A call to OMDB API
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"http://www.omdbapi.com/?apikey={_apiKey}&t={movieTitle}");
                var apiResult = JsonConvert.DeserializeObject<Movie>(response);
                return apiResult;
            }
        }
        public async Task<IEnumerable<string>> GetSearchHistory()
        {
            if (searchHistory.Count < 1)
            {
                return null;
            }
            return searchHistory.Take(5);
        }


    }
}
