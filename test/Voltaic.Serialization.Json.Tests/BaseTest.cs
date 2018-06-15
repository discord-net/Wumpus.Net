using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public enum TestType
    {
        Fail,
        Read,
        ReadWrite
    }

    public abstract class BaseTest<T>
    {
        private readonly JsonSerializer _serializer;
        private readonly IEqualityComparer<T> _comparer;

        public BaseTest(IEqualityComparer<T> comparer = null)
        {
            _serializer = new JsonSerializer();
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        protected void RunTest(TestData test, ValueConverter<T> converter = null)
        {
            switch (test.Type)
            {
                case TestType.Fail:
                    Assert.Throws<SerializationException>(() => _serializer.Read<T>(test.String, converter));
                    break;
                case TestType.Read:
                    Assert.Equal(test.Value, _serializer.Read<T>(test.String, converter), _comparer);
                    break;
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.Read<T>(test.String, converter), _comparer);
                    Assert.Equal(test.String, _serializer.WriteString(test.Value, converter));
                    break;
            }
        }

        protected void RunQuoteTest(TestData test, ValueConverter<T> converter = null)
        {
            switch (test.Type)
            {
                case TestType.Fail:
                    Assert.Throws<SerializationException>(() => _serializer.Read<T>('"' + test.String + '"', converter));
                    break;
                case TestType.Read:
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.Read<T>('"' + test.String + '"', converter), _comparer);
                    Assert.Throws<SerializationException>(() => _serializer.Read<T>('"' + test.String, converter));
                    break;
            }
        }

        protected void RunWhitespaceTest(TestData test, ValueConverter<T> converter = null)
        {
            switch (test.Type)
            {
                case TestType.Fail:
                    Assert.Throws<SerializationException>(() => _serializer.Read<T>(' ' + test.String + ' ', converter));
                    break;
                case TestType.Read:
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.Read<T>(' ' + test.String, converter), _comparer);
                    Assert.Equal(test.Value, _serializer.Read<T>(test.String + ' ', converter), _comparer);
                    Assert.Equal(test.Value, _serializer.Read<T>(' ' + test.String + ' ', converter), _comparer);
                    break;
            }
        }

        public class TestData
        {
            public T Value { get; }
            public string String { get; }
            public TestType Type { get; }

            public TestData(TestType type, string str, T value, IEqualityComparer<T> comparer = null)
            {
                Type = type;
                String = str;
                Value = value;
            }
        }

        public static object[] Fail(string str)
          => new object[] { new TestData(TestType.Fail, str, default(T)) };
        public static object[] Read(string str, T value)
          => new object[] { new TestData(TestType.Read, str, value) };
        public static object[] ReadWrite(string str, T value)
          => new object[] { new TestData(TestType.ReadWrite, str, value) };
    }
}
