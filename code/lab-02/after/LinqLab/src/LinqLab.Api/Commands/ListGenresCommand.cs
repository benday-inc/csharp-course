using Benday.CommandsFramework;

namespace LinqLab.Api.Commands;

[Command(Name = "genres",
    Description = "Get a list of genres")]
public class ListGenresCommand : SynchronousCommand
{
    public ListGenresCommand(CommandExecutionInfo info, ITextOutputProvider outputProvider) :
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

        args.AddBoolean("desc")
            .AllowEmptyValue()
            .AsNotRequired()
            .WithDescription("Sort genres in descending order")
            .WithDefaultValue(false);

        return args;
    }

    protected override void OnExecute()
    {
        var decadesAsString = Arguments.GetStringValue("decades");
        var decades = Utilities.CommaSeparatedValuesToIntArray(decadesAsString);

        var sortDescending = Arguments.GetBooleanValue("desc");

        var reader = new MovieDataReader();

        var movies = reader.GetMovies(decades);

        WriteLine($"Found {movies.Count} movies.");

        var genres = GetDistinctGenres(movies, sortDescending);

        if (genres.Length == 0)
        {
            WriteLine("No genres found.");            
        }
        else
        {
            foreach (var genre in genres)
            {
                WriteLine(genre);
            }
        }
    }

    private string[] GetDistinctGenres(List<Movie> movies, bool sortDescending)
    {
        var distinctGenres = movies.SelectMany(m => m.Genres).Distinct();

        if (sortDescending == false)
        {
            return distinctGenres
                .OrderBy(g => g)
                .ToArray();
        }
        else
        {
            return distinctGenres
                .OrderByDescending(g => g)
                .ToArray();
        }
    }
}