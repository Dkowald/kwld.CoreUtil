using System;
using System.Text.Json;
using kwld.CoreUtil.Strings;
using kwld.CoreUtil.Tests.String.samples;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwld.CoreUtil.Tests.String;

[TestClass]
public class DataStringTests
{
    private readonly Lazy<JsonSerializerOptions> _jsonConfig = new(() => {
        var cfg = new JsonSerializerOptions();
        cfg.Converters.Add(new DataStringConverterFactory());
        return cfg;
    });

    [TestMethod]
    public void MultipleTryParseCandidates()
    {
        var target = JsonSerializer.Deserialize<HostName>("\"a.b.xyz\"", _jsonConfig.Value);

        Assert.AreEqual("a.b.xyz", target, "Ok if have multiple TryParse methods");
    }

    [TestMethod]
    public void RefactoringWithADataString()
    {
        var info = "my-path-string";

        //pre factor
        DemoRefactor.EchoPathSegment(info);

        //post factor: use data string not raw string
        DemoRefactor.Echo(new(info));

        //post factor: can use data string in place of raw string
        DemoRefactor.EchoPathSegment(new("my-path-string"));

        Assert.IsTrue(true, "check factoring to replace a string with DataString");
    }

    [TestMethod]
    public void UsingASimpleDataString()
    {
        var name = new UserName("MyName ");

        Assert.AreEqual("myname", name, "trimmed and lower-cased");
        
        var tryParse = UserName.TryParse("not ! a user name");
        Assert.IsNull(tryParse);

        try
        {
            _ = new UserName("Not a * name1 ");
            Assert.Fail("Invalid name created");
        }
        catch (ArgumentException) { }
    }
    
    [TestMethod]
    public void UseAOneOfString()
    {
        var target = UserProfileType.Admin;

        var parsedValue = UserProfileType.TryParse(" admin");

        Assert.IsNotNull(parsedValue);
        Assert.IsTrue(ReferenceEquals(target, parsedValue), "reused instance");
    }

    [TestMethod]
    public void ReadWriteASimpleDataString()
    {
        var item = new PathSegment("foo");
        
        var json = JsonSerializer.Serialize(item, _jsonConfig.Value);

        Assert.AreEqual("\"foo\"", json, "DataString is a string in JSON");

        var read = JsonSerializer.Deserialize<PathSegment>(json, _jsonConfig.Value);

        Assert.AreEqual(item, read, "round trip a simple DataString");
    }

    [TestMethod]
    public void ReadWriteACombinedDataString()
    {
        var data = new HostName("a.v.c");

        var json = JsonSerializer.Serialize(data, _jsonConfig.Value);
        Assert.AreEqual("\"a.v.c\"", json);

        var read = JsonSerializer.Deserialize<HostName>(json, _jsonConfig.Value) ??
                   throw new Exception("Failed re-load object");

        Assert.AreEqual(data.ToString(), read.ToString(), "can round trip");
    }

    [TestMethod]
    public void ReadWriteAOneOfDataString()
    {
        var data = UserProfileType.Admin;

        var json = JsonSerializer.Serialize(data, _jsonConfig.Value);
    
        var read = JsonSerializer.Deserialize<UserProfileType>(json, _jsonConfig.Value);

        Assert.IsTrue(ReferenceEquals(data, read), "reuse existing object for one-of");
    }

#if NET7_0_OR_GREATER

    [TestMethod]
    public void ReadWriteAEntityMadeUpOfDataStrings()
    {
        var data = new UserProfile(new("fred"), new("fred@work.com"), UserProfileType.Guest);

        var json = JsonSerializer.Serialize(data, _jsonConfig.Value);

        var target = JsonSerializer.Deserialize<UserProfile>(json, _jsonConfig.Value);

        Assert.AreEqual(data, target);
    }
#endif
}
