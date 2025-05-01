using Benday.CommandsFramework;
using Benday.CommandsFramework.DataFormatting;

namespace LinqLab.Api.Commands;

[Command(Name = "popular",
    Description = "Get a list of the most popular actors")]
public class MostPopularActorsCommand : SynchronousCommand
{
    public MostPopularActorsCommand(CommandExecutionInfo info, ITextOutputProvider outputProvider) :
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

        args.AddInt32("rows")
            .AsNotRequired()
            .WithDescription(
                "Maximum number of results to return. Default is 20.")
            .WithDefaultValue(20);

        return args;
    }

    protected override void OnExecute()
    {
        var decadesAsString = Arguments.GetStringValue("decades");
        var decades = Utilities.CommaSeparatedValuesToIntArray(decadesAsString);

        var rows = Arguments.GetInt32Value("rows");

        var reader = new MovieDataReader();

        var movies = reader.GetMovies(decades);

        WriteLine($"Found {movies.Count} movies.");

        var actors = GetMostPopularActors(movies, rows);

        var formatter = new TableFormatter();

        formatter.AddColumn("Actor Name");
        formatter.AddColumn("# of Movies");

        foreach (var item in actors)
        {
            formatter.AddData(item.Name, item.MovieCount.ToString());
        }

        WriteLine(formatter.FormatTable());
    }

    private ActorInfo[] GetMostPopularActors(List<Movie> movies, int rows)
    {
        var actorInfoList = new List<ActorInfo>();
        foreach (var movie in movies)
        {
            foreach (var actor in movie.Cast)
            {
                var existingActor = actorInfoList.FirstOrDefault(a => a.Name == actor);
                if (existingActor == null)
                {
                    actorInfoList.Add(new ActorInfo() { 
                        Name = actor,
                        MovieCount = 1
                    });
                }
                else
                {
                    existingActor.MovieCount++;
                }
            }
        }

        var sortedActors = actorInfoList.OrderByDescending(a => a.MovieCount).ToArray();

        if (rows > 0 && rows < sortedActors.Length)
        {
            return sortedActors.Take(rows).ToArray();
        }
        else if (rows == 0)
        {
            return Array.Empty<ActorInfo>();
        }
        else
        {
            return sortedActors;
        }
    }   
}
