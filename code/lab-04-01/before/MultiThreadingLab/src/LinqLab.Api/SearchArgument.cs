namespace LinqLab.Api;

public class SearchArgument
{
    private const char SPACE_CHARACTER = ' ';
    private const string ASCENDING = "asc";
    private const string DESCENDING = "desc";
    public string Value { get; private set; }
    public bool IsAscending { get; private set; }
    public bool IsDescending { get; private set; }

    public SearchArgument(string argumentValue)
    {
        if (string.IsNullOrWhiteSpace(argumentValue) == true)
        {
            throw new InvalidOperationException($"Argument cannot be null or empty.");
        }
        else
        {
            var trimmed = argumentValue.Trim();

            if (trimmed.Contains(SPACE_CHARACTER) == false)
            {
                Value = trimmed;
                IsAscending = true;
                IsDescending = false;
            }
            else
            {
                var parts = trimmed.Split(
                    SPACE_CHARACTER,
                    StringSplitOptions.RemoveEmptyEntries);

                Value = parts[0];

                if (parts.Length > 1)
                {
                    var direction = parts[1].ToLower();

                    if (direction == ASCENDING)
                    {
                        IsAscending = true;
                        IsDescending = false;
                    }
                    else if (direction == DESCENDING)
                    {
                        IsAscending = false;
                        IsDescending = true;
                    }
                    else
                    {
                        // invalid direction
                        // default to ascending
                        IsAscending = true;
                        IsDescending = false;
                    }
                }
            }
        }
    }
}

