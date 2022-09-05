using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildIn.Tests
{
    [TestClass]
    public class ListTest
    {
        [TestMethod]
        public void TestAdd()
        {
            // arrange
            var list = new Variable(Types.List, subTypes: new List<Types> { Types.Text }, null);
            var stri = new Variable(Types.Text, "Hi");

            // act
            EBuildIn.List.Add(list, stri);
            
            // assert
            Assert.AreEqual(1, ((List<Variable>)list.Value).Count());
            Assert.AreEqual("Hi", ((List<Variable>)list.Value).First().Value);
        }
    }
}



