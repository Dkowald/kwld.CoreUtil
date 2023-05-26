#if NET6_0_OR_GREATER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace kwd.CoreUtil.Collections;

/// <summary>
/// An array-like collection of record's
/// <list type="bullet">
/// <item>Implements value-equality over items</item>
/// </list>
/// </summary>
/// <remarks>
/// ONLY use with objects that support value-equality.
/// </remarks>
public sealed class RecordArray<T> : IEquatable<RecordArray<T>>, IReadOnlyList<T>
{
    private readonly T[] _data;

    /// <inheritdoc cref="RecordArray{T}"/>
    public RecordArray(params T[] data)
    {
        _data = data; 
    }

    /// <inheritdoc cref="RecordArray{T}"/>
    public RecordArray(IEnumerable<T>? data)
    {
        _data = data?.ToArray() ?? Array.Empty<T>();
    }
    
    #region Equality
    /// <inheritdoc cref="Equals(object?)"/>
    public static bool operator ==(RecordArray<T>? lhs, RecordArray<T>? rhs)
    {
        if (lhs is null) return rhs is null;
        return lhs.Equals(rhs);
    }

    /// <summary>
    /// Determines whether the specified object is NOT equal to the current object.
    /// </summary>
    /// <returns><see langword="true"/> if the specified object is NOT equal to the current object; otherwise <see langword="true"/></returns>
    public static bool operator !=(RecordArray<T>? lhs, RecordArray<T>? rhs)
        => !(lhs == rhs);
    
    /// <inheritdoc/>
    public override bool Equals(object? obj)
        => obj is RecordArray<T> rhs && Equals(rhs);
    
    /// <inheritdoc />
    public bool Equals(RecordArray<T>? other)
        => other is not null && _data.SequenceEqual(other._data);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var item in _data) hash.Add(item);
        return hash.ToHashCode();
    }
    
    #endregion

    #region IReadOnlyList
    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_data).GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();

    /// <inheritdoc />
    public int Count => _data.Length;
    
    /// <inheritdoc />
    public T this[int index] => _data[index];
    #endregion
}

#endif