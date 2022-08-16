using DepRegAttributes.ExampleLibrary.RegisteredClasses;

namespace DepRegAttributes.Tests;

[TestClass]
public class TransientRegistrarionTests : UnitTestBase
{
    [TestMethod]
    public void GetTransientTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var transient = sut.GetRequiredService<TransientClassRegisteredAsSelf>();

        //Assert
        Assert.IsNotNull(transient);
    }

    [TestMethod]
    public void GetMultipleTransientsTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var transient1 = sut.GetRequiredService<TransientClassRegisteredAsSelf>();
        var transient2 = sut.GetRequiredService<TransientClassRegisteredAsSelf>();

        //Assert
        Assert.AreNotEqual(transient1, transient2);
    }

    [TestMethod]
    public void GetTransientFromMultipleInterfacesTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var transient1 = sut.GetRequiredService<ITransientClassWithMultipleIntefaces>();
        var transient2 = sut.GetRequiredService<ITransientClassWithMultipleIntefaces2>();

        //Assert
        Assert.AreNotEqual(transient1, transient2);
    }
}