using DepRegAttributes.ExampleLibrary;
using DepRegAttributes.ExampleLibrary.RegisteredClasses.Singletons;
using DepRegAttributes.ExampleLibrary.RegisteredClasses.Transients;
using Microsoft.Extensions.DependencyInjection;

//This is where we build our actual service provider
IServiceProvider serviceProvider = new ServiceCollection()
    .AddExampleLibraryRegistration() //Add the services from your extention
    .BuildServiceProvider();

TransientClassRegisteredAsSelf classRegisteredAsSelf = serviceProvider.GetRequiredService<TransientClassRegisteredAsSelf>();
TransientClassRegisteredAsSelf classRegisteredAsSelf2 = serviceProvider.GetRequiredService<TransientClassRegisteredAsSelf>();
classRegisteredAsSelf.WriteSomething();
classRegisteredAsSelf2.WriteSomething();

ITransientClassWithMultipleIntefaces multipleIntefaceClass = serviceProvider.GetRequiredService<ITransientClassWithMultipleIntefaces>();
ITransientClassWithMultipleIntefaces2 multipleIntefaceClass2 = serviceProvider.GetRequiredService<ITransientClassWithMultipleIntefaces2>();
multipleIntefaceClass.WriteSomething();
multipleIntefaceClass2.WriteSomethingAgain();

SingletonClassRegigisteredAsSelf singletonClass = serviceProvider.GetRequiredService<SingletonClassRegigisteredAsSelf>();
SingletonClassRegigisteredAsSelf singletonClass2 = serviceProvider.GetRequiredService<SingletonClassRegigisteredAsSelf>();
singletonClass.WriteSomething();
singletonClass2.WriteSomething();

ISingletonClassWithMultipleInterfaces multipleInterfaceSingletonClass = serviceProvider.GetRequiredService<ISingletonClassWithMultipleInterfaces>();
ISingletonClassWithMultipleInterfaces2 multipleInterfaceSingletonClass2 = serviceProvider.GetRequiredService<ISingletonClassWithMultipleInterfaces2>();
multipleInterfaceSingletonClass.WriteSomething();
multipleInterfaceSingletonClass2.WriteSomethingAgain();