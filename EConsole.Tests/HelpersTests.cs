using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EConsole.Tests;

[TestClass]
public class HelpersTests
{
    [TestMethod]
    public void TestIsValidTextFile()
    {
        // arrange
        var path = Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent?.Parent?.Parent?.Parent?.FullName, "TestScripts");

        // assert
        foreach (var file in Directory.GetFiles(path))
        {
            Assert.IsTrue(EConsole.Helpers.IsValidTextFile(file));
        }
    }
}
