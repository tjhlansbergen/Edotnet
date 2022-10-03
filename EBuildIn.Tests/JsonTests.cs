using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildIn.Tests
{
    [TestClass]
    public class JsonTest
    {
        [TestMethod]
        public void TestSelect()
        {
            // arrange
            var var1 = new Variable(Types.Text, "[\"Fiat\", \"BMW\"]");
            var var2 = new Variable(Types.Text, "{\"Cars\":[\"Ford\", \"BMW\", \"Fiat\"]}");
            
            var path1 = new Variable(Types.Text, "$[0]");   // fiat
            var path2 = new Variable(Types.Text, "$.Cars[1]");  // bmw

            // act
            var result1 = Json.Select(var1, path1);
            var result2 = Json.Select(var2, path2);

            // assert
            Assert.AreEqual("Fiat", result1.Value);
            Assert.AreEqual("BMW", result2.Value);
        }
    }
}

