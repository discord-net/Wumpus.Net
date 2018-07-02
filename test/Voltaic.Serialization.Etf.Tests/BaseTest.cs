using System;
using System.Collections.Generic;
using Voltaic.Serialization.Etf;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
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
        public ReadOnlyMemory<byte> Bytes { get; }
        public TestType Type { get; }

        public TestData(TestType type, byte[] bytes, T value)
        {
            Type = type;
            Bytes = bytes;
            Value = value;
        }
    }

    public abstract class BaseTest<T>
    {
        private readonly EtfSerializer _serializer;
        private readonly IEqualityComparer<T> _comparer;

        public BaseTest(IEqualityComparer<T> comparer = null)
        {
            _serializer = new EtfSerializer();
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        protected void RunTest(TestData<T> test, ValueConverter<T> converter = null)
        {
            switch (test.Type)
            {
                case TestType.FailRead:
                    Assert.Throws<SerializationException>(() => _serializer.Read(test.Bytes, converter));
                    break;
                case TestType.FailWrite:
                    Assert.Throws<SerializationException>(() => _serializer.Write(test.Value, converter));
                    break;
                case TestType.Read:
                    Assert.Equal(test.Value, _serializer.Read(test.Bytes, converter), _comparer);
                    break;
                case TestType.Write:
                    Assert.True(test.Bytes.Span.SequenceEqual(_serializer.Write(test.Value, converter).AsReadOnlySpan()));
                    break;
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.Read(test.Bytes, converter), _comparer);
                    Assert.True(test.Bytes.Span.SequenceEqual(_serializer.Write(test.Value, converter).AsReadOnlySpan()));
                    break;
            }
        }

        public static object[] FailRead(byte[] bytes)
          => new object[] { new TestData<T>(TestType.FailRead, bytes, default) };
        public static object[] FailWrite(T value)
          => new object[] { new TestData<T>(TestType.FailWrite, default, value) };
        public static object[] Read(byte[] bytes, T value)
          => new object[] { new TestData<T>(TestType.Read, bytes, value) };
        public static object[] Write(byte[] bytes, T value)
          => new object[] { new TestData<T>(TestType.Write, bytes, value) };
        public static object[] ReadWrite(byte[] bytes, T value)
          => new object[] { new TestData<T>(TestType.ReadWrite, bytes, value) };
    }
}
