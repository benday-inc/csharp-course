namespace LinqLab.Api.Commands;

public static class StringExtensionMethods
{
    public static bool EqualsCaseInsensitive(this string value1, string value2)
    {
        return string.Equals(value1, value2, StringComparison.OrdinalIgnoreCase);
    }

    public static bool ContainsCaseInsensitive(this string value1, string value2)
    {
        return value1.Contains(value2, StringComparison.OrdinalIgnoreCase);
    }

    public static ActorInfo ToActor(this string actorName)
    {
        var actor = new ActorInfo
        {
            Name = actorName,
            MovieCount = 0
        };

        return actor;
    }

    public static ActorInfo[] ToActors(this string[] actors)
    {
        return actors.Select(a => a.ToActor()).ToArray();
    }
}