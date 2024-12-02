using Benday.CommandsFramework;

namespace LinqLab.Api.Commands;

[Command(Name = "list",
    Description = "List movies.")]
public class ListMoviesCommand : SynchronousCommand
{
    public ListMoviesCommand(CommandExecutionInfo info, ITextOutputProvider outputProvider) :
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

        return args;
    }

    protected override void OnExecute()
    {
        var decadesAsString = Arguments.GetStringValue("decades");

        var decades = Utilities.CommaSeparatedValuesToIntArray(decadesAsString);

        var reader = new MovieDataReader();

        var movies = reader.GetMovies(decades);

        WriteLine($"Found {movies.Count} movies.");

        foreach (var movie in movies)
        {
            WriteLine(movie.ToString());
        }
    }
}