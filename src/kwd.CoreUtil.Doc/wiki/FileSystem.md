# Overview
A set of extensions to help working with the file system.

## Bread and butter
While using FileInfo and DirectoryInfo, I continually found myself 
reverting back to using strings for full paths etc.

These extensions started so I could leverage FileInfo and DirectoryInfo objects;
rather than string paths.

For example
```cs
using System.IO;

var file = new FileInfo("c:/tmp");

//Using System.IO.File utility
var text = File.ReadAllText(file.FullName);
Path.ChangeExtension(text.FullName, ".xxx");

//Using System.IO.FileInfo extension
var text2 = file.ReadAllText();
file.ChangeExtension(".xxx");
```
**FromFileExtensions** 
maps System.IO.File methods to corresponding 
FileInfo and DirectoryInfo extensions.

**FromDirectoryExtensions**
maps System.IO.Directory to corresponding DirectoryInfo extensions.
e.g Directory.SetCurrentDirectory maps naturally to DirectoryInfo.SetCurrentDirectory

**FromPathExtensions**
Map System.IO.Path methods to FileInfo and DirectoryInfo extension. e.g
Path.GetExtension map to FileInfo.GetExtension.

## Helper Extensions
A mixed bag of simple helpers. 

| Extension | Description |
| --------- | ----------- |
| **DirectoryInfo**|
|Touch    | Update the LastWriteTime of a directory |
|GetFile  | Get a file using a series of sub-path strings |
|GetFolder| Get a folder using a series of sub path strings |
| **FileInfo**|
|Touch | Update the LastWriteTime of a file |
| **FileSystemInfo**|
|Exists() | Calls refresh, then returns result from Exist |
|AsUri    | Converts item to Uri. Directories have a trailing '/'  |

## Override Extensions
These provide override on standard FileInfo / Directory info
methods so they take FileInfo or DirectoryInfo objects rather than 
strings.

The Destination item is refreshed and returned.

If needed, destination directory is created (if needed).

e.g 
FileInfo.MoveTo

| Extension | Description |
| --------- | ----------- |
| **FileInfo** |
| CopyTo | Copy file over another, or into a directory (creates parent directories if needed) |
| MoveTo | Move source file to target file (optional overwrite) or target directory |
| Replace | Replace destination with source file, optional backup destination and ignoreErrors|
| **DirectoryInfo**|
| MoveTo | Move source directory to target directory |

## Chain Extensions
Methods to provide a chaining style interface for some operations.

After performing the action (create / delete);
these extension(s) use Refresh() and return the source object.

Making a chainable method to improve code readability.
```c#
var f = new FileInfo("c:/temp/Me").EnsureExists();
Assert.IsTrue(f.Exists);
``` 

| Extension | Description |
| --------- | ----------- |
|EnsureDelete| Delete file / directory; return refreshed object |
|EnsureExists| Create file / directory; return refreshed object |

## TreeExtensions
Some extensions that operator on items in a Directory

| Extension | Description |
| --- | --- |
|Prune| Recursive remove empty directories |
| TreeCUD | Compare two directories returning the **C**reated **U**pdated and **D**eleted matching relative paths.|
| TreeSameFiles | Find files in other directory that have same relative path|
| Merge| Copy new (and optional updated) files from source to current directory. |
| MergeForce | Merge that copies all files from source to target directory. |

## Resolve Path
Case awareness for file system. 

A set of **DirectoryInfo** extensions, to help resolve path to a File System object.

| Extension | Description |
| --------- | ----------- |
| IsCaseSensitive| Test for case-sensitivity for the given test folder.|
| PathSplit | Split a file system path string into its segments |
| FindFolder | Navigate a sub path; replacing each path segment with same-case as found on file system (if found). |
| FindFile | FindFolder, and include last item as a file name |

## Directories
Helpers for oft used paths.

| Extension | Description |
| --------- | ----------- |
| Current | Process Current directory |
| Home    | Current user home path (windows or linux)  |
| Temp    | Users temporary folder.|
| AssemblyFolder| Folder containing assembly for a type |
