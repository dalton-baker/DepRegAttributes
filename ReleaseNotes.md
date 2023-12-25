### Version 7.0.0 
 - Rewrote library to use source generation instead of reflection. *higher chance of bugs*
 - Changed tag type from `string` to `object`.
 - Generic arguments are now available in C# 11 langage version, instead of .NET 7.0.
 - Adding `AddByAttribute()` as an alternative to `RegisterDependenciesByAttribute()`. This is to align more with other common Service Collection extensions.
#### Breaking Changes:
 - Tags are no longer a constructor argument, instead use "Tag" property.
 - Only one tag is allowed per attribute, before it was unlimited.
 - Previously if you called `AddByAttribute()` with no tag argument, all services for every tag would be added. This is no longer the case, you will only get services for tags you pass into service collection extension.
 - Removing extensibility (i.e. ObjectFactory). This was not useful and extremely complex with source generation.

### Version 6.0.0
 - Adding `ObjectFactory` property in attribute base for extensibility

### Version 5.0.0
 - Adding multi-targeting for .NET 7.0 and .NET Standard 2.0.
 - Adding generic attribute arguments for .NET 7.0 target.

### Version 4.0.0
 - Targeting .NET Standard 2.0 for wider compatibility.

### Version 3.0.0
 - Automatically registering by matching interface name.