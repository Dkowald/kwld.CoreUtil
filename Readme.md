## Overview
Core library holding various extensions to improve code development and readability.

### Framework support 
To leverage new language features, this now targets multiple frameworks.

Some features are only available for newer frameworks.

### Helpers for file system.

Mapping from static file system methods, such as _Path.ChangeExtension()_
to corresponding FileInfo (and DirectoryInfo) extensions, 
such as _FileInfo.ChangeExtension()_

Includes set of extensions for [System.IO.Abstractions](https://github.com/TestableIO/System.IO.Abstractions)
such as _IFileInfo.ChangeExtension()_

A number of other file-system goodies like Touch(); EnsureExists()

### Stream helpers

Simpler Read/Write lines to text stream.

A stream Tee.

### String helpers
String split and join helpers.

Split on whitespace (find words)

Compare case-ignorant, and white-space ignorant (Same).

### Dictionary helpers.
Add or merge 2 dictonaries.

Ensure an item exists (DefaultTo)

### Build tooling.

A number of msbuild helpers, including:

Download file as part of the build.


See [wiki](https://github.com/Dkowald/kwd.CoreUtil/wiki/) for details

---
^ v1.2.5 [source](https://github.com/Dkowald/kwd.CoreUtil) | [nuget](https://www.nuget.org/packages/kwd.CoreUtil/)