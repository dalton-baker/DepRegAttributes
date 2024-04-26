namespace DepRegAttributes.ExampleLibrary.RegisteredClasses.TaggedServices;

public interface IOverridableService
{
}

[RegisterTransient<IOverridableService>]
public class OverridableUntagged : IOverridableService
{
}

[RegisterTransient<IOverridableService>(Tag = "Override")]
public class OverridableTagged : IOverridableService
{
}
