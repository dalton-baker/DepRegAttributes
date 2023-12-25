using DepRegAttributes.ExampleLibrary.RegisteredClasses.ExternalReferences;
using DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;
using DepRegAttributes.ExternalExampleLibrary;
using DepRegAttributes.ExternalExampleLibrary.SubNamespace;
namespace DepRegAttributes.Tests;

[TestClass]
public class ExternalResourceTests : UnitTestBase
{
    [TestMethod]
    public void GetFromExternalInterface()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetService<IExternalInterface>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetFromExternalInterfaceWithNamespace()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetService<IExternalInterfaceInNamespace>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetTaggedWithExternalenum()
    {
        //Arrange
        var sut = CreateSut(ExternalEnum.Value);

        //Act
        var service = sut.GetService<TaggedWithExternalEnum>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetTaggedWithExternalConst()
    {
        //Arrange
        var sut = CreateSut(ExternalConst.Value);

        //Act
        var service = sut.GetService<TaggedWithExternalConst>();

        //Assert
        Assert.IsNotNull(service);
    }
}
