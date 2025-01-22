using Microsoft.VisualStudio.TestTools.UnitTesting;
using EInterpreter.EElements;
using EInterpreter.Lexing;
using EInterpreter.Validation;

namespace EInterpreter.Tests;

[TestClass]
public class PostValidationTests
{
    [TestMethod]
    public void ValidationShouldSucceed_Empty()
    {
        // arrange
        var validator = new PostValidator([]);

        // act
        var result = validator.Validate(new ETree());

        // assert
        Assert.IsTrue(result, "Validation should succeed if no steps are specified");
    }

    [TestMethod]
    public void ValidationShouldSucceed_GlobalIdentifiersAreUnique()
    {
        // arrange
        var validator = new PostValidator([new GlobalIdentifiersAreUnique()]);

        // act
        var result = validator.Validate(new ETree{Constants = [new EConstant("test", "Test1", "")], Utilities = [new EUtility("Test2")]});

        // assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ValidationShouldFail_GlobalIdentifiersAreUnique()
    {
        // arrange
        var validator = new PostValidator([new GlobalIdentifiersAreUnique()]);

        // act
        var result = validator.Validate(new ETree { Constants = [new EConstant("test", "Test1", "")], Utilities = [new EUtility("Test1")] });

        // assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ValidationShouldSucceed_ObjectIdentifiersAreUnique()
    {
        // arrange
        var validator = new PostValidator([new ObjectIdentifiersAreUnique()]);

        // act
        var result = validator.Validate(new ETree 
            { 
                Objects =
                [
                    new EObject("Test")
                    {
                        Properties =
                        [
                            new EDeclaration("test", "Test1"),
                            new EDeclaration("test", "Test2")
                        ]
                    }
                ]
            });

        // assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ValidationShouldFail_ObjectIdentifiersAreUnique()
    {
        // arrange
        var validator = new PostValidator([new ObjectIdentifiersAreUnique()]);

        // act
        var result = validator.Validate(new ETree
        {
            Objects =
            [
                new EObject("Test")
                {
                    Properties =
                    [
                        new EDeclaration("test", "Test1"),
                        new EDeclaration("test", "Test1")
                    ]
                }
            ]
        });

        // assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ValidationShouldSucceed_UtilityIdentifiersAreUnique()
    {
        // arrange
        var validator = new PostValidator([new UtilityIdentifiersAreUnique()]);

        // act
        var result = validator.Validate(new ETree
        {
            Utilities =
            [
                new EUtility("Test")
                {
                    Functions =
                    [
                        new EFunction("void", "Test1", []),
                        new EFunction("void", "Test2", [])
                    ]
                }
            ]
        });

        // assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ValidationShouldFail_UtilityIdentifiersAreUnique()
    {
        // arrange
        var validator = new PostValidator([new UtilityIdentifiersAreUnique()]);

        // act
        var result = validator.Validate(new ETree
        {
            Utilities =
            [
                new EUtility("Test")
                {
                    Functions =
                    [
                        new EFunction("void", "Test1", []),
                        new EFunction("void", "Test1", [])
                    ]
                }
            ]
        });

        // assert
        Assert.IsFalse(result);
    }
}
