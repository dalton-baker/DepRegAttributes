using Microsoft.Extensions.DependencyInjection;

namespace DepRegAttributes.ExampleLibrary
{
    //You need to write a service collection extention in the
    //project you want to be registered. This will automatically
    //detect your assembly and load all of the registered classes.
    //YOU ONLY NEED TO ADD THIS ONCE FOR A SINGLE PROJECT
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddExampleLibrary(this IServiceCollection services, object? tagFilter = null)
        {
            //This will automatically load all dependancies of the
            //assembly we are currently in. Alternatively you can
            //call this once and pass it all of the assemblies you
            //want to load
            return services.AddByAttribute(tagFilter);
        }
    }
}