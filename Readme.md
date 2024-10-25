## Overview
Core library holding various extensions to improve code development and readability.

See [Docs](docs/Home.md) for details

## Whats New
- Removing .net7.0 target (only maintaining supported versions)
- Better implementation for IDirectoryInfo.IsCaseSensitive()
- Renamed TreeCUD to TreeDiff
- IsCaseSensitive now available on Directory; Files and IFileSystem
## Features

### Global Usings
For easy use; all extensions are included as global usings (.net 6 +)  
By default the standard __ImplicitUsings__ property determines if they are 
  included or not.  
The can also be manually controlled via the __ImplicitUsings_CoreUtil__ build property.

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

### Collections.

Dictionary extensions to:
1.  Add a range, or merge.
2.  Ensure an item exists (DefaultTo)

A RecordArray for value-equality of a set of records.
(with array-like serialization)

### Build tooling.

A number of msbuild helpers, including:

Download file as part of the build.

---
^ [source](https://github.com/Dkowald/kwld.CoreUtil) | [nuget](https://www.nuget.org/packages/kwld.CoreUtil/)
