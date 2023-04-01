//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace S4144Demo;

//public abstract class Wrap<T> where T : notnull { }

//public class WrapV<T> : Wrap<T> where T : notnull {
//    public T Value { get; init; }
//    internal WrapV(T value) => Value = value;
//}

//public class WrapN<T> : Wrap<T> where T : notnull {
//    private WrapN() { }
//    internal static WrapN<T> Singleton = new();
//}

//public static class Wrap {
//    public static Wrap<T> From<T>(T? value) where T : struct =>
//        value is T v ? new WrapV<T>(v) : WrapN<T>.Singleton;
//    public static Wrap<T> From<T>(T? value) where T : class =>
//        value is not null ? new WrapV<T>(value) : WrapN<T>.Singleton;
//}

//public static class S4144 {
//    public static void TestValueType<T>(T? value) where T : struct {
//        var maybe = Wrap.From(value);
//        Assert.AreEqual(value, (maybe as WrapV<T>)!.Value);
//    }
//    public static void TestReferenceType<T>(T? value) where T : class {
//        var maybe = Wrap.From(value);
//        Assert.AreEqual(value, (maybe as WrapV<T>)!.Value);
//    }
//}
