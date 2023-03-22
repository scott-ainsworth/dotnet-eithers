//#nullable enable

//using System;
//using System.Collections.Generic;

//namespace Ainsworth.Eithers;

//internal sealed class ComparableSome<T> : Some<T>, IComparableMaybe<T>
//    where T : notnull, IComparable<T> {

//    #region Constructors, Casts, and Conversions

//    internal ComparableSome(T value) : base(value) { }

//    public static implicit operator ComparableSome<T>(T value) => new(value);

//    public static implicit operator T(ComparableSome<T> some) => some.Value;

//    #endregion
//    #region IComparible<T> Implementation

//    public int CompareTo(IComparableMaybe<T> other) =>
//        other is Some <T> s ? CompareTo(s.Value) : -1;

//    public int CompareTo(T other) => Value.CompareTo(other);

//    #endregion
//    #region System.Object overrides

//    public override int GetHashCode() => Value.GetHashCode();

//    #endregion
//}
