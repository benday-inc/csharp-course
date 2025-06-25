namespace LinqLab.Api.Commands;

public static class ValidationUtility
{   
    public static async Task<bool> ValidateResult(KeywordSearchResult result)
    {
        // get random bool
        var wait = new Random().Next(0, 2) == 1;

        if (wait == true)
        {
            var randomDelay = new Random().Next(100, 250);

            await Task.Delay(randomDelay);
        }
        else
        {
            await Task.CompletedTask;
        }

        return true;
    }
}
