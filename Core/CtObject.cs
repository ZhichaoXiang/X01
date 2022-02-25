using System.Threading;

namespace X01
{
    public class CtObject<TObject>
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
        public CtObject(TObject obj, CancellationToken? ct)
        {
            _obj = obj;
            _ct = ct;
        }
        public CtObject(TObject obj) : this(obj, null)
        {
        }
        public static implicit operator TObject(CtObject<TObject> c) => c.Obj;
        public static explicit operator CtObject<TObject>(TObject b) => new CtObject<TObject>(b);
    }
}