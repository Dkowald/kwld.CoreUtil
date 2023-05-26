#if NET6_0_OR_GREATER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace kwd.CoreUtil.Collections;

/// <summary>
/// Base for <see cref="RecordArray{T}"/>.
/// </summary>
public abstract class RecordArray
{
    /// <summary>
    /// Create a new <see cref="RecordArray{T}"/> with the given data.
    /// </summary>
    public static RecordArray<TNew> Create<TNew>(IEnumerable<TNew>? data) => new(data);

    /// <inheritdoc cref="Create{TNew}(System.Collections.Generic.IEnumerable{TNew}?)"/>
    public static RecordArray<TNew> Create<TNew>(params TNew[] data) => new(data);
}

/// <summary>
/// An array-like collection of record's
/// <list type="bullet">
/// <item>Implements value-equality over items</item>
/// <item>ONLY use with objects that support value-equality.</item>
/// <item>Containing record MUST implement copy constructor</item>
/// </list>
/// </summary>
/// <remarks>
/// copy collection using record clone: <br/>
///   new(copy.Items.Select(x => x with { }));
/// </remarks>
public sealed class RecordArray<T> : RecordArray, IEquatable<RecordArray<T>>, IReadOnlyList<T>
{
    private readonly T[] _data;
    
    /// <summary>
    /// Create empty<br/>
    /// <inheritdoc cref="RecordArray{T}"/>
    /// </summary>
    /// <remarks>
    /// Shorthand for RecordArray(Array.Empty&lt;T&gt;)<br/><br/>
    /// <inheritdoc cref="RecordArray{T}"/>
    /// </remarks>
    public RecordArray()
    {
        _data = Array.Empty<T>();
    }

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

    /// <summary>
    /// Copy the items into a new <see cref="RecordArray{T}"/>, optionally slicing the data.
    /// </summary>
    /// <param name="copyFunc">Copy function used to map items from old to new collection.</param>
    /// <param name="start">Start zero-based index of items to copy.</param>
    /// <param name="length">Optional count of items to copy. Defaults to all items.</param>
    /// <remarks> 
    /// Use in the records copy constructor:
    /// e.g : Items = copy.Items.Copy(x => x with {})
    /// </remarks>
    public RecordArray<T> Copy(Func<T, T> copyFunc, int start = 0, int? length = default)
        => new (Slice(start, length ?? _data.Length-start).Select(copyFunc));
    
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

    /// <summary>
    /// Get the total number of elements in the collection.
    /// </summary>
    /// <remarks>Same as <see cref="Count"/>;
    /// included to make easier factoring when replacing a standard T[] type</remarks>
    public int Length => _data.Length;

    /// <summary>
    /// Select a sub-set of the items (supports range operator).
    /// </summary>
    public T[] Slice(int start, int length)
    {
        if (start < 0) 
            throw new ArgumentOutOfRangeException(nameof(start), start, "Non-negative number required");

        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), length, "Non-negative number required");

        if (length + start > _data.Length)
            throw new ArgumentOutOfRangeException(nameof(length), length,
                "Specified argument was out of the range of valid values.");

        var slice = new T[length];
        Array.Copy(_data, start, slice, 0, length);
        return slice;
    }
}

#endif