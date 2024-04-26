# Version 3
## Version 3.1.1
### Updates
 - Changing `AddByAttribute` extension take a single optional tag filter instead of a list of tag filters.
   - Was `AddByAttribute(params object[] tagFilters)`.
   - Now `AddByAttribute(object? tagFilter = null)`.
### Breaking Changes
 - When calling `AddByAttribute` only services with a specified tag filter will be registered. Previously it would register all untagged services when a tag was passed in.
   - This means you will need to call `AddByAttribute` multiple times if you are using tags. Once with no tag filter (this will register all untagged services), then once for each tag you want to register.
   - This change was made mainly to make tags easier to understand. Also, the `params` parameter didn't allow you to specify the order that tagged services were registered in. 
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