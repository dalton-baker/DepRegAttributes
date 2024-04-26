# Version 8

## Version 8.0.1
### Updates
 - Changing `AddByAttribute` extension take a single optional tag filter instead of a list of tag filters.
   - Was `AddByAttribute(params object[] tagFilters)`.
   - Now `AddByAttribute(object? tagFilter = null)`.

### Breaking Changes
 - When calling `AddByAttribute` only services with a specified tag filter will be registered. Previously it would register all untagged services when a tag was passed in.
   - This means you will need to call `AddByAttribute` multiple times if you are using tags. Once with no tag filter (this will register all untagged services), then once for each tag you want to register.
   - This change was made mainly to make tags easier to understand. Also, the `params` parameter didn't allow you to specify the order that tagged services were registered in. 

## Version 8.0.0
### Updates
 - Targeting .NET Standard 2.0.
 - Adding `Key` and `Tag` properties to all register attributes.
 - The dependency `Microsoft.Extensions.DependencyInjection.Abstractions` has been upgraded to version 8.0.0.
	- This was to include Keyed service support.
 - Adding `AddByAttribute()` service collection extensions to align more with other common naming convensions.
	- The previous extension `RegisterDependenciesByAttribute()` has been marked as obsolete.
 - Adding Analyzers
	- Errors when a service type is incompatible with the class you are trying to use it with.
	- Error when a class is invalid (i.e. abstract, static, no public constructors).
	- Warnings when a Key or Tag has a value that will result in a reference comparison.
### Breaking Changes
 - Tags have been dramatically altered.
	- These are no longer a constructor argument, instead they are a property.
	- Only one is allowed per attribute, previously it was unlimmited.
 - Service collection extensions have changed.
	- When you add services by attribute, you no longer pull in all tagged services when you don't include a tag.
	- You will only get tagged services when the tag is explicitly included
	- You get all untagged services even if you include a tag.
 - Removing extensibility (i.e. ObjectFactory). This was not useful and had very limited use cases.

# Version 7 (Deprecated)
## Version 7.0.7
 - Refactoring for readability and efficiency
 - Fixing analyzer bug, `int` and `string` generic arguments were not being recognized
 - Removing error analyzer rule for public and internal implementation and service types
 - Adding open generic support.
 - Adding lots of documentation.
 - Reverting project dependencies to Microsoft.Extensions.DependencyInjection.Abstractions, versions 3.1.32 and 8.0.0.

## Version 7.0.6
 - Reverting back to reflection based registration, removing code generation.
 - Multi-targeting netstandard2.0 and net8.0.
 - Updating project dependencies to Microsoft.Extensions.DependencyInjection, versions 3.1.32 and 8.0.0.
 - New versions of service collection extensions: `AddByAttribute(params object[] tags)` and `AddByAttribute(Assembly, params object[] tags)`

## Version 7.0.5
 - Adding project with real dll output to hold attributes.
 - Releasing attribute project, source generator, and analyzer as single project.
 - Moving service provider extensions to `{project assembly}.DepRegAttributes`

## Version 7.0.4
 - Fixing `typeof()` arguments for Key or Tag. These were being picked up and used as a service type.
 - Allowing internal classes to be registered.
 - Adding warning analyzer for Tags and Keys that are array initializers.
 - Adding support for generic service types.
 - Adding warning analyzer for unbound generics.
 - Updating namespace to the project assembly.

## Version 7.0.3
 - Fixing nested types, previously the parent class was not being included.
 - Adding analyzers for private nested types.
 - Fixing multii-project bug. Projects conflict eachother if generated code is in the same namespace. 
   - Fixed this by moving all generated code into the project assembly. 
   - More info here: https://andrewlock.net/creating-a-source-generator-part-7-solving-the-source-generator-marker-attribute-problem-part1/

## Version 7.0.2
 - Adding Keyed services support.
 - Added `Key` property on attributes when a project references Microsoft.Extensions.DependencyInjection version 8.0.0 or greater.

## Version 7.0.1
 - Fixing nuget package tags and target framework

## Version 7.0.0 
### Updates
 - Rewrote library to use source generation instead of reflection. *higher chance of bugs*
 - Added an analyzer that tells a developer when an attribute is invalid.
 - Changed tag type from `string` to `object`.
 - Generic arguments are now available in C# 11 langage version, instead of .NET 7.0.
 - Adding `AddByAttribute()` as an alternative to `RegisterDependenciesByAttribute()`. This is to align more with other common Service Collection extensions.
### Breaking Changes
 - Tags are no longer a constructor argument, instead use "Tag" property.
 - Only one tag is allowed per attribute, before it was unlimited.
 - Previously if you called `AddByAttribute()` with no tag argument, all services for every tag would be added. This is no longer the case, you will only get services for tags you pass into service collection extension.
 - Removing extensibility (i.e. ObjectFactory). This was not useful and extremely complex with source generation.

## Version 6 (Deprecated)
### Version 6.0.0
 - Adding `ObjectFactory` property in attribute base for extensibility

## Version 5 (Deprecated)
### Version 5.0.0
 - Adding multi-targeting for .NET 7.0 and .NET Standard 2.0.
 - Adding generic attribute arguments for .NET 7.0 target.

## Version 4 (Deprecated)
### Version 4.0.0
 - Targeting .NET Standard 2.0 for wider compatibility.

## Version 3 (Deprecated)
### Version 3.0.0
 - Automatically registering by matching interface name.