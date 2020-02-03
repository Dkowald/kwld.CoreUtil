using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace kwd.CoreUtil.Streams
{
    /// <summary>
    /// Write (only) to multiple streams with one stream
    /// (aka unix Tee). 
    /// </summary>
    /// <remarks>
    /// Only supports basic stream write functionality.
    /// </remarks>
    public class TeeStream : Stream
    {
        private bool _disposed;
        private readonly bool _leaveOpen;
        private readonly List<Stream> _out = new List<Stream>();

        /// <summary>
        /// Create a <see cref="TeeStream"/> that will
        /// write to two streams at once.
        /// </summary>
        /// <param name="mainStream">Main stream</param>
        /// <param name="otherStream">Other (branch) stream</param>
        /// <param name="leaveOpen">If true, provided streams will not be closed when this is disposed.</param>
        public TeeStream(Stream mainStream, Stream otherStream, bool leaveOpen = false)
        {
            if(!mainStream.CanWrite)
                throw new ArgumentException
                    ("Must support write", nameof(mainStream));
            
            if(!otherStream.CanWrite)
                throw new ArgumentException
                    ("Must support write", nameof(otherStream));
            _leaveOpen = leaveOpen;

            _out.Add(mainStream);
            _out.Add(otherStream);
        }

        /// <summary>
        /// Add additional streams to be written to.
        /// </summary>
        public TeeStream Add(params Stream[] otherStreams)
        {
            if(otherStreams.Any(x => !x.CanWrite))
                throw new ArgumentException
                    ("Must support write", nameof(otherStreams));
    
            _out.AddRange(otherStreams);

            return this;
        }

        #region Stream

        /// <inheritdoc />
        public override void Flush()
        {
            foreach (var item in _out)
            {
                item.Flush();
            }
        }

        /// <inheritdoc />
        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(
                _out.Select(x => x.FlushAsync(cancellationToken)));
        }
        
        /// <inheritdoc />
        public override Task WriteAsync(byte[] buffer, int offset, int count, 
            CancellationToken cancellationToken)
        {
            return Task.WhenAll(_out.Select(x => 
                x.WriteAsync(buffer, offset, count, cancellationToken)));
        }

        /// <inheritdoc />
        public override async ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, 
            CancellationToken cancellationToken = default)
        {
            foreach (var item in _out)
            {
                await item.WriteAsync(buffer, cancellationToken);
            }
        }

        /// <inheritdoc />
        public override void Write(ReadOnlySpan<byte> buffer)
        {
            foreach (var item in _out)
            {
                item.Write(buffer);
            }
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            foreach (var item in _out)
            {
                item.Write(buffer, offset, count);
            }
        }
        
        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException("Cannot Read");
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException("Cannot Seek");
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            throw new NotImplementedException("Cannot set length: seek not supported");
        }

        /// <inheritdoc />
        public override bool CanRead => false;

        /// <inheritdoc />
        public override bool CanSeek => false;

        /// <inheritdoc />
        public override bool CanWrite => true;

        /// <summary>
        /// Gets the length in bytes of the main (first) stream.
        /// </summary>
        public override long Length => _out.First().Length;
        
        /// <summary>
        /// Gets the current position in the main (first) stream.
        /// </summary>
        public override long Position 
        { 
            get => _out.First().Position;
            set => throw new NotImplementedException("Cannot seek");
        }
        #endregion

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (!_leaveOpen && disposing)
            { 
                var errors = new List<Exception>();
                var idx = 0;
                foreach (var item in _out)
                {
                    try
                    {
                        item.Dispose();
                    }
                    catch (Exception ex)
                    {
                        errors.Add(new Exception($"Error disposing stream at index: {idx}", ex));
                    }

                    idx++;
                }

                if(errors.Any())
                    throw new AggregateException(errors);
            }

            _disposed = true;
            base.Dispose(disposing);
        }

        /// <inheritdoc />
        public override async ValueTask DisposeAsync()
        {
            if(_disposed)return;

            if (!_leaveOpen)
            {
                var errors = new List<Exception>();
                var idx = 0;
                foreach (var item in _out)
                {
                    try
                    {
                        await item.DisposeAsync();
                    }
                    catch (Exception error)
                    {
                        errors.Add(
                            new Exception(
                                $"Error disposing stream at index: {idx}", error));
                    }

                    idx++;
                }

                if (errors.Any())
                    throw new AggregateException(errors);
            }

            _disposed = true;
            await base.DisposeAsync();
        }
    }
}
