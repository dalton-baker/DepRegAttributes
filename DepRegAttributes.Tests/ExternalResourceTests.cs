namespace DepRegAttributes.Tests;

[TestClass]
public class ExternalResourceTests : UnitTestBase
{
    [TestMethod]
    public void GetFromExternalInterface()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<IExternalInterface>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetFromExternalInterfaceWithNamespace()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<IExternalInterfaceInNamespace>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetTaggedWithExternalenum()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary(ExternalEnum.Value)
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<TaggedWithExternalEnum>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetTaggedWithExternalConst()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary(ExternalConst.Value)
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<TaggedWithExternalConst>();

        //Assert
        Assert.IsNotNull(service);
    }
}
