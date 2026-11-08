using System.Globalization;

namespace EBuildIn;

public static class Time
{
    public static List<Types> SystemParameters => [];

    public static Variable System()
    {
        return new Variable(Types.Text, DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
    }

    public static List<Types> UtcParameters => [];

    public static Variable Utc()
    {
        return new Variable(Types.Text, DateTime.UtcNow.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
    }
}
