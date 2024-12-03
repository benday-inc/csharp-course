namespace NunitLab.Api;

public class BigImportantService
{
    public async Task<int> DoSomethingImportant()
    {
        // Do something important...
        await Task.Delay(2500);
        return 42;
    }
}

