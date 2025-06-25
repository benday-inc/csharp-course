using Benday.CommandsFramework;

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
    private Task SearchAsync(string keyword, bool multithreaded)
    {
        throw new NotImplementedException();
    }
}