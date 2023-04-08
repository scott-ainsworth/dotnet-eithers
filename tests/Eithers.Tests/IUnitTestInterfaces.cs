namespace TestSupport;

/// <summary>
///   A unit test that requires no value arguments and the notnull type constraint.
/// </summary>
public interface IUnitTest0 {
    /// <summary>
    ///   Run a unit test case that requires no value arguments.
    /// </summary>
    /// <typeparam name="T">The type of the value required by this test case. 
    ///   This type parameter must have a <see langword="notnull"/> constraint.</typeparam>
    void RunTest<T>() where T : notnull;
}

/// <summary>
///   A unit test that requires a single value argument and the notnull type constraint.
/// </summary>
public interface IUnitTest1 {
    /// <summary>
    ///   Run a unit test case that requires one value argument.
    /// </summary>
    /// <typeparam name="T">The type of the value required by this test case. 
    ///   This type parameter must have a <see langword="notnull"/> constraint.</typeparam>
    void RunTest<T>(T value) where T : notnull;
}

/// <summary>
///   A unit test that requires two value arguments and the notnull type constraint.
/// </summary>
public interface IUnitTest2 {
    /// <summary>
    ///   Run a unit test case that requires two value arguments.
    /// </summary>
    /// <typeparam name="T">The type of the value required by this test case. 
    ///   This type parameter must have a <see langword="notnull"/> constraint.</typeparam>
    void RunTest<T>(T value, T value2) where T : notnull;
}

/// <summary>
///   A unit test that requires no value arguments and both class and struct type constraints.
/// </summary>
public interface IUnitTest0Split {
    /// <summary>
    ///   Run a unit test case that requires no value arguments.
    /// </summary>
    /// <typeparam name="T">The type of the value required by this test case.
    ///   This type parameter must have a <see langword="class"/> constraint.</typeparam>
    void RunTestOnReferenceType<T>() where T : class;
    /// <summary>
    ///   Run a unit test case that requires no value arguments.
    /// </summary>
    /// <typeparam name="T">The type of the value required by this test case.
    ///   This type parameter must have a <see langword="struct"/> constraint.</typeparam>
    void RunTestOnValueType<T>() where T : struct;
}

/// <summary>
///   A unit test that requires one value argument and both class and struct type constraints.
/// </summary>
public interface IUnitTest1Split {
    /// <summary>
    ///   Run a unit test case that requires one value argument.
    /// </summary>
    /// <typeparam name="T">The type of the value required by this test case.
    ///   This type parameter must have a <see langword="class"/> constraint.</typeparam>
    void RunTestOnReferenceType<T>(T value) where T : class;
    /// <summary>
    ///   Run a unit test case that requires one value argument.
    /// </summary>
    /// <typeparam name="T">The type of the value required by this test case.
    ///   This type parameter must have a <see langword="struct"/> constraint.</typeparam>
    void RunTestOnValueType<T>(T value) where T : struct;
}

/// <summary>
///   A unit test that requires two value arguments and both class and struct type constraints.
/// </summary>
public interface IUnitTest2Split {
    /// <summary>
    ///   Run a unit test case that requires two value arguments.
    /// </summary>
    /// <typeparam name="T">The type of the value required by this test case.
    ///   This type parameter must have a <see langword="class"/> constraint.</typeparam>
    void RunTestOnReferenceType<T>(T value, T value2) where T : class;
    /// <summary>
    ///   Run a unit test case that requires two value arguments.
    /// </summary>
    /// <typeparam name="T">The type of the value required by this test case.
    ///   This type parameter must have a <see langword="struct"/> constraint.</typeparam>
    void RunTestOnValueType<T>(T value, T value2) where T : struct;
}
