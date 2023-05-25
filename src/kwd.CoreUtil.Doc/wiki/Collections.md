# Overview
A set of classes to simplify working with collections.


## Dictionary
Helpers to provide some extra capabilities when using 
a Dictionary

|Extension|Description|
|    ---  |   :---:   |
|AddRange| Helpers to add multiple items at once.|
|Merge | Upsert items into a dictionary |
|ToDictionary| Convert a enumerable of KeyValuePair (or tupple) to dictionary |
|DefaultTo| Set a key default value if none exists.|

## (net6+) RecordArray
An collection specificly for records.

Use this to provide value-equality when working with a 
collection of objects.

When using this, you will still need to provide a 
copy constructor in the owning record type.

Includes a STJ converter to serialize a RecordArray as a 
JSON array.

To use the serializer, add a __RecordArrayConverterFactory__
as a converter.
