using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using kwld.CoreUtil.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwld.CoreUtil.Tests.Collections
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        [TestMethod]
        public void AddRange_()
        {
            var target = new Dictionary<string, string>
            {
                {"A", "1"}
            };
            
            target.AddRange(("B", "2"));

            Assert.AreEqual(2, target.Count);

            try
            {
                target.AddRange(new[] { new KeyValuePair<string, string>("B", "2") });
                Assert.Fail("Already has the 'B' key");
            }
            catch (ArgumentException) { }
        }

        [TestMethod]
        public void Merge_()
        {
            var target = new Dictionary<string, string>
            {
                {"A", "1"},
                {"Other", "99"}
            };

            var rhs = new[]{("B", "2"),("A", "fred")};

            target.Merge(rhs);

            Assert.IsFalse(target.ContainsKey("B"));
            Assert.AreEqual("fred", target["A"]);
            Assert.AreEqual("99", target["Other"]);
        }

        [TestMethod]
        public void DefaultWith_()
        {
            var lhs = new[]
            {
                ("red", 10),
                ("blue", 5)
            }.ToDictionary();

            var defaults = new[]
            { ("red", 5), ("green", 5), ("blue", 5) };

            lhs.WithDefaults(defaults);

            Assert.AreEqual(10, lhs["red"]);
            Assert.AreEqual(5, lhs["green"]);
        }
    }
}
