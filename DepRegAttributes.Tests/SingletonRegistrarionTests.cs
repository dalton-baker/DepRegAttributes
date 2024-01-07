namespace DepRegAttributes.Tests;

[TestClass]
public class SingletonRegistrarionTests : UnitTestBase
{
    [TestMethod]
    public void GetSingletonTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var singleton = sut.GetRequiredService<SingletonClassRegisteredAsSelf>();
        var singleton2 = sut.GetRequiredService<SingletonClassRegisteredAsSelf>();

        //Assert
        Assert.IsNotNull(singleton);
        Assert.AreEqual(singleton, singleton2);
    }

    [TestMethod]
    public void GetMultipleSingletonsTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var singleton1 = sut.GetRequiredService<SingletonClassRegisteredAsSelf>();
        var singleton2 = sut.GetRequiredService<SingletonClassRegisteredAsSelf>();

        //Assert
        Assert.IsNotNull(singleton1);
        Assert.IsNotNull(singleton2);
        Assert.AreEqual(singleton1, singleton2);
    }

    [TestMethod]
    public void GetSingletonFromMultipleInterfacesTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var singleton1 = sut.GetRequiredService<ISingletonClassWithMultipleInterfaces>();
        var singleton2 = sut.GetRequiredService<ISingletonClassWithMultipleInterfaces2>();

        //Assert
        Assert.IsNotNull(singleton1);
        Assert.IsNotNull(singleton2);
        Assert.AreEqual(singleton1, singleton2);
    }

    [TestMethod]
    public void GetSingletonFromMultipleInterfacesGenericTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var singleton1 = sut.GetRequiredService<ISingletonClassWithMultipleInterfacesGeneric>();
        var singleton2 = sut.GetRequiredService<ISingletonClassWithMultipleInterfacesGeneric2>();

        //Assert
        Assert.IsNotNull(singleton1);
        Assert.IsNotNull(singleton2);
        Assert.AreEqual(singleton1, singleton2);
    }

    [TestMethod]
    public void GetSingletonGroupsTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var singletonGroup1 = sut.GetRequiredService<ISingletonGroup1>();
        var singletonGroup1_2 = sut.GetRequiredService<ISingletonGroup1_2>();
        var singletonGroup2 = sut.GetRequiredService<ISingletonGroup2>();
        var singletonGroup2_2 = sut.GetRequiredService<ISingletonGroup2_2>();

        //Assert
        Assert.IsNotNull(singletonGroup1);
        Assert.IsNotNull(singletonGroup1_2);
        Assert.AreEqual(singletonGroup1, singletonGroup1_2);

        Assert.IsNotNull(singletonGroup2);
        Assert.IsNotNull(singletonGroup2_2);
        Assert.AreEqual(singletonGroup2, singletonGroup2_2);

        Assert.AreNotEqual(singletonGroup1, singletonGroup2);
        Assert.AreNotEqual(singletonGroup1, singletonGroup2_2);
        Assert.AreNotEqual(singletonGroup1_2, singletonGroup2);
        Assert.AreNotEqual(singletonGroup1_2, singletonGroup2_2);
    }

    [TestMethod]
    public void GetSingletonFromSingleInterfaceTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var singleton1 = sut.GetRequiredService<ISingletonClassWithOneInterface>();
        var singleton2 = sut.GetRequiredService<ISingletonClassWithOneInterface>();

        //Assert
        Assert.IsNotNull(singleton1);
        Assert.IsNotNull(singleton2);
        Assert.AreEqual(singleton1, singleton2);
    }
}
