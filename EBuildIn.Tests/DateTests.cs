using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildIn.Tests;

[TestClass]
public class DateTest
{
    [TestMethod]
    public void TestSystem()
    {
        // arrange
        var date = new Variable(Types.Text, DateTime.Now.Date.ToString("yyyy-MM-dd"));

        // act
        var result = EBuildIn.Date.System();

        // assert
        Assert.AreEqual(date.Value, result.Value);
    }
}



