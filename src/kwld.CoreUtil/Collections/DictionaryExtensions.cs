﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace kwld.CoreUtil.Collections
{
    /// <summary>
    /// A set of helpers for some dictionary sugar
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Add data from <paramref name="rhs"/> into <paramref name="lhs"/>.
        /// Entries already in <paramref name="rhs"/> are overwritten.
        /// </summary>
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> lhs, IEnumerable<KeyValuePair<TKey, TValue>> rhs)
        {
            foreach (var item in rhs)
            {
                lhs.Add(item.Key, item.Value);
            }

            return lhs;
        }

        /// <inheritdoc cref="AddRange{TKey,TValue}(IDictionary{TKey,TValue},IEnumerable{KeyValuePair{TKey,TValue}})"/>
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> lhs,
            IDictionary<TKey, TValue> rhs)
            => lhs.AddRange((IEnumerable<KeyValuePair<TKey, TValue>>)rhs);

        /// <inheritdoc cref="AddRange{TKey,TValue}(IDictionary{TKey,TValue},IEnumerable{KeyValuePair{TKey,TValue}})"/>
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> lhs,
            params (TKey Key, TValue Value)[] rhs)
            => lhs.AddRange(rhs.Select(x => KeyValuePair.Create(x.Key, x.Value)));

        /// <summary>
        /// Merge items from <paramref name="rhs"/> into <paramref name="lhs"/>;
        /// Any existing items are updated.
        /// </summary>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> lhs, IEnumerable<KeyValuePair<TKey, TValue>> rhs)
        {
            foreach (var item in rhs)
            {
                if (lhs.ContainsKey(item.Key)) lhs[item.Key] = item.Value;
                else lhs.Add(item.Key, item.Value);
            }

            return lhs;
        }

        /// <inheritdoc cref="Merge{TKey,TValue}(IDictionary{TKey,TValue},IEnumerable{KeyValuePair{TKey,TValue}})"/>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> lhs,
            IDictionary<TKey, TValue> rhs)
            => lhs.Merge((IEnumerable<KeyValuePair<TKey, TValue>>)rhs);

        /// <inheritdoc cref="Merge{TKey,TValue}(IDictionary{TKey,TValue},IEnumerable{KeyValuePair{TKey,TValue}})"/>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> lhs,
            params (TKey Key, TValue Value)[] rhs)
            => lhs.Merge(rhs.Select(x => KeyValuePair.Create(x.Key, x.Value)));

        /// <summary>
        /// Sugar to crate dictionary from set of key-value data.
        /// <see cref="Dictionary{TKey,TValue}(IEnumerable{KeyValuePair{TKey,TValue}})"/>
        /// </summary>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> data)
            where TKey : notnull => new Dictionary<TKey, TValue>(data);

        /// <inheritdoc cref="ToDictionary{TKey,TValue}(IEnumerable{KeyValuePair{TKey,TValue}})"/>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> data)
            where TKey : notnull => new Dictionary<TKey, TValue>(data.Select(x => KeyValuePair.Create(x.Key, x.Value)));

        /// <summary>
        /// If <paramref name="key"/> is null, assign <paramref name="value"/>
        /// </summary>
        public static IDictionary<string, string> DefaultTo(this IDictionary<string, string> lhs, string key, string value)
        {
            if (lhs.ContainsKey(key))
            {
                if (lhs[key] != value)
                    throw new Exception("Key value already set");
            }
            else
            {
                lhs[key] = value;
            }

            return lhs;
        }
    }
}
