#if NET6_0_OR_GREATER

using System.Text.Json;
using kwld.CoreUtil.Collections;
using kwld.CoreUtil.Tests.Collections.samples;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwld.CoreUtil.Tests.Collections;

[TestClass]
public class RecordArrayTests
{
    [TestMethod]
    public void ASetOfThingsTests()
    {
        var a = new ASetOfThings(new AThing[] { new("a") });
        var b = new ASetOfThings(new AThing[] { new("a") });
        var cloned = a with { };

        Assert.IsFalse(ReferenceEquals(a.Items, b.Items), "different lists");
        
        //FAIL!
        //Assert.AreEqual(a, b, "Should be value-equality");
        //Assert.AreEqual(a.GetHashCode(), b.GetHashCode(), "Hashes should match for value-equality");
        
        //FAIL! clone just copies the reference 
        //Assert.IsFalse(ReferenceEquals(a.Items, cloned.Items), "Want a copy of the set, not the same set");

        //FAIL! -- looks like it's success; but its actually comparing object references
        Assert.AreEqual(a, cloned, "Has value-equality");
        Assert.AreEqual(a.GetHashCode(), cloned.GetHashCode(), "Hashes should match for value-equality");
    }

    [TestMethod]
    public void ASetOfThingsFixed()
    {
        var a = new ASetOfThingsFixed(new AThing[] { new("a") });
        var b = new ASetOfThingsFixed(new AThing[] { new("a") });
        var cloned = a with { };

        Assert.IsFalse(ReferenceEquals(a.Items, b.Items), "different lists");
        
        Assert.AreEqual(a, b, "Should be value-equality");
        Assert.AreEqual(a.GetHashCode(), b.GetHashCode(), "Hashes should match for value-equality");
        
        Assert.IsFalse(ReferenceEquals(a.Items, cloned.Items), "Want a copy of the set, not the same set");

        Assert.AreEqual(a, cloned, "Has value-equality");
        Assert.AreEqual(a.GetHashCode(), cloned.GetHashCode(), "Hashes should match for value-equality");
    }

    [TestMethod]
    public void ANewSetOfThings()
    {
        var a = new ANewSetOfThings(new(new AThing("A") ));
        var b = new ANewSetOfThings(new(new AThing("A")));
        var cloned = a with { };

        Assert.IsFalse(ReferenceEquals(a.Items, b.Items), "different lists");

        Assert.AreEqual(a, b, "Should be value-equality");
        Assert.AreEqual(a.GetHashCode(), b.GetHashCode(), "Hashes should match for value-equality");

        Assert.IsFalse(ReferenceEquals(a.Items, cloned.Items), "Want a copy of the set, not the same set");

        Assert.AreEqual(a, cloned, "Has value-equality");
        Assert.AreEqual(a.GetHashCode(), cloned.GetHashCode(), "Hashes should match for value-equality");
    }
    
    [TestMethod]
    public void ABetterSetOfThings()
    {
        var a = new ABetterSetOfThings(new(new AThing("A")), "A");
        var b = new ABetterSetOfThings(new(new AThing("A")), "A");
        var cloned = a with { };

        Assert.IsFalse(ReferenceEquals(a.Items, b.Items), "different lists");

        Assert.AreEqual(a, b, "Should be value-equality");
        Assert.AreEqual(a.GetHashCode(), b.GetHashCode(), "Hashes should match for value-equality");

        Assert.IsFalse(ReferenceEquals(a.Items, cloned.Items), "Want a copy of the set, not the same set");

        Assert.AreEqual(a, cloned, "Has value-equality");
        Assert.AreEqual(a.GetHashCode(), cloned.GetHashCode(), "Hashes should match for value-equality");
    }

    [TestMethod]
    public void SupportSerialize()
    {
        var cfg = new JsonSerializerOptions();
        cfg.Converters.Add(new RecordArrayConverterFactory());

        var a = new ANewSetOfThings(new(new AThing("A")));

        var json = JsonSerializer.Serialize(a, cfg);
        var b = JsonSerializer.Deserialize<ANewSetOfThings>(json, cfg);

        Assert.IsNotNull(b);
        Assert.IsFalse(ReferenceEquals(a.Items, b.Items), "different lists");
        Assert.AreEqual(a, b, "Should be value-equality");
        Assert.AreEqual(a.GetHashCode(), b.GetHashCode(), "Hashes should match for value-equality");
    }

    [TestMethod]
    public void SupportRangeExpressions()
    {
        var item1 = new AThing("A");
        var itemLast = new AThing("C");
        var a = new ANewSetOfThings(new(
            item1,
            new AThing("B"),
            new AThing("1"),
            new AThing("2"),
            itemLast));
        
        //use index to get element at index.
        var first = a.Items[0];
        Assert.AreEqual(item1.Name, first.Name);

        //use from-end index.
        var last = a.Items[^1];
        Assert.AreEqual(itemLast.Name, last.Name);

        //can use range to slice.
        var subSet = a.Items[1..^1];
        Assert.AreEqual(3, subSet.Length);
    }

    [TestMethod]
    public void Copy_()
    {
        var item0 = new AThing("A");

        var target = RecordArray.Create(
            item0,
            new AThing("B"),
            new AThing("1"),
            new AThing("2"),
            new AThing("C"));
        
        var copy =  target.Copy(x => x with { });
        
        Assert.IsFalse(ReferenceEquals(item0, copy[0]));
    }
}
#endif