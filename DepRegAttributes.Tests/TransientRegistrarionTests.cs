using DepRegAttributes.ExampleLibrary.RegisteredClasses;
using DepRegAttributes.ExampleLibrary.Test;

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
        Assert.IsNotNull(transient1);
        Assert.IsNotNull(transient2);
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
        Assert.IsNotNull(transient1);
        Assert.IsNotNull(transient2);
        Assert.AreNotEqual(transient1, transient2);
    }

    [TestMethod]
    public void GetTransientFromMultipleInterfacesGenricTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var transient1 = sut.GetRequiredService<ITransientClassWithMultipleIntefacesGeneric>();
        var transient2 = sut.GetRequiredService<ITransientClassWithMultipleIntefacesGeneric2>();

        //Assert
        Assert.IsNotNull(transient1);
        Assert.IsNotNull(transient2);
        Assert.AreNotEqual(transient1, transient2);
    }

    [TestMethod]
    public void GetTransientFromSingleInterfaceTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var transient1 = sut.GetRequiredService<ITransientClassWithOneInterface>();
        var transient2 = sut.GetRequiredService<ITransientClassWithOneInterface>();

        //Assert
        Assert.IsNotNull(transient1);
        Assert.IsNotNull(transient2);
        Assert.AreNotEqual(transient1, transient2);
    }


    [TestMethod]
    public void GetTransientWithTypedInterfaceTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var transient = sut.GetRequiredService<ITransientClassWithTypedInterface<string>>();

        //Assert
        Assert.IsNotNull(transient);
    }


    [TestMethod]
    public void GetTransientWithTypedInterfaceInOtherNamespaceTest()
    {
        //Arrange
        var sut = CreateSut();

        //Act
        var transient = sut.GetRequiredService<ITransientClassWithTypedInterface<ITypeInOtherNamespace>>();

        //Assert
        Assert.IsNotNull(transient);
    }
}