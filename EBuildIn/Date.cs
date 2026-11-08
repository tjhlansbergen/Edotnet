using System.Net;

namespace EBuildIn;

public static class Date
{
    public static List<Types> SystemParameters => [];

    public static Variable System()
    {
        return new Variable(Types.Text, DateTime.Now.Date.ToString("yyyy-MM-dd"));
    }

    public static List<Types> UtcParameters => [];

    public static Variable Utc()
    {
        return new Variable(Types.Text, DateTime.UtcNow.Date.ToString("yyyy-MM-dd"));
    }
}