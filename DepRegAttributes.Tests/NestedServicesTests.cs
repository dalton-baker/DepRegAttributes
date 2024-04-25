namespace DepRegAttributes.Tests;

[TestClass]
public class NestedServicesTests : UnitTestBase
{
    [TestMethod]
    public void GetNestedServiceTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<ParentService.NestedService>();

        //Assert
        Assert.IsNotNull(service);
    }
    
    [TestMethod]
    public void GetNestedServiceWithInterfaceTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<ParentService.INestedServiceWithInterface>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetDoubleNestedServiceTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<ParentService.MiddleService.DoubleNestedService>();

        //Assert
        Assert.IsNotNull(service);
    }
}
