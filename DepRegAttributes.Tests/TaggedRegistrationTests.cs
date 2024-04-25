namespace DepRegAttributes.Tests;

[TestClass]
public class TaggedRegistrationTests : UnitTestBase
{
    [TestMethod]
    public void GetTaggedOneTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary("One")
            .BuildServiceProvider();
        
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
        var sut = new ServiceCollection()
            .AddExampleLibrary("Two")
            .BuildServiceProvider();

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
        var sut1 = new ServiceCollection()
            .AddExampleLibrary("One")
            .BuildServiceProvider();

        var sut2 = new ServiceCollection()
            .AddExampleLibrary("Two")
            .BuildServiceProvider();

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
    public void GetEnumTagged()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary(TagEnum.TagExample)
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<TaggedWithEnum>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetConstTagged()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary(Const.Value)
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<TaggedWithConst>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetIntTagged()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary(100)
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<TaggedWithInt>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetInternalConstTagged()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary(TaggedWithInternalConst.Value)
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<TaggedWithInternalConst>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetInternalPrivateConstTagged()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary("Value")
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<TaggedWithInternalPrivateConst>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetArrayTagged()
    {
        //Note, this is possible, but you will not be able to get any services tagged this way
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary(new[] { "Value1", "Value2" })
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<TaggedWithArray>();

        //Assert
        Assert.IsNull(service);
    }

    [TestMethod]
    public void GetTypeTagged()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary(typeof(TypeTag))
            .BuildServiceProvider();

        //Act
        var service = sut.GetService<ITaggedWithType>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void EnsureTaggedNotPresentInUnfilteredRegistration()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

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

    [TestMethod]
    public void EnsureUtaggedNotPresentInFilteredRegistration()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary("Misc. Tag")
            .BuildServiceProvider();

        //Act
        var untagged = sut.GetService<TaggedNothing>();
        var transient = sut.GetService<ITransientClassWithOneInterface>();

        //Assert
        Assert.IsNull(untagged);
        Assert.IsNull(transient);
    }

    [TestMethod]
    public void RegisterTaggedAndUntaggedTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .AddExampleLibrary("One")
            .BuildServiceProvider();

        //Act
        var one = sut.GetService<TaggedOne>();
        var oneTwo = sut.GetService<TaggedOneTwo>();
        var untagged = sut.GetService<TaggedNothing>();
        var transient = sut.GetService<ITransientClassWithOneInterface>();

        //Assert
        Assert.IsNotNull(one);
        Assert.IsNotNull(oneTwo);
        Assert.IsNotNull(untagged);
        Assert.IsNotNull(transient);
    }

    [TestMethod]
    public void OverrideByTaggedServiceTest()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .AddExampleLibrary("Override")
            .BuildServiceProvider();

        //Act
        var overrodeService = sut.GetService<IOverridableService>();

        //Assert
        Assert.IsInstanceOfType(overrodeService, typeof(OverridableTagged));
        Assert.IsNotInstanceOfType(overrodeService, typeof(OverridableUntagged));
    }

    [TestMethod]
    public void EnsureOverridableServiceExists()
    {
        //Arrange
        var sut = new ServiceCollection()
            .AddExampleLibrary()
            .BuildServiceProvider();

        //Act
        var overrodeService = sut.GetService<IOverridableService>();

        //Assert
        Assert.IsNotInstanceOfType(overrodeService, typeof(OverridableTagged));
        Assert.IsInstanceOfType(overrodeService, typeof(OverridableUntagged));
    }


}