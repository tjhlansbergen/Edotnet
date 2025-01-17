using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildIn.Tests;

[TestClass]
public class ModulesTests
{
    [TestMethod]
    public void TestFindFound()
    {
        // act
        var result = EBuildIn.Modules.FindFunctionAndReturnParameters("Console", "WriteLine");

        // assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(Types.T, result.Single());
    }

    [TestMethod]
    public void TestFindNotFound()
    {
        // act
        var result = EBuildIn.Modules.FindFunctionAndReturnParameters("Test", "Test");

        // assert
        Assert.IsNull(result);
    }
}