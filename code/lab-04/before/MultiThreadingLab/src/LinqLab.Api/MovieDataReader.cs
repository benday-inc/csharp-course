using System.Text.Json;

namespace LinqLab.Api;

public class MovieDataReader
{
    private string FindDataDirectory()
    {
        var assemblyLocation = typeof(MovieDataReader).Assembly.Location ??
            throw new InvalidOperationException("could not find assembly location");

        var directory = Path.GetDirectoryName(assemblyLocation) ??
            throw new InvalidOperationException("Could not get directory from assembly location.");

        var directoryToCheck = Path.Combine(directory, "data");

        Console.WriteLine($"{nameof(FindDataDirectory)}(): Checking {directoryToCheck}...");

        while (!Directory.Exists(directoryToCheck))
        {
            directoryToCheck = Path.GetFullPath(Path.Combine(directory, ".."));
            if (directoryToCheck.Length < 5)
            {
                throw new DirectoryNotFoundException("Could not find the data directory.");
            }

            Console.WriteLine($"{nameof(FindDataDirectory)}(): Checking {directoryToCheck}...");
        }

        Console.WriteLine($"{nameof(FindDataDirectory)}(): Found data directory at {directoryToCheck}");

        return directoryToCheck;
    }

    private bool IsValidDecade(int year)
    {
        var validValues = new List<int>();

        validValues.Add(1970);
        validValues.Add(1980);
        validValues.Add(1990);
        validValues.Add(2000);
        validValues.Add(2010);
        validValues.Add(2020);

        var returnValue = validValues.Contains(year);

        return returnValue;
    }

    /// <summary>
    /// Returns a list of movies from the movies.json file
    /// </summary>
    /// <param name="decades">List of decades to load</param>
    /// <returns></returns>
    public List<Movie> GetMovies(params int[] decades)
    {
        if (decades.Length == 0)
        {
            throw new ArgumentException("At least one decade must be provided.");
        }
        else
        {
            foreach (var decade in decades)
            {
                if (!IsValidDecade(decade))
                {
                    throw new ArgumentException($"Invalid decade provided: '{decade}'");
                }
            }

            var sourceDir = FindDataDirectory();

            var movies = new List<Movie>();

            foreach (var decade in decades)
            {
                var json = File.ReadAllText(Path.Combine(sourceDir, $"movies-{decade}s.json"));

                var decadeMovies = JsonSerializer.Deserialize<List<Movie>>(json);

                if (decadeMovies != null)
                {
                    movies.AddRange(decadeMovies);
                }
            }

            return movies;
        }        
    }
}

