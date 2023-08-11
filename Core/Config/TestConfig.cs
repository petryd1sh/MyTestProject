namespace MyTestProject.Core;

public static class TestConfig
{
    public static string GetTestParameter(string parameterName)
    {
        return TestContext.Parameters[parameterName] ?? throw new TestParameterException(
            $"Unable to find {parameterName} in test config. Specify a config file or provide the test parameter.");
    }
}