using System;

namespace X01
{
    public abstract class ADisposable : IDisposable
    {
        public void test11()
        {
        }
        protected virtual void OnDisposing()
        {
        }
        public void Dispose()
        {
        }
    }
}
