using Microsoft.VisualStudio.TestTools.UnitTesting;
using EInterpreter.EElements;
using EInterpreter.Lexing;

namespace EInterpreter.Tests;

[TestClass]
public class EngineTests
{
    [TestMethod]
    public void RunShouldFail()
    {
        // arrange
        var tree = new ETree
        {
            Constants = [new EConstant("test", "Test1", "")],
            Utilities = [new EUtility("Test2")]
        };
        var engine = new EInterpreter.Engine.Engine(tree);

        // assert
        Assert.ThrowsException<EInterpreter.EngineException>(() => engine.Run());
    }

    [TestMethod]
    public void RunShouldSucceed()
    {
        // arrange
        var tree = new ETree
        {
            Utilities =
            [
                new EUtility("Program")
                {
                    Functions =
                    [
                        new EFunction("Boolean", "Program.Start", [new EProperty("Text", "arguments")])
                        {
                            Elements = { new EReturn("", "true")}
                        }
                    ]
                }
            ]
        };
        var engine = new EInterpreter.Engine.Engine(tree);

        // act
        engine.Run();
    }
}
