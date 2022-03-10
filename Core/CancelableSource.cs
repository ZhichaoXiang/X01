
namespace X01{

public static class CancelableSourceExt{
public static IEnumerable<TItem> MonitorCancellation(this CancelableSource cs,IEnumerable<TItem> enumerable ){
return enumerable.select(x=>   { 
cs.throwifcancellationrequested
return x;});
}
}
}
