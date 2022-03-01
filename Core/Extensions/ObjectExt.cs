namespace X01 {
     public static class ObjectExt{
          public static bool IsDisposed(this object obj) {
            try {
                obj?.GetType();
                return (false);
            } catch(ObjectDisposedException) {
                return (true);
            }
        }
    }
}
