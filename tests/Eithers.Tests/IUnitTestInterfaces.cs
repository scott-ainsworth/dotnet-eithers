namespace TestSupport;

public interface IUnitTest0 {
    void RunTest<T>() where T : notnull;
}

public interface IUnitTest1 {
    void RunTest<T>(T value) where T : notnull;
}

public interface IUnitTest2 {
    void RunTest<T>(T value, T value2) where T : notnull;
}

public interface IUnitTest0Split {
    void RunTestOnReferenceType<T>() where T : class;
    void RunTestOnValueType<T>() where T : struct;
}

public interface IUnitTest1Split {
    void RunTestOnReferenceType<T>(T value) where T : class;
    void RunTestOnValueType<T>(T value) where T : struct;
}

public interface IUnitTest2Split {
    void RunTestOnReferenceType<T>(T value, T value2) where T : class;
    void RunTestOnValueType<T>(T value, T value2) where T : struct;
}
