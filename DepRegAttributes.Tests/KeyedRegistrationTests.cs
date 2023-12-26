using DepRegAttributes.ExampleLibrary.RegisteredClasses.KeyedServices;
using DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;
namespace DepRegAttributes.Tests;

[TestClass]
public class KeyedRegistrationTests : UnitTestBase
{
    [TestMethod]
    public void GetKeyedSingletonWithMultipleInterfacesTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service1 = sut.GetKeyedService<IKeyedInterface>(Const.Value);
        var service2 = sut.GetKeyedService<IKeyedInterface2>(Const.Value);

        //Assert
        Assert.IsNotNull(service1);
        Assert.IsNotNull(service2);
        Assert.AreEqual(service1, service2);
    }

    [TestMethod]
    public void GetArrayKeyedTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetKeyedService<KeyedWithArray>(new[] { "Value1", "Value2" });

        //Assert
        Assert.IsNull(service);
    }

    [TestMethod]
    public void DontGetConstKeyedTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetService<KeyedWithConst>();

        //Assert
        Assert.IsNull(service);
    }

    [TestMethod]
    public void GetConstKeyedTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetKeyedService<KeyedWithConst>(Const.Value);

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetEnumKeyedTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetKeyedService<KeyedWithEnum>(TagEnum.TagExample);

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetIntKeyedTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetKeyedService<KeyedWithInt>(100);

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetKeyedWithInterfaceTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetKeyedService<IKeyedWithInterface>("Value");

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetKeyedWithInternalConstTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetKeyedService<KeyedWithInternalConst>(KeyedWithInternalConst.Value);

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetKeyedWithInternalPrivateConstTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetKeyedService<KeyedWithInternalPrivateConst>("Value");

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetStringKeyedTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetKeyedService<KeyedWithString>("Value");

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetKeyedAndTagged()
    {
        //Arrange
        var sut = CreateSut(TagEnum.TagExample);

        //Act
        var service = sut.GetKeyedService<KeyedAndTagged>(Const.Value);

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void DontGetKeyedAndTagged()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var service = sut.GetKeyedService<KeyedAndTagged>(Const.Value);

        //Assert
        Assert.IsNull(service);
    }
}
