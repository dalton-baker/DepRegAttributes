using DepRegAttributes.ExampleLibrary.RegisteredClasses.NestedServices;

namespace DepRegAttributes.Tests;

[TestClass]
public class NestedServicesTests : UnitTestBase
{
    [TestMethod]
    public void GetNestedServiceTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetService<ParentService.NestedService>();

        //Assert
        Assert.IsNotNull(service);
    }
    
    [TestMethod]
    public void GetNestedServiceWithInterfaceTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetService<ParentService.INestedServiceWithInterface>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetDoubleNestedServiceTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetService<ParentService.MiddleService.DoubleNestedService>();

        //Assert
        Assert.IsNotNull(service);
    }
}
