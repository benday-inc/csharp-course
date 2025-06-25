namespace LinqLab.Api.Commands;

public class KeywordSearchResult
{
    public required string MatchType { get; set; }
    public required string MatchDescription { get; set; }
    public required Movie Movie { get; set; }
}