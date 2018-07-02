using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
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

        public TestData(TestType type, EtfTokenType tokenType, IEnumerable<byte> bytes, T value)
        {
            Type = type;
            Bytes = new ReadOnlyMemory<byte>(new byte[] { 131, (byte)tokenType }.Concat(bytes).ToArray());
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

        public static object[] FailRead(EtfTokenType tokenType, IEnumerable<byte> bytes)
          => new object[] { new TestData<T>(TestType.FailRead, tokenType, bytes, default) };
        public static object[] FailWrite(T value)
          => new object[] { new TestData<T>(TestType.FailWrite, EtfTokenType.None, default, value) };
        public static object[] Read(EtfTokenType tokenType, IEnumerable<byte> bytes, T value)
          => new object[] { new TestData<T>(TestType.Read, tokenType, bytes, value) };
        public static object[] Write(EtfTokenType tokenType, IEnumerable<byte> bytes, T value)
          => new object[] { new TestData<T>(TestType.Write, tokenType, bytes, value) };
        public static object[] ReadWrite(EtfTokenType tokenType, IEnumerable<byte> bytes, T value)
          => new object[] { new TestData<T>(TestType.ReadWrite, tokenType, bytes, value) };
    }
}
