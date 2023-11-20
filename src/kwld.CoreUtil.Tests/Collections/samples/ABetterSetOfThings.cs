#if NET6_0_OR_GREATER
using kwld.CoreUtil.Collections;

namespace kwld.CoreUtil.Tests.Collections.samples;

/// <summary>
/// Test inheritance with base record using a <see cref="RecordArray{T}"/>
/// </summary>
public record ABetterSetOfThings(RecordArray<AThing> Items, string Name)
    : ANewSetOfThings(Items);
#endif