namespace LinqLab.Api;

public static class Utilities
{
    public static int[] CommaSeparatedValuesToIntArray(string input)
    {
        var returnValue = new List<int>();
        if (string.IsNullOrWhiteSpace(input) == false)
        {
            var parts = input.Split(',');
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int value) == true)
                {
                    returnValue.Add(value);
                }
            }
        }
        return returnValue.ToArray();
    }

    public static string[] CommaSeparatedValuesToStringArray(string input)
    {
        var returnValue = new List<string>();
        if (string.IsNullOrWhiteSpace(input) == false)
        {
            var parts = input.Split(',');
            foreach (var part in parts)
            {
                returnValue.Add(part);
            }
        }
        return returnValue.ToArray();
    }

    public static SearchArgument[] CommaSeparatedValuesToSearchArguments(string input)
    {
        var returnValue = new List<SearchArgument>();

        var parts = CommaSeparatedValuesToStringArray(input);

        foreach (var part in parts)
        {
            returnValue.Add(new SearchArgument(part));
        }

        return returnValue.ToArray();
    }
}

