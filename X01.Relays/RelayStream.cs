using System.IO;

namespace X01.Relays
{
    public class RelayStream : Stream
    {
        readonly Cancelable<Stream> _cancelableStream;
        Stream stream => _cancelableStream;
        public override bool CanRead => stream.CanRead;
        public override bool CanSeek => stream.CanSeek;
        public override bool CanWrite => stream.CanWrite;
        public override long Length => stream.Length;
        public override long Position
        {
            get => stream.Position;
            set => stream.Position = value;
        }
        public RelayStream(Cancelable<Stream> cancelableStream)
        {
            _cancelableStream = cancelableStream;
        }
        public RelayStream(Stream stream) : this(new Cancelable<Stream>(stream, null))
        {
        }
        public override void Flush()
        {
            stream.Flush();
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            return (stream.Seek(offset, origin));
        }
        public override void SetLength(long value)
        {
            stream.SetLength(value);
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            return (stream.Read(buffer, offset, count));
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            stream.Write(buffer, offset, count);
        }
    }
}