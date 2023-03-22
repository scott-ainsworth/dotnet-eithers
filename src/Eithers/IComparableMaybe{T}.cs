//#nullable enable

//using System;

//namespace Ainsworth.Eithers;

//public interface IComparableMaybe<T> : IComparable<IComparableMaybe<T>>, IComparable<T>
//    where T : notnull, IComparable<T> {

//    public bool Equals(T value) => value.Equals(value);

//    public bool Equals(Maybe<T> value) => value is Some<T> v && Equals(v);
//}
