using Benday.CommandsFramework;
using System.Runtime.InteropServices;

namespace LinqLab.Api.Commands;

[Command(
    IsAsync = true,
    Name = "search",
    Description = "Searches by keyword (name, title)")]
public class SearchCommand : AsynchronousCommand
{
    public SearchCommand(CommandExecutionInfo info, ITextOutputProvider outputProvider) :
        base(info, outputProvider)
    {

    }

    public override ArgumentCollection GetArguments()
    {
        var args = new ArgumentCollection();

        args.AddString("keyword")
            .AsRequired()
            .WithDescription(
                "Keyword to search for.")
            .FromPositionalArgument(1);

        args.AddBoolean("multithreaded")
            .AsNotRequired()
            .AllowEmptyValue()
            .WithDescription("Run multithreaded search. defaults to false.")
            .WithDefaultValue(false);

        return args;
    }

    protected override async Task OnExecute()
    {
        var keyword = Arguments.GetStringValue("keyword");
        var multithreaded = Arguments.GetBooleanValue("multithreaded");

        if (string.IsNullOrWhiteSpace(keyword))
        {
            throw new KnownException("Keyword value cannot be null or empty.");
        }

        await SearchAsync(keyword, multithreaded);
    }
    private async Task SearchAsync(string keyword, bool multithreaded)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var decades = new int[] { 1970, 1980, 1990, 2000, 2010, 2020 };

        var results = new List<KeywordSearchResult>();

        foreach (var decade in decades)
        {
            var decadeResult = await SearchAsync(keyword, decade);

            if (decadeResult.Count > 0)
            {
                results.AddRange(decadeResult);
            }
        }

        stopwatch.Stop();

        if (results.Count == 0)
        {
            WriteLine("No results found.");            
        }
        else
        {
            WriteLine($"Found {results.Count} results for keyword '{keyword}':");
            WriteLine("--------------------------------------------------");

            foreach (var result in results)
            {
                WriteLine($"{result.MatchType}: {result.MatchDescription} ({result.Movie.Year})");
            }
        }

        WriteLine("--------------------------------------------------");

        WriteLine($"Search completed in {stopwatch.ElapsedMilliseconds} ms.");
    }

    private Task<List<KeywordSearchResult>> SearchAsync(string keyword, int decade)
    {
        var sortDescending = Arguments.GetBooleanValue("desc");

        var reader = new MovieDataReader();

        var movies = reader.GetMovies(decade);

        var results = new List<KeywordSearchResult>();

        var matchingMovies = movies
            .Where(m => m.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var movie in matchingMovies)
        {
            results.Add(new KeywordSearchResult
            {
                MatchType = "Title",
                MatchDescription = movie.Title,
                Movie = movie
            });
        }

        var matchingMoviesByCast = movies
            .Where(m => m.Cast.Any(c => c.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        foreach (var movie in matchingMoviesByCast)
        {
            var matchingCast = movie.Cast
                .Where(c => c.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var castMember in matchingCast)
            {
                results.Add(new KeywordSearchResult
                {
                    MatchType = "Cast",
                    MatchDescription = $"{castMember} in {movie.Title}",
                    Movie = movie
                });
            }
        }

        return Task.FromResult(results);
    }
}
