using System.IO;
using kwd.CoreUtil.Strings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.Streams
{
    [TestClass]
    public class StringStreamTests
    {
        [TestMethod]
        public void AsUTF8_()
        {
            var someText = "A line of text, but I want a stream.";

            using var rd = someText.AsUTF8Stream();

            var fromStream = new StreamReader(rd)
                .ReadToEnd();

            Assert.AreEqual(someText, fromStream);
        }

        [TestMethod]
        public void AsASCII_()
        {
            var someText = "A line of text, but I want a stream";

            using var rd = someText.AsASCIIStream();

            var fromStream = new StreamReader(rd)
                .ReadToEnd();

            Assert.AreEqual(someText, fromStream);
        }

        [TestMethod]
        public void AsASCII_WithUnicodeChar()
        {
            var text = "Some text with unicode char \u2103";

            var fromUTF8Stream = new StreamReader(text.AsUTF8Stream()).ReadToEnd();
            
            Assert.AreEqual(text, fromUTF8Stream);

            var fromAsciiStream = new StreamReader(text.AsASCIIStream()).ReadToEnd();

            Assert.AreEqual("Some text with unicode char ", fromAsciiStream);
        }
    }
}