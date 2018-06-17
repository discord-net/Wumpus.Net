using System.Collections.Generic;
using Voltaic.Serialization.Tests;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public abstract class BaseTest<T>
    {
        private readonly Utf8Serializer _serializer;
        private readonly IEqualityComparer<T> _comparer;

        public BaseTest(IEqualityComparer<T> comparer = null)
        {
            _serializer = new Utf8Serializer();
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        protected void RunTest(TestData<T> test, ValueConverter<T> converter = null)
        {
            switch (test.Type)
            {
                case TestType.FailRead:
                    Assert.Throws<SerializationException>(() => _serializer.Read<T>(test.String, converter));
                    break;
                case TestType.FailWrite:
                    Assert.Throws<SerializationException>(() => _serializer.WriteString(test.Value, converter));
                    break;
                case TestType.Read:
                    Assert.Equal(test.Value, _serializer.Read<T>(test.String, converter), _comparer);
                    break;
                case TestType.Write:
                    Assert.Equal(test.String, _serializer.WriteString(test.Value, converter));
                    break;
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.Read<T>(test.String, converter), _comparer);
                    Assert.Equal(test.String, _serializer.WriteString(test.Value, converter));
                    break;
            }
        }


        public static object[] FailRead(string str)
          => new object[] { new TestData<T>(TestType.FailRead, str, default) };
        public static object[] FailWrite(T value)
          => new object[] { new TestData<T>(TestType.FailWrite, default, value) };
        public static object[] Read(string str, T value)
          => new object[] { new TestData<T>(TestType.Read, str, value) };
        public static object[] Write(string str, T value)
          => new object[] { new TestData<T>(TestType.Write, str, value) };
        public static object[] ReadWrite(string str, T value)
          => new object[] { new TestData<T>(TestType.ReadWrite, str, value) };
    }
}
