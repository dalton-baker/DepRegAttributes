namespace DepRegAttributes.Tests;

[TestClass]
public class OpenGenericTests : UnitTestBase
{
    [TestMethod]
    public void GetGenericTransientTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var transientInt = sut.GetRequiredService<TransientOpenGeneric<int>>();
        var transientString = sut.GetRequiredService<TransientOpenGeneric<string>>();
        var transientLong = sut.GetRequiredService<TransientOpenGeneric<long>>();

        //Assert
        Assert.IsNotNull(transientInt);
        Assert.IsNotNull(transientString);
        Assert.IsNotNull(transientLong);
    }

    [TestMethod]
    public void GetGenericTransientInterfaceTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var transientInt = sut.GetRequiredService<ITransientOpenGeneric<int>>();
        var transientString = sut.GetRequiredService<ITransientOpenGeneric<string>>();
        var transientLong = sut.GetRequiredService<ITransientOpenGeneric<long>>();

        //Assert
        Assert.IsNotNull(transientInt);
        Assert.IsNotNull(transientString);
        Assert.IsNotNull(transientLong);
    }

    [TestMethod]
    public void GetMutipleGenericTransientTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var transientInt = sut.GetRequiredService<TransientOpenMultipleGeneric<int, string>>();
        var transientString = sut.GetRequiredService<TransientOpenMultipleGeneric<string, string>>();
        var transientLong = sut.GetRequiredService<TransientOpenMultipleGeneric<long, string>>();

        //Assert
        Assert.IsNotNull(transientInt);
        Assert.IsNotNull(transientString);
        Assert.IsNotNull(transientLong);
    }

    [TestMethod]
    public void GetMutipleGenericTransientInterfaceTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var transientInt = sut.GetRequiredService<ITransientOpenMultipleGeneric<int, string>>();
        var transientString = sut.GetRequiredService<ITransientOpenMultipleGeneric<string, string>>();
        var transientLong = sut.GetRequiredService<ITransientOpenMultipleGeneric<long, string>>();

        //Assert
        Assert.IsNotNull(transientInt);
        Assert.IsNotNull(transientString);
        Assert.IsNotNull(transientLong);
    }
}
