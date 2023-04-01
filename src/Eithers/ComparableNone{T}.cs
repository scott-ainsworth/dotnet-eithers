//using System;

//namespace Ainsworth.Eithers;

//internal class ComparableNone<T> : None<T>, IComparableMaybe<T>
//    where T: notnull, IComparable<T> {

//    #region Properties

//    internal static new readonly ComparableNone<T> NoneValue = new();

//    #endregion
//    #region Constructors, Casts, and Conversions

//    protected ComparableNone() { }

//    #endregion
//    #region IComparable<T> Implementation

//    public int CompareTo(IComparableMaybe<T> other) => other.IsNone ? 0 : -1;

//    public int CompareTo(T other) => -1;

//    #endregion
//}
