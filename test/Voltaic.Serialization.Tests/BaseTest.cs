namespace Voltaic.Serialization.Tests
{
    public enum TestType : byte
    {
        FailRead,
        FailWrite,
        Read,
        Write,
        ReadWrite
    }

    public class TestData<T>
    {
        public T Value { get; }
        public string String { get; }
        public TestType Type { get; }

        public TestData(TestType type, string str, T value)
        {
            Type = type;
            String = str;
            Value = value;
        }
    }
}
