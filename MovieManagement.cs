using System.Collections.Generic;

namespace Movie_Management_API
{
    public class Movie()
    {
        public string title { get; set; }
        public string genre { get; set; }
        public int year { get; set; }
        public string director { get; set; }
    }
    public class MovieManagement
    {
       
        private List<Movie> movies = new List<Movie>
        {
            new Movie
            {
                title = "The Shawshank Redemption",
                genre = "Drama",
                year = 1994,
                director = "Frank Darabont"
            }
        };
        public List<Movie> GetMovieData()
        {
            return movies;
        }
        public List<Movie> AddMovie(Movie movie)
        {
            movies.Add(movie);
            return movies;
        }

    }
}
