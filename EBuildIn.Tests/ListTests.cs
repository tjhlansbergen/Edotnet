using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildIn.Tests;

[TestClass]
public class ListTest
{
    [TestMethod]
    public void TestAdd()
    {
        // arrange
        var list = new Variable(Types.List, subTypes: [Types.Text.ToString()], null);
        var stri = new Variable(Types.Text, "Hi");

        // act
        EBuildIn.List.Add(list, stri);
        
        // assert
        Assert.AreEqual(1, (list.Value as List<Variable>)?.Count);
        Assert.AreEqual("Hi", (list.Value as List<Variable>)?.First().Value);
    }
}



