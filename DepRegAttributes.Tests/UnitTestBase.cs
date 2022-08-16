namespace DepRegAttributes.Tests;

public abstract class UnitTestBase
{
    public TestContext TestContext { get; set; } = null!;

    public IServiceProvider CreateSut(string? tag = null)
        => new ServiceCollection()
            .AddExampleLibraryRegistration(tag)
            .BuildServiceProvider();
}
