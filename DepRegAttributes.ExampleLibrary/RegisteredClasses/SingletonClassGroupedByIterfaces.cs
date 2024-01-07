namespace DepRegAttributes.ExampleLibrary.RegisteredClasses;

public interface ISingletonGroup1 { }
public interface ISingletonGroup1_2 { }

public interface ISingletonGroup2 { }
public interface ISingletonGroup2_2 { }

[RegisterSingleton<ISingletonGroup1, ISingletonGroup1_2>]
[RegisterSingleton<ISingletonGroup2, ISingletonGroup2_2>]
public class SingletonClassGroupedByIterfaces: 
    ISingletonGroup1, 
    ISingletonGroup1_2, 
    ISingletonGroup2, 
    ISingletonGroup2_2
{
}
