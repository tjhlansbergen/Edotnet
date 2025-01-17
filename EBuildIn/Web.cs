using System.Net;

namespace EBuildIn;

public static class Web
{
    public static List<Types> ReadParameters => new List<Types> { Types.Text };

    public static Variable Read(Variable url)
    {
        var content = new WebClient().DownloadString((string)url.Value);
        return new Variable(Types.Text, content);
    }
}