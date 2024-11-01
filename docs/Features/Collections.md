﻿# Overview
A set of classes to simplify working with collections.

[<-Home](../Home.md)  

## Dictionary
Extensions to provide some extra capabilities when using a
IDictionary&lt;TKey, TItem&gt;

|Extension|Description|
|    ---  |   :---:   |
|lhs.AddRange(rhs)| Copies the content rhs into lhs.|
|lhs.Merge(rhs) | Any item found in lhs is replaced with value from rhs (if exists)|
|lhs.WithDefaults(rhs)| Any item in rhs not found in lhs is added to lhs.|
|ToDictionary| Convert a enumerable of KeyValuePair (or tupple) to dictionary |

## (net6+) RecordArray
An collection specificly for records.

Use this to provide value-equality when working with a 
collection of objects.

When using this, you will still need to provide a 
copy constructor in the owning record type.

Simple example:
``` cs
using kwld.CoreUtil.Collections;

public record AThing(string Name);

public record SomeThings(RecordArray<AThing> Items)
{
   protected SomeThings(SomeThings copy)
   {
     Items = copy.Items.Copy(x => x with {});
   }
}

var a = new SomeThings(RecordArray.Create(new AThing("1")));
var b = new SomeThings(RecordArray.Create(new AThing("1")));

Asser.AreEqual(a, b, "The RecordArray provides value-equality")
```

Use the __RecordArrayConverterFactory__
 [STJ](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview)
converter to serialize a RecordArray as a JSON array.

