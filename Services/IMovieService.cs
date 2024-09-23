using otonoMovieAppTask.Entities;

namespace otonoMovieAppTask.Services
{
    public interface IMovieService
    {
        Task<Movie> MovieSearch(string movieTitle);
        Task<IEnumerable<string>> GetSearchHistory();
    }
}
