using System.Collections.Generic;
using EInterpreter.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EInterpreter.Tests
{
    [TestClass]
    public class PreValidationTests
    {
        [TestMethod]
        public void ValidationShouldSucceed_Empty()
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep>());

            // act
            var result = validator.Validate(new[] {""});

            // assert
            Assert.IsTrue(result, "Validation should succeed if no steps are specified");
        }

        [TestMethod]
        [DataRow("{")]
        [DataRow("}" )]
        [DataRow(";" )]
        [DataRow(")" )]
        [DataRow("// comment" )]
        public void ValidationShouldSucceed_HasValidLineEnds(string line)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasValidLineEndsStep() });

            // act
            var result = validator.Validate(new [] { line });

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("Invalid line!")]
        public void ValidationShouldFail_HasValidLineEnds(string line)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasValidLineEndsStep() });

            // act
            var result = validator.Validate(new[] { line });

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(new [] { "\"\"" })]
        [DataRow(new[] { "\"hi!\"", "\"there\"" })]
        public void ValidationShouldSucceed_HasMatchingQuotes(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasMatchingQuotes() });

            // act
            var result = validator.Validate(lines);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow(new[] { "\"" })]
        [DataRow(new[] { "\"hi!\"", "\"there\"\"" })]
        public void ValidationShouldFail_HasMatchingQuotes(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasMatchingQuotes() });

            // act
            var result = validator.Validate(lines);

            // assert
            Assert.IsFalse(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "{}" })]
        [DataRow(new[] { "()" })]
        [DataRow(new[] { "[]" })]
        [DataRow(new[] { "<>" })]
        [DataRow(new[] { "{","}" })]
        [DataRow(new[] { "(","...",")" })]
        [DataRow(new[] { "[","<","]",">" })]
        [DataRow(new[] { "<{()][}>", "><{(][})" })]
        public void ValidationShouldSucceed_HasMatchingBraces(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasMatchingBraces() });

            // act
            var result = validator.Validate(lines);

            // assert
            Assert.IsTrue(result, $"Testcase {string.Join(" ",lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "{" })]
        [DataRow(new[] { "((" })]
        [DataRow(new[] { "[][" })]
        [DataRow(new[] { "<>...<>...", "<...<>" })]
        [DataRow(new[] { "<{()]", "[}>>" })]
        public void ValidationShouldFail_HasMatchingBraces(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new HasMatchingBraces() });

            // act
            var result = validator.Validate(lines);

            // assert
            Assert.IsFalse(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "Utility Program" })]
        public void ValidationShouldSucceed_ContainsProgramUtility(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ContainsProgramUtility() });

            // act
            var result = validator.Validate(lines);

            // assert
            Assert.IsTrue(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "Utility" })]
        [DataRow(new[] { "Program" })]
        [DataRow(new[] { "UtilityProgram" })]
        [DataRow(new[] { "Utility", "Program" })]
        public void ValidationShouldFail_ContainsProgramUtility(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ContainsProgramUtility() });

            // act
            var result = validator.Validate(lines);

            // assert
            Assert.IsFalse(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "Function Boolean Start" })]
        public void ValidationShouldSucceed_ContainsStartFunction(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ContainsStartFunction() });

            // act
            var result = validator.Validate(lines);

            // assert
            Assert.IsTrue(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        [DataRow(new[] { "Function Start" })]
        [DataRow(new[] { "Boolean Start" })]
        public void ValidationShouldFail_ContainsStartFunction(string[] lines)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ContainsStartFunction() });

            // act
            var result = validator.Validate(lines);

            // assert
            Assert.IsFalse(result, $"Testcase {string.Join(" ", lines)} failed");
        }

        [TestMethod]
        public void ValidationShouldSucceed_BlockOpeningsOk()
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new BlockOpeningsOk() });
            var result = false;

            // act
            foreach (var block in PreValidator.Blocks)
            {
                result = validator.Validate(new[] { block, "{" });
            }

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("(")]
        [DataRow("[")]
        [DataRow(" ")]
        [DataRow("\n")]
        [DataRow("\r\n")]
        public void ValidationShouldFail_BlockOpeningsOk(string nextLine)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new BlockOpeningsOk() });
            var result = false;

            // act
            foreach (var block in PreValidator.Blocks)
            {
                result = validator.Validate(new[] { block, nextLine });
            }

            // assert
            Assert.IsFalse(result, $"Testcase {nextLine} failed");
        }

        [TestMethod]
        [DataRow("Utility Parser")]
        [DataRow("Function List<Thing> GetThings()")]
        [DataRow("Object Car")]
        public void ValidationShouldSucceed_BlockDeclarationsOk(string block)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new BlockDeclarationsOk() });

            // act
            var result = validator.Validate(new[] { block });
            
            // assert
            Assert.IsTrue(result, $"Testcase {block} failed");
        }

        [TestMethod]
        [DataRow("Utility String Parser")]
        [DataRow("Function GetThings()")]
        [DataRow("Object bool Car")]
        public void ValidationShouldFail_BlockDeclarationsOk(string block)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new BlockDeclarationsOk() });

            // act
            var result = validator.Validate(new[] { block });

            // assert
            Assert.IsFalse(result, $"Testcase {block} failed");
        }

        [TestMethod]
        [DataRow("Constant Boolean PayMore = true")]
        [DataRow("Constant Number TheAnswer = 42")]
        public void ValidationShouldSucceed_ConstantsOk(string constant)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ConstantsOk() });

            // act
            var result = validator.Validate(new[] { constant });

            // assert
            Assert.IsTrue(result, $"Testcase {constant} failed");
        }

        [TestMethod]
        [DataRow("Constant Boolean PayMore")]
        [DataRow("Constant TheAnswer = 42")]
        [DataRow("Constant Text = \"mytext\"")]
        public void ValidationShouldFail_ConstantsOk(string constant)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new ConstantsOk() });

            // act
            var result = validator.Validate(new[] { constant });

            // assert
            Assert.IsFalse(result, $"Testcase {constant} failed");
        }

        [TestMethod]
        [DataRow("Property Boolean Test")]
        [DataRow("Property Number TestNum")]
        public void ValidationShouldSucceed_PropertiesOk(string prop)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new PropertiesOk() });

            // act
            var result = validator.Validate(new[] { prop });

            // assert
            Assert.IsTrue(result, $"Testcase {prop} failed");
        }

        [TestMethod]
        [DataRow("Property Boolean")]
        [DataRow("Property Number TestNum = 42")]
        public void ValidationShouldFail_PropertiesOk(string prop)
        {
            // arrange
            var validator = new PreValidator(new List<IPreValidationStep> { new PropertiesOk() });

            // act
            var result = validator.Validate(new[] { prop });

            // assert
            Assert.IsFalse(result, $"Testcase {prop} failed");
        }
    }
}
