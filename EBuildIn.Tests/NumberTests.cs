using EBuildIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildIn.Tests
{
    [TestClass]
    public class NumberTests
    {
        [TestMethod]
        [DataRow(1.0,1.0, 2.0)]
        [DataRow(1.0, 2.0, 3.0)]
        [DataRow(4.0, 5.0, 9.0)]
        [DataRow(4.4, 5.3, 9.7)]
        [DataRow(4346346.43463643, 796464.335, 5142810.76963643)]
        public void TestAdd(double a, double b, double expected)
        {
            // arrange
            var aa = new Variable(Types.Number, a, "TestAdd");
            var bb = new Variable(Types.Number, b, "TestAdd");

            // act
            var result = Number.Add(aa, bb);

            // assert
            Assert.AreEqual(expected, result.Value);
            Assert.AreEqual(a, aa.Value);
            Assert.AreEqual(b, bb.Value);
        }

        [TestMethod]
        [DataRow(1.0, 1.0, 0.0)]
        [DataRow(1.0, 2.0, -1.0)]
        [DataRow(5.0, 3.0, 2.0)]
        public void TestSubtract(double a, double b, double expected)
        {
            // arrange
            var aa = new Variable(Types.Number, a, "TestAdd");
            var bb = new Variable(Types.Number, b, "TestAdd");

            // act
            var result = Number.Subtract(aa, bb);

            // assert
            Assert.AreEqual(expected, result.Value);
            Assert.AreEqual(a, aa.Value);
            Assert.AreEqual(b, bb.Value);
        }

        [TestMethod]
        [DataRow(1.0, 1.0, false)]
        [DataRow(1.0, 2.0, true)]
        [DataRow(5.0, 3.0, false)]
        [DataRow(-5.0, 3.0, true)]
        public void TestLessThen(double a, double b, bool expected)
        {
            // arrange
            var aa = new Variable(Types.Number, a, "TestAdd");
            var bb = new Variable(Types.Number, b, "TestAdd");

            // act
            var result = Number.LessThen(aa, bb);

            // assert
            Assert.AreEqual(expected, result.Value);
            Assert.AreEqual(b, bb.Value);
        }

        [TestMethod]
        [DataRow(1.0, 1.0, false)]
        [DataRow(1.0, 2.0, false)]
        [DataRow(5.0, 3.0, true)]
        [DataRow(-5.0, 3.0, false)]
        public void TestGreaterThen(double a, double b, bool expected)
        {
            // arrange
            var aa = new Variable(Types.Number, a, "TestAdd");
            var bb = new Variable(Types.Number, b, "TestAdd");

            // act
            var result = Number.GreaterThen(aa, bb);

            // assert
            Assert.AreEqual(expected, result.Value);
            Assert.AreEqual(b, bb.Value);
        }

        [TestMethod]
        [DataRow(1.0, 1.0, false)]
        [DataRow(1.0, 2.0, true)]
        [DataRow(5.0, 3.0, true)]
        [DataRow(-5.0, 5.0, true)]
        public void TestNotEqual(double a, double b, bool expected)
        {
            // arrange
            var aa = new Variable(Types.Number, a, "TestAdd");
            var bb = new Variable(Types.Number, b, "TestAdd");

            // act
            var result = Number.NotEqual(aa, bb);

            // assert
            Assert.AreEqual(expected, result.Value);
            Assert.AreEqual(b, bb.Value);
        }
    }
}
