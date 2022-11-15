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
        var singleton = sut.GetRequiredService<SingletonClassRegigisteredAsSelf>();
        var singleton2 = sut.GetRequiredService<SingletonClassRegigisteredAsSelf>();

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
        var singleton1 = sut.GetRequiredService<SingletonClassRegigisteredAsSelf>();
        var singleton2 = sut.GetRequiredService<SingletonClassRegigisteredAsSelf>();

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
