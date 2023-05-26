using System;
using System.Collections.Generic;
using kwd.CoreUtil.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.Collections
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
                {"A", "1"}
            };

            target.AddRange(("B", "2"));

            var other = new Dictionary<string, string>
                { { "B", "@" } };

            target.Merge(other);

            Assert.AreEqual("@", target["B"]);
        }
    }
}
