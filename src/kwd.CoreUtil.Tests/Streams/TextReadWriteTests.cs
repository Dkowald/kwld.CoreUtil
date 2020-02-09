using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using kwd.CoreUtil.Streams;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.Streams
{
    [TestClass]
    public class TextReadWriteTests
    {
        [TestMethod]
        public void WriteReadLines()
        {
            var lines = new[] {"Line 1", "Line 2", "Line 3"};

            string data;
            using (var wr = new StringWriter())
            {
                wr.WriteLines("Line 1", "Line 2", "Line 3");
                wr.Flush();

                data = wr.GetStringBuilder().ToString();
            }

            var expectedWrite = string.Join(Environment.NewLine, lines) +
                                Environment.NewLine;
            Assert.AreEqual(expectedWrite, data);

            string[] readData;
            using(var rd = new StringReader(data))
            {
                readData = rd.ReadLines().ToArray();
            }

            CollectionAssert.AreEqual(lines, readData);
        }

        [TestMethod]
        public async Task WriteReadLinesAsync()
        {
            var lines = new[] {"Line 1", "Line 2", "Line 3"};

            string data;
            await using (var wr = new StringWriter())
            {
                await wr.WriteLinesAsync("Line 1", "Line 2", "Line 3");
                await wr.FlushAsync();

                data = wr.GetStringBuilder().ToString();
            }

            var expectedWrite = string.Join(Environment.NewLine, lines) +
                                Environment.NewLine;
            Assert.AreEqual(expectedWrite, data);

            string[] readData;
            using(var rd = new StringReader(data))
            {
                readData = await rd.ReadLinesAsync().ToArrayAsync();
            }

            CollectionAssert.AreEqual(lines, readData);
        }
    }
}