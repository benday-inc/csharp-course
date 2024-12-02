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

        args.AddString("sort")
            .AsNotRequired()
            .WithDescription(
                "Sort by field name. Valid values are 'title', 'year', 'genre'. Multiple values can be used via comma separated values. Direction using 'asc' or 'desc'")
            .WithDefaultValue(string.Empty);

        args.AddString("genre")
            .AsNotRequired()
            .WithDescription(
                "Filter by genre")
            .WithDefaultValue(string.Empty);

        args.AddInt32("year")
            .AsNotRequired()
            .WithDescription(
                "Filter by year")
            .WithDefaultValue(-1);

        args.AddString("title")
            .AsNotRequired()
            .WithDescription(
                "Filter by title")
            .WithDefaultValue(string.Empty);

        return args;
    }

    protected override void OnExecute()
    {
        var decadesAsString = Arguments.GetStringValue("decades");
        var decades = Utilities.CommaSeparatedValuesToIntArray(decadesAsString);
        
        var genre = Arguments.GetStringValue("genre");
        var title = Arguments.GetStringValue("title");
        var year = Arguments.GetInt32Value("year");

        var sorts =
            Utilities.CommaSeparatedValuesToSearchArguments(
                Arguments.GetStringValue("sort"));

        var reader = new MovieDataReader();

        var movies = reader.GetMovies(decades);

        if (string.IsNullOrEmpty(genre) == false)
        {
            movies = FilterByGenre(movies, genre);
        }

        if (string.IsNullOrEmpty(title) == false)
        {
            movies = FilterByTitle(movies, title);
        }

        if (year != -1)
        {
            movies = FilterByYear(movies, year);
        }

        WriteLine($"Found {movies.Count} movies.");

        if (sorts.Length == 0)
        {
            foreach (var movie in movies)
            {
                WriteLine(movie.ToString());
            }
        }
        else
        {
            var sortedMovies = Sort(movies, sorts);
        }
    }
    private List<Movie> FilterByGenre(List<Movie> movies, string genre)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<Movie> Sort(
        List<Movie> movies, SearchArgument[] sorts)
    {
        throw new NotImplementedException();
    }

    private List<Movie> FilterByTitle(List<Movie> movies, string title)
    {
        throw new NotImplementedException();
    }
    private List<Movie> FilterByYear(List<Movie> movies, int year)
    {
        throw new NotImplementedException();
    }
}