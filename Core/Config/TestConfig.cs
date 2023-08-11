namespace MyTestProject.Core.Config;

public static class TestConfig
{
    public static string GetTestRunParameter(string parameterName)
    {
        return TestContext.Parameters[parameterName] ?? throw new TestParameterException(
            $"Unable to find {parameterName} in test config. Specify a config file or provide the test parameter.");
    }
    
    public static string GetEnvironmentVariable(string parameterName)
    {
        return Environment.GetEnvironmentVariable(parameterName) ?? throw new TestParameterException(
            $"Unable to find {parameterName} in test config. Specify a config file or provide the test parameter.");
    }
}