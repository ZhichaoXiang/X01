using System.Threading;

namespace X01
{
    public class Cancelable<TObject>
    {
        readonly TObject _obj;
        readonly CancellationToken? _ct;
        public TObject Obj
        {
            get
            {
                _ct?.ThrowIfCancellationRequested();
                return (_obj);
            }
        }
        public Cancelable(TObject obj, CancellationToken? ct)
        {
            _obj = obj;
            _ct = ct;
        }
        public Cancelable(TObject obj) : this(obj, null)
        {
        }
        public static implicit operator TObject(Cancelable<TObject> c) => c.Obj;
        // public static explicit operator Cancelable<TObject>(TObject b, CancellationToken? ct) => new Cancelable<TObject>(b, ct);
    }
}