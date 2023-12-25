namespace DepRegAttributes.Tests;

public abstract class UnitTestBase
{
    public TestContext TestContext { get; set; } = null!;

    public IServiceProvider CreateSut(params object[] includeTags)
        => new ServiceCollection()
            .AddExampleLibraryRegistration(includeTags)
            .BuildServiceProvider();
}
