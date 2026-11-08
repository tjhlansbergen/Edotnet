using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EBuildIn.Tests;

[TestClass]
public class TimeTests
{
    [TestMethod]
    public void TestSystem()
    {
        var parameters = EBuildIn.Modules.FindFunctionAndReturnParameters("Time", "System");
        var result = EBuildIn.Modules.Run("Time", "System", []);

        Assert.AreEqual(0, parameters?.Count);
        AssertTimeIsCurrent(result, DateTime.Now);
    }

    [TestMethod]
    public void TestUtc()
    {
        var parameters = EBuildIn.Modules.FindFunctionAndReturnParameters("Time", "Utc");
        var result = EBuildIn.Modules.Run("Time", "Utc", []);

        Assert.AreEqual(0, parameters?.Count);
        AssertTimeIsCurrent(result, DateTime.UtcNow);
    }

    private static void AssertTimeIsCurrent(Variable result, DateTime reference)
    {
        Assert.AreEqual(Types.Text, result.Type);

        var actualTime = TimeSpan.ParseExact(
            (string)result.Value!,
            @"hh\:mm\:ss",
            CultureInfo.InvariantCulture);
        var difference = Math.Abs((actualTime - reference.TimeOfDay).TotalSeconds);
        difference = Math.Min(difference, TimeSpan.FromDays(1).TotalSeconds - difference);

        Assert.IsTrue(difference <= 2, $"Expected a time close to {reference:HH:mm:ss}, but got {actualTime}.");
    }
}
