using System.Net;

namespace EBuildIn;

public static class Web
{
    public static List<Types> ReadParameters => [Types.Text];

    public static Variable Read(Variable url)
    {
        var client = new HttpClient();
        var content = client.GetStringAsync((string)url.Value!).GetAwaiter().GetResult();
        return new Variable(Types.Text, content);
    }
}