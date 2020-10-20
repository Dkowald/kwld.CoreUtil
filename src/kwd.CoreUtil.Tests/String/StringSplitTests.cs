using System;
using System.Linq;
using kwd.CoreUtil.Strings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.String
{
    [TestClass]
    public class StringSplitTests
    {
        [TestMethod]
        public void Words_()
        {
            var words = "I'll be BACK! ".Words().ToArray();
            Assert.AreEqual(3, words.Length);
            Assert.AreEqual("BACK!", words.Last());
        }

        [TestMethod]
        public void NextWord_()
        {
            var word = "a phrase".AsSpan().NextWord(out var rest);

            Assert.AreEqual("a", word.ToString());

            Assert.AreEqual(" phrase", rest.ToString());

            word = rest.NextWord(out rest);
            Assert.AreEqual("phrase", word.ToString());
            Assert.AreEqual("", rest.ToString());

            word = " single\t".AsSpan().NextWord(out rest);
            Assert.AreEqual("single", word.ToString());
            Assert.AreEqual(1, rest.Length);
        }

        [TestMethod]
        public void NextWord_NoWords()
        {
            var word = "".AsSpan().NextWord(out var rest);
            Assert.AreEqual(0, word.Length);
            Assert.AreEqual(0, rest.Length);

            word = "  \t\r\n\t ".AsSpan().NextWord(out rest);
            Assert.AreEqual(0, word.Length);
            Assert.AreEqual(0, rest.Length);
        }

        [TestMethod]
        public void NextWord_CustomDelimiter()
        {
            Func<char, bool> isNewLine = c => c == '\n';

            var multiLine = " a couple\n of lines. ";

            var word = multiLine.AsSpan().NextWord(out var rest, isNewLine);

            Assert.AreEqual(" a couple", word.ToString());

            word = rest.NextWord(out rest, isNewLine);
            Assert.AreEqual(" of lines. ", word.ToString());
        }
    }
}
