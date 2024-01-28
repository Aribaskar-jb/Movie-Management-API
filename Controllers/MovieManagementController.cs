using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieManagementController : ControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var data = new List<Movie>();
            string jsonContent = System.IO.File.ReadAllText("C:\\Users\\jbari\\Documents\\Kovai.co\\Movie Management API\\Data\\Move.json");

            data = JsonConvert.DeserializeObject<List<Movie>>(jsonContent);

            return Ok(data);
        }
        
        [HttpPost]
        public IActionResult AddMovie( Movie newMovie)
        {
            if (newMovie == null)
            {
                return BadRequest("Invalid movie data.");
            }

            List<Movie> movies = LoadMovies();
            movies.Add(newMovie);
            SaveMovies(movies);

            return Ok("Added");
        }
        private List<Movie> LoadMovies()
        {
            List<Movie> movies;
            try
            {
                string jsonData = System.IO.File.ReadAllText("C:\\Users\\jbari\\Documents\\Kovai.co\\Movie Management API\\Data\\Move.json");
                movies = JsonConvert.DeserializeObject<List<Movie>>(jsonData);
            }
            catch (Exception)
            {
                movies = new List<Movie>();
            }
            return movies;
        }
        private void SaveMovies(List<Movie> movies)
        {
            string jsonData = JsonConvert.SerializeObject(movies, Formatting.Indented);
            System.IO.File.WriteAllText("C:\\Users\\jbari\\Documents\\Kovai.co\\Movie Management API\\Data\\Move.json", jsonData);
        }
        [HttpPut]
        public IActionResult UpdateMovie(Movie Update)
        {
            if (Update == null)
            {
                return BadRequest("Invalid movie data.");
            }
            string title = Update.title;
            List<Movie> movies = LoadMovies();
            foreach (Movie movie in movies)
            {
                if (movie.title == title)
                {
                    movie.title = title;
                    movie.director = Update.director;
                    movie.genre = Update.genre;
                    movie.year = Update.year;
                }
            }
            SaveMovies(movies);
            return Ok("Updated Movie");
        }

        [HttpDelete]
        public IActionResult DeleteMovie(string Title)
        {
            if (Title == null)
            {
                return BadRequest("Invalid movie data.");
            }
            List<Movie> movies = LoadMovies();
            int Count = 0;
            bool temp = false;
            foreach (Movie movie in movies)
            {
                if(movie.title == Title)
                {
                    temp= true;
                    break;
                }
                Count++;
            }
            if (temp)
            {
                movies.RemoveAt(Count);
                SaveMovies(movies);
                return Ok("Delete Movie");
                
            }
            else
            {
                return Ok("Enter the valid Movie");
            }
        }
    }
}