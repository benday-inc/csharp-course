using Benday.CommandsFramework;

namespace LinqLab.Api.Commands;

[Command(Name = "actor",
    Description = "Get info about an actor")]
public class GetActorCommand : SynchronousCommand
{
    public GetActorCommand(CommandExecutionInfo info, ITextOutputProvider outputProvider) :
        base(info, outputProvider)
    {

    }

    public override ArgumentCollection GetArguments()
    {
        var args = new ArgumentCollection();

        args.AddString("decades")
            .AsNotRequired()
            .WithDescription(
                "Decades to load separated by comma. Defaults to '2010,2020'")
            .WithDefaultValue("2010,2020");

        args.AddString("name")
            .AsRequired()
            .WithDescription(
                "Name of the actor to get info about.");

        return args;
    }

    protected override void OnExecute()
    {
        var decadesAsString = Arguments.GetStringValue("decades");
        var decades = Utilities.CommaSeparatedValuesToIntArray(decadesAsString);

        var actorName = Arguments.GetStringValue("name");

        var reader = new MovieDataReader();

        var movies = reader.GetMovies(decades);

        WriteLine($"Found {movies.Count} movies.");

        var moviesWithActor = GetMoviesWithActor(movies, actorName);

        var mostCommonCoStars = GetMostCommonCoStars(moviesWithActor, actorName);

        var mostCommonGenres = GetMostCommonGenres(moviesWithActor);

        var moviesPerYear = GetMoviesPerYear(moviesWithActor);

        WriteLine();

        WriteLine($"Actor: {actorName}");

        WriteLine("Most common co-stars:");
        WriteLine("Name | # of Movies");
        foreach (var item in mostCommonCoStars.Take(5))
        {
            WriteLine($"{item.Name} ({item.MovieCount})");
        }

        WriteLine();

        WriteLine("Most common genres:");
        foreach (var item in mostCommonGenres.Take(5))
        {
            WriteLine(item);
        }

        WriteLine();
        
        WriteLine("Movies per year:");
        WriteLine("Year | # of Movies");
        foreach (var item in moviesPerYear)
        {
            WriteLine($"{item.Year} | {item.MovieCount}");
        }
    }

    private IEnumerable<Movie> GetMoviesWithActor(
        List<Movie> movies, string actorName)
    {
        var moviesWithActor = movies.Where(
            m =>
            {
                var found = false;
                foreach (var a in m.Cast)
                {
                    if (a.ContainsCaseInsensitive(actorName))
                    {
                        found = true;
                        break;
                    }
                }
                return found;
            }).ToList();

        return moviesWithActor;
    }

    private IEnumerable<ActorInfo> GetMostCommonCoStars(
        IEnumerable<Movie> moviesWithActor, string actorName)
    {
        var coStars = new Dictionary<string, ActorInfo>();

        foreach (var movie in moviesWithActor)
        {
            foreach (var actor in movie.Cast)
            {
                var toLower = actor.ToLower();

                if (coStars.ContainsKey(toLower) == true)
                {
                    coStars[toLower].MovieCount++;
                }
                else
                {
                    var info = actor.ToActor();
                    info.MovieCount = 1;
                    coStars.Add(toLower, info);
                }
            }
        }

        var coStarsSortedByMovieCountDescending =
            coStars.Values
                .Where(a => a.Name.EqualsCaseInsensitive(actorName) == false)
                .OrderByDescending(a => a.MovieCount)
                .ToList();

        return coStarsSortedByMovieCountDescending;
    }

    private IEnumerable<string> GetMostCommonGenres(IEnumerable<Movie> moviesWithActor)
    {
        var genres = new Dictionary<string, int>();

        foreach (var movie in moviesWithActor)
        {
            foreach (var genre in movie.Genres)
            {
                var toLower = genre.ToLower();

                if (genres.ContainsKey(toLower) == true)
                {
                    genres[toLower]++;
                }
                else
                {
                    genres.Add(toLower, 1);
                }
            }
        }

        var genresSortedByCountDescending =
            genres.OrderByDescending(g => g.Value)
                .Select(g => g.Key)
                .ToList();

        return genresSortedByCountDescending;
    }

    private IEnumerable<MovieCountByYear> GetMoviesPerYear(IEnumerable<Movie> moviesWithActor)
    {
        // group movies by year
        var moviesByYear = new Dictionary<int, MovieCountByYear>();

        foreach (var movie in moviesWithActor)
        {
            var year = movie.Year;
            if (moviesByYear.ContainsKey(year) == true)
            {
                moviesByYear[year].MovieCount++;
            }
            else
            {
                var info = new MovieCountByYear();
                info.Year = year;
                info.MovieCount = 1;
                moviesByYear.Add(year, info);
            }
        }

        // sort by year

        var moviesByYearSorted = moviesByYear.Values
            .OrderBy(m => m.Year)
            .ToList();

        return moviesByYearSorted;
    }
}
