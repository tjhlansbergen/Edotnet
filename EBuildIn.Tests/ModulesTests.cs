using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildIn.Tests
{
    [TestClass]
    public class ModulesTests
    {
        [TestMethod]
        public void TestFindFound()
        {
            // act
            var result = EBuildIn.Modules.FindFunctionAndReturnParameters("Console", "WriteText");

            // assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Text", result.Single());
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
}