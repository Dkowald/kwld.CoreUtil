# Overview
Extensions to help working with strings.

Where applicable, Span\<char\> overrides are provided to avoid string allocations.

| Name  | Description |
| ---  | :---: |
| String Match  | Compare strings |
| String Find| Find pieces of a string |
| String Build | (re)Combine strings  |
| String Stream| Strings as streams |

[<-Home](../Home.md)  

## (NET6 or greater) DataString 
Implement pattern to wrap a string as DataString

Use a DataString pattern to create small classes that encapsulate parsing and generating strings.

A DatString class should be a relativly straight forward drop-in replacement for a string.

The pattern also supports STJ Json serialization.

To create a DataString implement IDataString (or IDataString<T> for .net 7+)
The interface includes a list of expectations for the DataString:
1. Should be a record type (immutable)
2. MUST provide a TryParse static member
3. MUST overload ToString() to return a value that could be used by TryParse
4. Should provide a constructor that takes ony a string.
5. Should provide an implicit cast to string.

To serialize a DataString (with STJ) include the DataStringConverterFactory

## Match
Extensions to help compare strings.

| Name  | Description |
| ---  | :---: |
|Same              | Compare two strings ignoring case and any leading / trailing white space.|
|SamePhrase        | Compare the words in 2 strings; with optional custom delimiter check function.|
|IsNullOrWhiteSpace| Extension to check if given string is null or only white space|
|IsNullOrEmpty     | Extension to check if given string is null or empty string|
|IsEmptyOrEqual    | True if the string is null / empty or equal to given value|

## Find
Extensions to extract sub strings.

| Name  | Description |
| ---  | :---: |
|Words| Split a string into a set of 'words'. A delimiter funcion is used to identify white-space.|
|NextWord| A fast, span-based string split|
|IndexOf| Overload IndexOf to provide a custom predicate.|
|Trim | Trim [Start] [End] as an extension|

## Build
Extensions to combine sub strings.

| Name  | Description |
| ---  | :---: |
|Combine| Use a seperator to Join string fragments|
|AsASCII| Remve any non ASCII characters |
|EnsurePrefix| Ensures the string startes with a given prefix |
|EnsurePostfix| Ensures the string ends with a given prefix |
|DefaultTo|Returning a default value if string is null or whitespace, else self. |

## Stream

| Name  | Description |
| ---  | :---: |
|AsUTF8Stream| Wrap string into a Memory stream |
|AsASCIIStream| Wrap string into a stream, non ASCII characters are dropped.|