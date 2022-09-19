using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EInterpreter.Lexing;
using EInterpreter.EElements;

namespace EInterpreter.Tests
{
    [TestClass]
    public class ParsersTests
    {
        [TestMethod]
        [DataRow("Constant Boolean Test = \"True\"")]
        [DataRow("Constant Text Test = \"Some text\"")]
        [DataRow("Constant Number Test = 42.2")]
        public void ParsersShouldSucceed_ParseConstant(string line)
        {
            // act
            var result = Parsers.ParseConstant(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EConstant));
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Constant Test")]
        [DataRow("Constant Test = test")]
        [DataRow("Constant Boolean Test")]
        public void ParsersShouldFail_ParseConstant(string line)
        {
            // assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => Parsers.ParseConstant(line));
        }

        [TestMethod]
        [DataRow("Property Boolean Test")]
        [DataRow("Number 42")]
        public void ParsersShouldSucceed_ParseProperty(string line)
        {
            // act
            var result = Parsers.ParseProperty(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EProperty));
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Property Test = test")]
        public void ParsersShouldFail_ParseProperty(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseProperty(line));
        }

        [TestMethod]
        [DataRow("Object Test")]
        public void ParsersShouldSucceed_ParseObject(string line)
        {
            // act
            var result = Parsers.ParseObject(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EObject));
            Assert.AreEqual("Test", result.Name);
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Object")]
        [DataRow("Test Object")]
        [DataRow("Utility Test")]
        public void ParsersShouldFail_ParseObject(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseObject(line));
        }

        [TestMethod]
        [DataRow("Utility Test")]
        public void ParsersShouldSucceed_ParseUtility(string line)
        {
            // act
            var result = Parsers.ParseUtility("Test.", line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EUtility));
            Assert.AreEqual("Test.Test", result.Name);
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Utility")]
        [DataRow("Test Utility")]
        [DataRow("Object Test")]
        public void ParsersShouldFail_ParseUtility(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseUtility("Test.", line));
        }

        [TestMethod]
        [DataRow("if(True)")]
        [DataRow("foreach(item in list)")]
        public void ParsersShouldSucceed_ParseStatement(string line)
        {
            // act
            var result = Parsers.ParseStatement(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Guid.TryParse(result.Name, out _));
            Assert.IsInstanceOfType(result, typeof(EStatement));
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("if")]
        [DataRow("foreach")]
        public void ParsersShouldFail_ParseStatement(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseStatement(line));
        }

        [TestMethod]
        [DataRow("Function Boolean SomeFunction()", 0)]
        [DataRow("Function Number GetAge(Text birthdate)", 1)]
        [DataRow("Function Number GetAge(Text birthdate, Boolean inDays)", 2)]
        public void ParsersShouldSucceed_ParseFunction(string line, int numberOfParameters)
        {
            // act
            var result = Parsers.ParseFunction("", line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EFunction));
            Assert.AreEqual(numberOfParameters, result.Parameters.Count);
            Assert.IsTrue(line.SplitClean(' ')[2].StartsWith(result.Name));
        }

        [TestMethod]
        [DataRow("Function Test")]
        [DataRow("Function Boolean Test")]
        [DataRow("Boolean Test()")]
        public void ParsersShouldFail_ParseFunction(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseFunction("", line));
        }

        [TestMethod]
        [DataRow("Console:WriteLine()")]
        [DataRow("Console:WriteLine(\"some text to write\")")]
        public void ParsersShouldSucceed_ParseFunctionCall(string line)
        {
            // act
            var result = Parsers.ParseFunctionCall(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EFunctionCall));
        }

        [TestMethod]
        [DataRow("Console.WriteLine()")]
        [DataRow("Console:WriteLine")]
        [DataRow("WriteLine()")]
        public void ParsersShouldFail_ParseFunctionCall(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseFunctionCall(line));
        }

        [TestMethod]
        [DataRow("new Message message")]
        [DataRow("new Number Age")]
        public void ParsersShouldSucceed_ParseDeclaration(string line)
        {
            // act
            var result = Parsers.ParseDeclaration(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EDeclaration));
        }

        [TestMethod]
        [DataRow("Boolean Test")]
        [DataRow("new Number")]
        [DataRow("new Number Age = 42")]
        public void ParsersShouldFail_ParseDeclaration(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseDeclaration(line));
        }

        [TestMethod]
        [DataRow("return \"hi\";")]
        [DataRow("return 42;")]
        [DataRow("return someVar;")]
        public void ParsersShouldSucceed_ParseReturn(string line)
        {
            // act
            var result = Parsers.ParseReturnStatement(line);

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EReturn));
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("return test")]
        [DataRow("return var = 42;")]
        public void ParsersShouldFail_ParseReturn(string line)
        {
            // assert
            Assert.ThrowsException<ParserException>(() => Parsers.ParseReturnStatement(line));
        }

        [TestMethod]
        [DataRow("Hi I have one , real comma at index 15", new [] {14})]
        [DataRow("\",\"", new int[] {})]
        [DataRow("\",\",", new [] {3})]
        [DataRow("List:Get(numbers, 1)", new int [] {})]
        [DataRow("Console:Write(\",\")", new int [] {})]
        [DataRow("Console:Write(\",\", 1)", new int [] {})]
        [DataRow("Console:Write(\",\", 1), 1", new int [] {21})]
        [DataRow("a, b", new [] {1})]
        [DataRow("a, b, c", new [] {1, 4})]
        [DataRow("a, \"b,,,\", c", new [] {1, 9})]
        public void TestGetRealCommas(string line, int[] indices)
        {
            // act
            var result = Parsers.GetRealCommas(line);

            // assert
            CollectionAssert.AreEqual(result, indices, $" testcase: {line}");
        }
    }
}
