namespace Minimal_API.API.Common;

public static class Utils
{
    public static Dictionary<string, object?> ExtensionsReturnValues(params KeyValuePair<string, object?>[] keyValuePair)
    {
        var extensions = new Dictionary<string, object?>(keyValuePair);

        return extensions;
    }
}