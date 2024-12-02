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
        foreach (var item in mostCommonCoStars)
        {
            WriteLine($"{item.Name} ({item.MovieCount})");
        }

        WriteLine();

        WriteLine("Most common genres:");
        foreach (var item in mostCommonGenres)
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

    private IEnumerable<Movie> GetMoviesWithActor(List<Movie> movies, string actorName)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<ActorInfo> GetMostCommonCoStars(
        IEnumerable<Movie> moviesWithActor, string actorName)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<string> GetMostCommonGenres(IEnumerable<Movie> moviesWithActor)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<MovieCountByYear> GetMoviesPerYear(IEnumerable<Movie> moviesWithActor)
    {
        throw new NotImplementedException();
    }
}
