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

        //Assert
        Assert.IsNotNull(singleton);
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
        Assert.AreEqual(singleton1, singleton2);
    }
}
