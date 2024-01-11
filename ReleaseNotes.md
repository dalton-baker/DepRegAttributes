# Version 3
## Version 3.1.0
### Updates
 - Targeting .NET Standard 2.0.
 - Adding `Tag` property to all register attributes.
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