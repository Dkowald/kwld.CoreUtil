using System;
using System.Collections.Generic;
using kwd.CoreUtil.Strings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.String
{
    [TestClass]
    public class StringBuildTest
    {
        [TestMethod]
        public void Combine_()
        {
            var charCombine = ' '.Combine("multi", "string", "with", "space");

            Assert.AreEqual("multi string with space", charCombine);

            var strCombine = "; ".Combine(new List<string>{"Part1", "Part2", "Part3"});

            Assert.AreEqual("Part1; Part2; Part3", strCombine);
        }

        [TestMethod]
        public void Combine_Spans()
        {
            var s1 = "Hello".AsSpan();
            var s2 = "World".AsSpan();
            var s3 = "!".AsSpan();

            var sEnd = string.Empty.Combine(s2, s3);

            var result = ' '.Combine(Span<char>.Empty, s1, sEnd);

            Assert.AreEqual("Hello World!", result);
        }

        [TestMethod]
        public void AsASCII_()
        {
            var text = "Some text with unicode char " + "\u2103";

            var data = text.AsASCII();
            Assert.AreEqual("Some text with unicode char ", data);
        }
    }
}
