using Microsoft.VisualStudio.TestTools.UnitTesting;
using EInterpreter.Lexing;

namespace EInterpreter.Tests;

[TestClass]
public class LexerTests
{
    [TestMethod]
    public void TestGetTreeEmpty()
    {
        // arrange
        var lexer = new Lexer();

        // act
        var result = lexer.GetTree(["// comment"]);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, lexer.Tokens.Count);
    }

    [TestMethod]
    public void TestGetTree()
    {
        // arrange
        var lexer = new Lexer();

        // act
        var result = lexer.GetTree(["// comment", "Constant Boolean Test = true"]);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Constants.Count);
        Assert.AreEqual(2, lexer.Tokens.Count);
    }
}
