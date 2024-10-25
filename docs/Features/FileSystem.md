# Overview
A set of extensions to help working with the file system.

These work with both System.IO types and 
[System.IO.Abstractions](https://github.com/TestableIO/System.IO.Abstractions)

[<-Home](../Home.md)

## Bread and butter
While using FileInfo and DirectoryInfo; often have to revert back to strings
to use other file system methods.

For example
``` cs
FileInfo file;
System.IO.File.AppendAllText(file.FullName, "some data");
```

These extension improve readability by working directly with the 
IFileInfo object.
```cs
FileInfo file;
file.AppendAllText("some data");
```

In addition, the extensions are also implemented with
[System.IO.Abstractions](https://github.com/TestableIO/System.IO.Abstractions)
```cs
System.IO.IFileInfo file;
file.AppendAllText("some data");
```

The extensions are grouped by the static System.IO.XXX they map in.

**FromFileExtensions.cs** 
maps System.IO.File methods to corresponding 
FileInfo and DirectoryInfo extensions.

**FromDirectoryExtensions.cs**
maps System.IO.Directory to corresponding DirectoryInfo extensions.
e.g Directory.SetCurrentDirectory maps naturally to DirectoryInfo.SetCurrentDirectory

**FromPathExtensions.cs**
Map System.IO.Path methods to FileInfo and DirectoryInfo extension. e.g
Path.GetExtension map to FileInfo.GetExtension.

## Helper Extensions
A mixed bag of simple helpers. 

| Extension | Description |
| --------- | ----------- |
| For **DirectoryInfo**|
|Touch    | Update the LastWriteTime of a directory         |
|GetFile  | Get a file using a series of sub-path strings   |
|GetFolder| Get a folder using a series of sub path strings |
|MoveTo   | Overloads for MoveTo                            |
| For **FileInfo**|
|Touch | Update the LastWriteTime of a file |
| For **FileSystemInfo**|
|Exists() | Calls refresh, then returns result from Exist |
|AsUri()    | Converts item to file:// Uri. Directories have a trailing '/'  |

## Overload Extensions
These provide overload's on standard FileInfo / DirectoryInfo
methods so they take FileInfo or DirectoryInfo objects rather than 
strings.

The Destination item is refreshed and returned.

If needed; destination directory is created.

| Extension | Description |
| --------- | ----------- |
| For **FileInfo** |
| CopyTo | Copy file over another, or into a directory (creates parent directories as needed) |
| MoveTo | Move source file to target file or target directory  (optional overwrite) |
| Replace | Replace destination with source file, optional backup destination and ignoreErrors|
| For **DirectoryInfo**|
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
| For **FileInfo** and **DirectoryInfo**|
|EnsureDelete| Delete _file_ or _directory_; return refreshed object       |
|EnsureExists| Create _file_ or _directory_; return refreshed object       |
|EnsureEmpty | Ensures _file_ or _directory_ exists as is currently empty |
|EnsureDirectory| Ensures the _directory_ for a file exists |

## TreeExtensions
Some extensions that operate on items in a Directory

| Extension | Description |
| --- | --- |
| **DirectoryInfo**|
|Prune| Recursive remove empty directories |
| TreeDiff | Compare two directories returning the **C**reated **U**pdated and **D**eleted matching relative paths.|
| TreeSameFiles | Find files in other directory that have same relative path |
| Merge| Copy new (and optional updated) files from source to current directory. |
| MergeForce | Merge that copies all files from source to target directory. |

## Resolve Path
Things to help with path strings inside file system elements.

Includes an approach to determine if the file system is cases sensitive

| Extension      | Description |
| -------------- | ----------- |
| IsCaseSensitive| Test for case-sensitivity for the given test folder.|
| PathSplit      | Split a file system path string into its segments |
| FindFolder     | Navigate a sub path; replacing each path segment with same-case as found on file system (if found). |
| FindFile       | FindFolder, and include last item as a file name |
| Expand         | Expand the path, resolving relative path segments, and replacing environment variables. |

### case sensitive checking
The case sensitive check is not fancy. 
You provide a directory path that exists and has a letter in it.
It then creates either an all upper case or all lower case path based on 
if the test path has an upper case character.
If that path exists.. then its (like 99%) a case-ignorant file system.

## Directories
Helpers for commonly used paths.

| Extension | Description |
| --------- | ----------- |
| Current       | Exceuting Process' Current directory |
| Home          | Current user home path (windows or linux)  |
| Temp          | Users temporary folder.|
| TempFile      | Create a temporary File  |
| AssemblyFolder| Folder containing assembly for a type |
| Project       | Codes standard source project folder |

## Disposable
Small disposable wrappers.

|Class | Description |
|----|----|
|PushD | Set the current directory, reverting on dispose. |
|TempFile | Define a file or folder as temporary, deleting on dispose.|