# Overview
Extensions to help working with strings.

Where applicable, Span\<char\> overrides are provided to avoid string allocations.

| Name  | Description |
| ---  | :---: |
| String Match  | Compare strings |
| String Find| Find pieces of a string |
| String Build | (re)Combine strings  |
| String Stream| Strings as streams |

## Match
Extensions to help compare strings.

| Name  | Description |
| ---  | :---: |
|Same| Compare two strings ignoring case and any leading / trailing white space. |
|SamePhrase| Compare the words in 2 strings; with optional custom delimiter check function. |
|IsNullOrWhiteSpace| Extension to check if given string is null or only white space|
|IsNullOrEmpty| Extension to check if given string is null or empty string|

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

## Stream

| Name  | Description |
| ---  | :---: |
|AsUTF8Stream| Wrap string into a Memory stream |
|AsASCIIStream| Wrap string into a stream, non ASCII characters are dropped.|