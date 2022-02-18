using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace X01.Relays;
public class RelayStream : Stream
{
    readonly Stream _stream;
    readonly CancellationToken? _ct;
    Stream stream
    {
        get
        {
            _ct?.ThrowIfCancellationRequested();
            return (_stream);
        }
    }
    public override bool CanRead => stream.CanRead;

    public override bool CanSeek => stream.CanSeek;

    public override bool CanWrite => stream.CanWrite;

    public override long Length => stream.Length;

    public override long Position
    {
        get => stream.Position;
        set => stream.Position = value;
    }
    public RelayStream(Stream stream, CancellationToken? ct)
    {
        _stream = stream;
        _ct = ct;
    }
    public RelayStream(Stream stream) : this(stream, null)
    {
    }

    public override void Flush()
    {
        stream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new System.NotImplementedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new System.NotImplementedException();
    }

    public override void SetLength(long value)
    {
        throw new System.NotImplementedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new System.NotImplementedException();
    }
}
