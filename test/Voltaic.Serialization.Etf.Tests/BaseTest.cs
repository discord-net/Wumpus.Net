using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class BinaryTestData<T>
    {
        public T Value { get; }
        public ReadOnlyMemory<byte> Bytes { get; }
        public TestType Type { get; }

        public BinaryTestData(TestType type, EtfTokenType tokenType, IEnumerable<byte> bytes, T value)
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

        protected void RunTest(BinaryTestData<T> test, ValueConverter<T> converter = null)
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
                    Assert.True(TestSkip(test.Bytes));
                    break;
                case TestType.Write:
                    Assert.True(test.Bytes.Span.SequenceEqual(_serializer.Write(test.Value, converter).AsReadOnlySpan()));
                    break;
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.Read(test.Bytes, converter), _comparer);
                    Assert.True(TestSkip(test.Bytes));
                    Assert.True(test.Bytes.Span.SequenceEqual(_serializer.Write(test.Value, converter).AsReadOnlySpan()));
                    break;
            }
        }

        public static object[] FailRead(EtfTokenType tokenType, byte[] bytes)
          => new object[] { new BinaryTestData<T>(TestType.FailRead, tokenType, bytes, default) };
        public static object[] FailWrite(T value)
          => new object[] { new BinaryTestData<T>(TestType.FailWrite, EtfTokenType.None, default, value) };
        public static object[] Read(EtfTokenType tokenType, byte[] bytes, T value)
          => new object[] { new BinaryTestData<T>(TestType.Read, tokenType, bytes, value) };
        public static object[] Write(EtfTokenType tokenType, byte[] bytes, T value)
          => new object[] { new BinaryTestData<T>(TestType.Write, tokenType, bytes, value) };
        public static object[] ReadWrite(EtfTokenType tokenType, byte[] bytes, T value)
          => new object[] { new BinaryTestData<T>(TestType.ReadWrite, tokenType, bytes, value) };

        public static IEnumerable<object[]> FailReads(string str)
            => CreateStringTests(TestType.FailRead, str, default);
        public static IEnumerable<object[]> Reads(string str, T value)
            => CreateStringTests(TestType.Read, str, value);
        private static IEnumerable<object[]> CreateStringTests(TestType type, string str, T value)
        {
            var utf8 = Encoding.UTF8.GetBytes(str);
            if (utf8.Length <= byte.MaxValue)
            {
                var header = new byte[] { (byte)utf8.Length };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallAtom, header.Concat(utf8), value) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallAtomUtf8, header.Concat(utf8), value) };
            }
            if (utf8.Length <= ushort.MaxValue )
            {
                var header = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(header, (ushort)utf8.Length);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.Atom, header.Concat(utf8), value) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.AtomUtf8, header.Concat(utf8), value) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.String, header.Concat(utf8), value) };
            }
            // if (utf8.Length <= uint.MaxValue)
            {
                var header = new byte[4];
                BinaryPrimitives.WriteUInt32BigEndian(header, (uint)utf8.Length);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.Binary, header.Concat(utf8), value) };
            }
        }
        protected static IEnumerable<object[]> TextToBinary(IEnumerable<object[]> tests)
        {
            foreach (var test in tests)
            {
                var textTest = test[0] as TextTestData<T>;
                var type = textTest.Type;
                switch (type)
                {
                    case TestType.FailWrite:
                    case TestType.Write:
                        continue;
                    case TestType.ReadWrite:
                        type = TestType.Read;
                        break;
                }
                foreach (var x in CreateStringTests(type, textTest.String, textTest.Value))
                    yield return x;
            }
        }

        private static bool TestSkip(ReadOnlyMemory<byte> bytes)
        {
            var span = bytes.Span.Slice(1);
            if (!EtfReader.Skip(ref span, out var skipped))
                return false;
            if (span.Length != 0)
                return false;
            if (skipped.Length != bytes.Length - 1)
                return false;
            return true;
        }
    }
}
