#if NET6_0_OR_GREATER
using System;
using System.Collections.Immutable;
using System.Linq;

namespace kwd.CoreUtil.Tests.Collections.samples;

/// <summary>A in-place fixed record</summary>
public record ASetOfThingsFixed(AThing[] Items)
{
    protected ASetOfThingsFixed(ASetOfThingsFixed other)
    { Items = other.Items.Select(x => x with { }).ToArray(); }

    public virtual bool Equals(ASetOfThingsFixed? other) =>
        other is not null && Items.SequenceEqual(other.Items);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var item in Items) hash.Add(item);
        return hash.ToHashCode();
    }
}
#endif