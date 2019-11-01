# Overview
This set of extension started because I prefere to use an
object rather than a string when working with file paths.

While using FileInfo and DirectoryInfo, I continually found myself 
reverting back to using strings.

Here are a number of extensions that should make it eaiser to work with 
FileInfo and DirectoryInfo obejcts.

## FileInfoFileExtensions
Map System.IO.File and System.IO.Directory methods to corresponding 
FileInfo and Directory info extensions.

Using these extensions, If you can do it with System.IO.File; you can do it with 
FileInfo. e.g. File.AppendAllLines has a corresponding FileInfo extension.

## FileInfoPathExtensions
Map System.IO.Path methods to FileInfo and DirectoryInfo extension.

With this methods such as Path.GetExtension map to corresponding FileInfo extension.

## DirectoryInfoDirectoryExtensions
Map System.IO.Directory to corresponding DirectoryInfo extensions.
e.g Directory.SetCurrentDirectory maps naturally to DirectoryInfo.SetCurrentDirectory

## FileDirectoryExtensions
Some System.IO.File methods map naturally to use both FileInfo and DirectoryInfo

FileInfo.MoveTo(DirectoryInfo) - Move file to specified directory.

## FileSystemInfoExtensions
Generally usefull extensions for File system objects.
Common for both Files and directory objects.

**Touch** 
Create item (if missing) and update LastWrite to latest. 
Optionally set LastWrite to particular value.

**EnsureDelete**
Delete the item from the file system; 
safe if it doesnt currently exist.

**Exists()**
Combines Refresh() with Exists to give a more acurate 
tests for existance.

**AsUri**
Create Uri for the given file system object.
Always appends a '/' for folders. 

## FileInfoExtensions
Generally usefull extension particular to a file.

**EnsureCreate()**
Create the file, if it doesnt exist.
Will cerate directory structure if needed.

## DirectoryInfoExtensions
Generally useful extensions particular to a Directory

**GetFile**
Get child file inside directory. 

**GetFolder**
Get child directory.
