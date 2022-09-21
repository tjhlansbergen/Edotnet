using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EInterpreter.Tests
{
    [TestClass]
    public class ScriptTests
    {
        [TestMethod]
        [DataRow(new[] { "Hello World!" }, "hello_world.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_constant.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_function.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_parameter.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_return.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_variable.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_assignment.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_if.e")]
        [DataRow(new[] { "Hello World!" }, "hello_world_while.e")]
        [DataRow(new[] { "Audi", "4", "Porsche", "2" }, "hello_world_object.e")]
        [DataRow(new[] { "10", " ", "8", "9" }, "hello_world_foreach.e")]
        [DataRow(new[] { "10", "7", "Fiffy10" }, "hello_world_foreachobject.e")]
        [DataRow(new[] { "1", "2" }, "hello_world_list.e")]
        [DataRow(new[] { "Hello World!" }, "number_equality.e")]
        [DataRow(new[] { "42", "84", "63" }, "number_addition.e")]
        [DataRow(new[] { "420", "6", "3" }, "number_calculus.e")]
        [DataRow(new[] { "1", "Fizz", "Buzz", "FizzBuzz", "98" }, "fizzbuzz_while.e")]
        public void TestWorkerFullScripts(string[] shouldContain, string name)
        {
            // arrange
            var stringWriter = new StringWriter();
            var worker = new Worker(stringWriter);

            var path = Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent?.Parent?.Parent?.Parent?.FullName,
                $"TestScripts//{name}");
            var lines = File.ReadAllLines(path);

            var shouldContainComplete = shouldContain.Concat(new[] { $"Pre-validation for `{name}` successful", $"Post-validation for `{name}` successful", "ran for", "returned" });

            // act
            worker.Go(lines, name);

            // assert
            Assert.IsFalse(stringWriter.ToString().Contains("Runtime error"), $"Runtime error while running script {name}");

            foreach (var s in shouldContainComplete)
            {
                Assert.IsTrue(stringWriter.ToString().Contains(s), $"Result for {name} does not contain: {s}");
            }
        }
    }
}
