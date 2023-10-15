using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public void GetAllFromUnFilteredCollection()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var one = sut.GetService<TaggedOne>();
        var two = sut.GetService<TaggedTwo>();
        var oneTwo = sut.GetService<TaggedOneTwo>();
        var untagged = sut.GetService<TaggedNothing>();
        var interfaceOneTwo = sut.GetService<ITaggedOneTwoWithInterface>();

        //Assert
        Assert.IsNotNull(one);
        Assert.IsNotNull(two);
        Assert.IsNotNull(oneTwo);
        Assert.IsNotNull(untagged);
        Assert.IsNotNull(interfaceOneTwo);
    }
}