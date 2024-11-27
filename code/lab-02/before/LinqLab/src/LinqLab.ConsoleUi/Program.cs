// See https://aka.ms/new-console-template for more information
using LinqLab.Api;

Console.WriteLine("Hello, World!");


var reader = new MovieDataReader();

var movies = reader.GetMovies(1980);

foreach (var movie in movies)
{
    Console.WriteLine(movie.Title);
}