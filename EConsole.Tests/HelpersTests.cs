using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EConsole.Tests;

[TestClass]
public class HelpersTests
{
    [TestMethod]
    public void TestIsValidTextFile()
    {
        // arrange
        var solutionRootDir = new DirectoryInfo(Environment.CurrentDirectory).Parent?.Parent?.Parent?.Parent?.FullName;
        if (solutionRootDir == null) throw new DirectoryNotFoundException("Could not find root directory");

        var path = Path.Combine(solutionRootDir, "TestScripts");

        // assert
        foreach (var file in Directory.GetFiles(path))
        {
            Assert.IsTrue(EConsole.Helpers.IsValidTextFile(file));
        }
    }
}
