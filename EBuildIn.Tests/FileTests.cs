using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildIn.Tests
{
    [TestClass]
    public class FileTests
    {
        [TestMethod]
        public void TestReadFileNotFound()
        {
            // arrange

            // act
            
            // assert
            Assert.ThrowsException<FileNotFoundException>(() => EBuildIn.File.Read(new Variable(Types.Text, "\file\not\found.txt")));        
        }
    }
}

