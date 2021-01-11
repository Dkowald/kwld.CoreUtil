 # Overview
 For a while I've been using [GitPackage](https://github.com/Dkowald/GitPackage) to manage a set of handy extension I use for various projects.

 Over time my code has out grown this simple approach,
 hense this nuget package.

 ## File system extensions
 A number of extensions to make working with FileInfo and DirectoryInfo easier.

 Many of the extension are to make it easier to code with FileInfo / Directory info 
 without resorting to strings.

 There are also some basic 'missing pieces' to help, such as __Touch__.

 And some more complex helpers, like __Merge__ to sync files between folder trees.

 See [[FileSystem|FileSystem]] for details.
 
## Streams 
Helper to work with streams. 
Easier to read and write lines. 
A __Tee__ stream.

See [[Streams|Streams]] for details.

## Strings
Find / Split / Merge helpers to work with strings more naturally.
Helper to convert a string to a stream.

See [[Strings|Strings]] for details.
