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

        args.AddString("actor")
            .AsNotRequired()
            .WithDescription(
                "Filter by actor name")
            .WithDefaultValue(string.Empty);

        args.AddInt32("rows")
            .AsNotRequired()
            .WithDescription(
                "Maximum number of results to return")
            .WithDefaultValue(-1);

        return args;
    }

    protected override void OnExecute()
    {
        var decadesAsString = Arguments.GetStringValue("decades");
        var decades = Utilities.CommaSeparatedValuesToIntArray(decadesAsString);
        
        var genre = Arguments.GetStringValue("genre");
        var actor = Arguments.GetStringValue("actor");
        var title = Arguments.GetStringValue("title");
        var year = Arguments.GetInt32Value("year");
        var numberOfRows = Arguments.GetInt32Value("rows");

        var sorts =
            Utilities.CommaSeparatedValuesToSearchArguments(
                Arguments.GetStringValue("sort"));

        var reader = new MovieDataReader();

        var movies = reader.GetMovies(decades);

        var filterByGenre = false;

        if (string.IsNullOrEmpty(genre) == false)
        {
            filterByGenre = true;
            movies = FilterByGenre(movies, genre);
        }

        if (string.IsNullOrEmpty(title) == false)
        {
            movies = FilterByTitle(movies, title);
        }

        var filterByActor = false;

        if (string.IsNullOrEmpty(actor) == false)
        {
            filterByActor = true;
            movies = FilterByActor(movies, actor);
        }

        if (year != -1)
        {
            movies = FilterByYear(movies, year);
        }

        WriteLine($"Found {movies.Count} movies.");

        if (sorts.Length == 0)
        {
            if (numberOfRows != -1)
            {
                movies = FilterByNumberOfRows(movies, numberOfRows);
            }

            foreach (var movie in movies)
            {
                if (filterByGenre == true)
                {
                    WriteLine(movie.ToStringGenres());
                }
                else if (filterByActor == true)
                {
                    WriteLine(movie.ToStringActors());
                }
                else
                {
                    WriteLine(movie.ToString());
                }
            }
        }
        else
        {
            var sortedMovies = SortMovies(movies, sorts);

            if (numberOfRows != -1)
            {
                movies = FilterByNumberOfRows(movies, numberOfRows);
            }

            foreach (var movie in sortedMovies)
            {
                if (filterByGenre == true)
                {
                    WriteLine(movie.ToStringGenres());
                }
                else if (filterByActor == true)
                {
                    WriteLine(movie.ToStringActors());
                }
                else
                {
                    WriteLine(movie.ToString());
                }
            }
        }
    }

    private List<Movie> FilterByNumberOfRows(List<Movie> movies, int numberOfRows)
    {
        return movies.Take(numberOfRows).ToList();
    }

    private List<Movie> FilterByGenre(List<Movie> movies, string genre)
    {
        var filteredMovies = movies.Where(
            m =>
            {
                var found = false;
                foreach (var g in m.Genres)
                {
                    if (g.ContainsCaseInsensitive(genre))
                    {
                        found = true;
                        break;
                    }
                }
                return found;
            }).ToList();

        return filteredMovies;
    }

    

    private IEnumerable<Movie> SortMovies(
        List<Movie> movies, SearchArgument[] sorts)
    {
        if (sorts.Length == 0)
        {
            return movies;
        }


        var firstSort = sorts[0];

        if (firstSort.Value.EqualsCaseInsensitive("title") || 
            firstSort.Value.EqualsCaseInsensitive("name"))
        {
            var sortedMovies = movies.AsEnumerable();

            if (firstSort.IsAscending == true)
            {
                sortedMovies = sortedMovies.OrderBy(m => m.Title);
            }
            else
            {
                sortedMovies = sortedMovies.OrderByDescending(m => m.Title);
            }

            return sortedMovies;
        }
        else if (firstSort.Value.EqualsCaseInsensitive("year"))
        {
            var sortedMovies = movies.AsEnumerable();

            if (firstSort.IsAscending == true)
            {
                sortedMovies = sortedMovies.OrderBy(m => m.Year);
            }
            else
            {
                sortedMovies = sortedMovies.OrderByDescending(m => m.Year);
            }

            return sortedMovies;
        }
        else if (firstSort.Value.EqualsCaseInsensitive("genre"))
        {
            var sortedMovies = movies.AsEnumerable();

            if (firstSort.IsAscending == true)
            {
                sortedMovies = sortedMovies.OrderBy(m => m.Genres.FirstOrDefault());
            }
            else
            {
                sortedMovies = sortedMovies.OrderByDescending(m => m.Genres.FirstOrDefault());
            }

            return sortedMovies;
        }

        return movies;
    }

    private List<Movie> FilterByTitle(List<Movie> movies, string title)
    {
        var filteredMovies = movies
            .Where(m => 
                m.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase
                )).ToList();
        return filteredMovies;
    }

    private List<Movie> FilterByActor(List<Movie> movies, string actor)
    {
        var filteredMovies = movies
            .Where(m =>
                m.Cast.Any(a =>
                a.Contains(actor, StringComparison.CurrentCultureIgnoreCase)))
            .ToList();

        return filteredMovies;
    }

    private List<Movie> FilterByYear(List<Movie> movies, int year)
    {
        var filteredMovies = movies.Where(m => m.Year == year).ToList();

        return filteredMovies;
    }
}
