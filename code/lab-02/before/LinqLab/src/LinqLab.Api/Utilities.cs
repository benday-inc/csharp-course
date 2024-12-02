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
}

