using DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;
namespace DepRegAttributes.Tests;

[TestClass]
public class TaggedRegistrationTests : UnitTestBase
{
    [TestMethod]
    public void GetTaggedOneTest()
    {
        //Arrange
        var sut = CreateSut("One");
        
        //Act
        var one = sut.GetService<TaggedOne>();
        var two = sut.GetService<TaggedTwo>();
        var oneTwo = sut.GetService<TaggedOneTwo>();
        var interfaceOneTwo = sut.GetService<ITaggedOneTwoWithInterface>();

        //Assert
        Assert.IsNotNull(one);
        Assert.IsNull(two);
        Assert.IsNotNull(oneTwo);
        Assert.IsNotNull(interfaceOneTwo);
    }

    [TestMethod]
    public void GetTaggedTwoTest()
    {
        //Arrange
        var sut = CreateSut("Two");

        //Act
        var one = sut.GetService<TaggedOne>();
        var two = sut.GetService<TaggedTwo>();
        var oneTwo = sut.GetService<TaggedOneTwo>();
        var interfaceOneTwo = sut.GetService<ITaggedOneTwoWithInterface>();

        //Assert
        Assert.IsNull(one);
        Assert.IsNotNull(two);
        Assert.IsNotNull(oneTwo);
        Assert.IsNotNull(interfaceOneTwo);
    }

    [TestMethod]
    public void GetMultiTaggedTest()
    {
        //Arrange
        var sut1 = CreateSut("One");
        var sut2 = CreateSut("Two");

        //Act
        var untagged1 = sut1.GetService<TaggedOneTwo>();
        var untagged2 = sut2.GetService<TaggedOneTwo>();
        var interfaceOneTwo1 = sut1.GetService<ITaggedOneTwoWithInterface>();
        var interfaceOneTwo2 = sut2.GetService<ITaggedOneTwoWithInterface>();

        //Assert
        Assert.IsNotNull(untagged1);
        Assert.IsNotNull(untagged2);
        Assert.IsNotNull(interfaceOneTwo1);
        Assert.IsNotNull(interfaceOneTwo2);
    }

    [TestMethod]
    public void GetUntaggedTest()
    {
        //Arrange
        var sut1 = CreateSut("One");
        var sut2 = CreateSut("Two");

        //Act
        var untagged1 = sut1.GetService<TaggedNothing>();
        var untagged2 = sut2.GetService<TaggedNothing>();

        //Assert
        Assert.IsNotNull(untagged1);
        Assert.IsNotNull(untagged2);
    }

    [TestMethod]
    public void GetEnumTagged()
    {
        //Arrange
        var sut = CreateSut(TagEnum.TagExample);

        //Act
        var service = sut.GetService<TaggedWithEnum>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetConstTagged()
    {
        //Arrange
        var sut = CreateSut(Const.Value);

        //Act
        var service = sut.GetService<TaggedWithConst>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetIntTagged()
    {
        //Arrange
        var sut = CreateSut(100);

        //Act
        var service = sut.GetService<TaggedWithInt>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetInternalConstTagged()
    {
        //Arrange
        var sut = CreateSut(TaggedWithInternalConst.Value);

        //Act
        var service = sut.GetService<TaggedWithInternalConst>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetInternalPrivateConstTagged()
    {
        //Arrange
        var sut = CreateSut("Value");

        //Act
        var service = sut.GetService<TaggedWithInternalPrivateConst>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetArrayTagged()
    {
        //Arrange
        var sut = CreateSut(new[] { "Value1", "Value2" });
        //Note, this is possible, but you will not be able to get any services tagged this way

        //Act
        var service = sut.GetService<TaggedWithArray>();

        //Assert
        Assert.IsNull(service);
    }

    [TestMethod]
    public void GetTypeTagged()
    {
        //Arrange
        var sut = CreateSut(typeof(TypeTag));

        //Act
        var service = sut.GetService<ITaggedWithType>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void EnsureTaggedNotPresentInUntaggerRegistration()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var one = sut.GetService<TaggedOne>();
        var two = sut.GetService<TaggedTwo>();
        var oneTwo = sut.GetService<TaggedOneTwo>();
        var untagged = sut.GetService<TaggedNothing>();
        var enumTag = sut.GetService<TaggedWithEnum>();
        var interfaceOneTwo = sut.GetService<ITaggedOneTwoWithInterface>();

        //Assert
        Assert.IsNull(one);
        Assert.IsNull(two);
        Assert.IsNull(oneTwo);
        Assert.IsNotNull(untagged);
        Assert.IsNull(interfaceOneTwo);
        Assert.IsNull(enumTag);
    }
}