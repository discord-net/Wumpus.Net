using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public enum TestType : byte
    {
        FailRead,
        FailWrite,
        Read,
        Write,
        ReadWrite
    }

    public class TextTestData<T>
    {
        public T Value { get; }
        public string String { get; }
        public TestType Type { get; }

        public TextTestData(TestType type, string str, T value)
        {
            Type = type;
            String = str;
            Value = value;
        }
    }

    public abstract class BaseTest<T>
    {
        private readonly Utf8Serializer _serializer;
        private readonly IEqualityComparer<T> _comparer;

        public BaseTest(IEqualityComparer<T> comparer = null)
        {
            _serializer = new Utf8Serializer();
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        protected void RunTest(TextTestData<T> test, ValueConverter<T> converter = null)
        {
            switch (test.Type)
            {
                case TestType.FailRead:
                    Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>(test.String, converter));
                    break;
                case TestType.FailWrite:
                    Assert.Throws<SerializationException>(() => _serializer.WriteUtf16String(test.Value, converter));
                    break;
                case TestType.Read:
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String, converter), _comparer);
                    break;
                case TestType.Write:
                    Assert.Equal(test.String, _serializer.WriteUtf16String(test.Value, converter));
                    break;
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String, converter), _comparer);
                    Assert.Equal(test.String, _serializer.WriteUtf16String(test.Value, converter));
                    break;
            }
        }

        public static object[] FailRead(string str)
          => new object[] { new TextTestData<T>(TestType.FailRead, str, default) };
        public static object[] FailWrite(T value)
          => new object[] { new TextTestData<T>(TestType.FailWrite, default, value) };
        public static object[] Read(string str, T value)
          => new object[] { new TextTestData<T>(TestType.Read, str, value) };
        public static object[] Write(string str, T value)
          => new object[] { new TextTestData<T>(TestType.Write, str, value) };
        public static object[] ReadWrite(string str, T value)
          => new object[] { new TextTestData<T>(TestType.ReadWrite, str, value) };
    }
}
