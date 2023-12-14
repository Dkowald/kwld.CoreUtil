 # Overview
A handly library for various core .NET code.

 ## File system extensions
 A number of extensions to make working with FileInfo and DirectoryInfo easier.

 Many of the extension are to make it easier to code with FileInfo / Directory info 
 without resorting to strings.

 There are also some basic 'missing pieces' to help, such as __Touch__.

 And some more complex helpers, like __Merge__ to sync files between folder trees.

 See [Features/FileSystem](Features/FileSystem.md) for details.
 
## Streams 
Helper to work with streams. 
Easier to read and write lines. 
A __Tee__ stream.

See [Features/Streams](Features/Streams.md) for details.

## Strings
Find / Split / Merge helpers to work with strings more naturally.
Helper to convert a string to a stream.

See [Features/Strings](Features/Strings.md)

## Collections
Utilities for coding with collection.

Includes some IDictionary helpers, such as Merge.

And specialized collections, such as RecordArray,
to help with collections of record types.

See [Features/Collections](Features/Collections.md)

## Build
Helpers for msbuild.

__Global Using's__  
CoreUtll extensions included as global using's if 
the standard __ImplicitUsings__ property is set to 'enable'.
Optionally they can be explicitly controlled via the __ImplicitUsings_CoreUtil__ property;
```xml
<PropertyGroup>
 <!--By default; also enabled use of CoreUtl global usings-->
 <ImplicitUsings>enable</ImplicitUsings>
</PropertyGroup>

<PropertyGroup>
 <!--using off; no CoreUtil global usings-->
 <ImplicitUsings>disable</ImplicitUsings>
</PropertyGroup>

<PropertyGroup>
 <!--usings off, but override to include CoreUtl-->
 <ImplicitUsings>disable</ImplicitUsings>
 <ImplicitUsings_CoreUtil>enable</ImplicitUsings_CoreUtil>
</PropertyGroup>
```
**NOTE:** this ONLY works where the project directly includes this package.

__DownloadFile__ : easy support for downloading a file from a url

See [Features/Build](Features/Build.md) for details.