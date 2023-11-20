# Overview
Extension to help working with .NET Streams

| Name  | Description |
| ---  | :---: |
| TeeStream | Write to multiple streams at once |
| Text Read Write | Sugar for text writer and reader |

[<-Home](../Home.md)  

## TeeStream
Named after the unix Tee command;
this writes data to multiple streams at once.

Read and seek operations not supported.

## TextWriterExtensions & TextReaderExtensions
A handfull of helper to make working with TextWrite
more natural.

> WriteLines - a multi-line version of WriteLine.

> ReadLines - a multi-line version of ReadLine.

Note: readlines uses a IAsyncEnumerable (C#8),
so you may also want __[System.Linq.Async](https://www.nuget.org/packages/System.Linq.Async/)__