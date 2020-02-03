using System;
using System.IO;
using System.Threading.Tasks;

using kwd.CoreUtil.Streams;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.Streams
{
    [TestClass]
    public class TeeStreamTests
    {
        [TestMethod]
        public void WritesToAll()
        {
            var main = new MemoryStream();
            var other = new MemoryStream();

            var data = "Test text";

            using var target = new TeeStream(main, other);

            using var wr = new StreamWriter(target);
            wr.Write(data);

            wr.Flush();

            Assert.AreEqual(data, Read(main));
            Assert.AreEqual(data, Read(other));
        }

        [TestMethod]
        public void WritesToAdded()
        {
            using var main = new MemoryStream();
            using var other = new MemoryStream();

            var target = new TeeStream(main, other);

            using var wr = new StreamWriter(target);

            wr.Write("Test");
            wr.Flush();

            var other2 = new MemoryStream();
            target.Add(other2);

            wr.Write(" after added");

            wr.Flush();
            
            Assert.AreEqual("Test after added", Read(main));
            Assert.AreEqual("Test after added", Read(other));
            Assert.AreEqual(" after added", Read(other2));
        }

        [TestMethod]
        public async Task WriteAsync()
        {
            var main = new MemoryStream();
            var other = new MemoryStream();

            await using var target = new TeeStream(main, other);

            await using var wr = new StreamWriter(target);

            var data = "Test data".AsMemory();
            await wr.WriteAsync(data);

            await wr.FlushAsync();

            var other2 = new MemoryStream();
            target.Add(other2);

            await wr.WriteAsync(" after add");
            await wr.FlushAsync();

            Assert.AreEqual("Test data after add", Read(other));
            Assert.AreEqual(" after add", Read(other2));
        }

        [TestMethod]
        public void DisposeAll()
        {
            var main = new MemoryStream();
            var other = new MemoryStream();

            using var target = new TeeStream(main, other);
            using var wr = new StreamWriter(target);

            wr.Dispose();
            
            try
            {
                Read(main);
                Assert.Fail("Expected stream to be closed.");
            }catch(ObjectDisposedException){}
        }

        [TestMethod]
        public void LeaveOpen()
        {
            var main = new MemoryStream();
            var other = new MemoryStream();
            
            using var target = new TeeStream(main, other, true);

            target.Dispose();

            Assert.IsTrue(main.CanSeek, "Not disposed");
            
            main.Dispose();
            other.Dispose();
        }

        private static string Read(MemoryStream stream)
        {
            stream.Position = 0;
            return new StreamReader(stream).ReadToEnd();
        }

    }
}
