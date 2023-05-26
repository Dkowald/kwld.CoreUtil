#if NET6_0_OR_GREATER
using System.Linq;
using kwd.CoreUtil.Collections;

namespace kwd.CoreUtil.Tests.Collections.samples;

/// <summary>A fixed record using a <see cref="RecordArray{T}"/></summary>
public record ANewSetOfThings(RecordArray<AThing> Items)
{
    protected ANewSetOfThings(ANewSetOfThings copy)
    {
        Items = new(copy.Items.Select(x => x with { }));
    }
}
#endif