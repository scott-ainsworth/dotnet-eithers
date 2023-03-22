//#nullable enable

//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace Ainsworth.Eithers;

//public struct EitherEnumerator<T> : IEnumerator<T>
//    where T : notnull {

//    public T Value { get; private init; }

//    public T Current => Value;

//    object IEnumerator.Current => Current;

//    private int index = -1;

//    internal EitherEnumerator(T value) {
//        Value = value;
//        index = -1;
//    }

//    public bool MoveNext() => ++ index == 0;

//    public void Reset() => index = -1;

//    public void Dispose() { /* nothing to dispose */ }

//}
