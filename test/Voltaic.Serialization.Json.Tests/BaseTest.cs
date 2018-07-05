using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public abstract class BaseTest<T>
    {
        private readonly JsonSerializer _serializer;
        private readonly IEqualityComparer<T> _comparer;

        public BaseTest(IEqualityComparer<T> comparer = null)
        {
            _serializer = new JsonSerializer();
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        protected void RunTest(TextTestData<T> test, ValueConverter<T> converter = null)
        {
            switch (test.Type)
            {
                case TestType.FailRead:
                    Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>(test.String, converter));
                    Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>(' ' + test.String + ' ', converter));
                    break;
                case TestType.FailWrite:
                    Assert.Throws<SerializationException>(() => _serializer.WriteUtf16String(test.Value, converter));
                    break;
                case TestType.Read:
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String, converter), _comparer);
                    Assert.True(TestSkip(test.String));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(' ' + test.String, converter), _comparer);
                    Assert.True(TestSkip(' ' + test.String));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String + ' ', converter), _comparer);
                    Assert.True(TestSkip(test.String + ' '));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(' ' + test.String + ' ', converter), _comparer);
                    Assert.True(TestSkip(' ' + test.String + ' '));
                    break;
                case TestType.Write:
                    Assert.Equal(test.String, _serializer.WriteUtf16String(test.Value, converter));
                    Assert.True(TestSkip(test.String));
                    break;
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String, converter), _comparer);
                    Assert.True(TestSkip(test.String));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(' ' + test.String, converter), _comparer);
                    Assert.True(TestSkip(' ' + test.String));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(test.String + ' ', converter), _comparer);
                    Assert.True(TestSkip(test.String + ' '));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(' ' + test.String + ' ', converter), _comparer);
                    Assert.True(TestSkip(' ' + test.String + ' '));
                    Assert.Equal(test.String, _serializer.WriteUtf16String(test.Value, converter));
                    break;
            }
        }

        protected void RunQuoteTest(TextTestData<T> test, ValueConverter<T> converter = null, bool onlyReads = false)
        {
            switch (test.Type)
            {
                case TestType.FailRead:
                    Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>('"' + test.String + '"', converter));
                    break;
                case TestType.FailWrite:
                    Assert.Throws<SerializationException>(() => _serializer.WriteUtf16String(test.Value, converter));
                    break;
                case TestType.Read:
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>('"' + test.String + '"', converter), _comparer);
                    Assert.True(TestSkip('"' + test.String + '"'));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(" \"" + test.String + "\" ", converter), _comparer);
                    Assert.True(TestSkip(" \"" + test.String + "\" "));
                    Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>('"' + test.String, converter)); // Unclosed quote
                    Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>(" \"" + test.String + ' ', converter)); // Unclosed quote
                    break;
                case TestType.ReadWrite:
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>('"' + test.String + '"', converter), _comparer);
                    Assert.True(TestSkip('"' + test.String + '"'));
                    Assert.Equal(test.Value, _serializer.ReadUtf16<T>(" \"" + test.String + "\" ", converter), _comparer);
                    Assert.True(TestSkip(" \"" + test.String + "\" "));
                    Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>('"' + test.String, converter)); // Unclosed quote
                    Assert.Throws<SerializationException>(() => _serializer.ReadUtf16<T>(" \"" + test.String + ' ', converter)); // Unclosed quote
                    if (!onlyReads)
                        Assert.Equal('"' + test.String + '"', _serializer.WriteUtf16String(test.Value, converter));
                    break;
                case TestType.Write:
                    if (!onlyReads)
                        Assert.Equal('"' + test.String + '"', _serializer.WriteUtf16String(test.Value, converter));
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

        private static bool TestSkip(string str)
        {
            var utf16Bytes = MemoryMarshal.AsBytes(str.AsSpan());
            if (Encodings.Utf16.ToUtf8Length(utf16Bytes, out int count) != OperationStatus.Done)
                throw new SerializationException("Failed to convert to UTF8");
            var utf8Bytes = new byte[count];
            if (Encodings.Utf16.ToUtf8(utf16Bytes, utf8Bytes.AsSpan(), out _, out _) != OperationStatus.Done)
                throw new SerializationException("Failed to convert to UTF8");
            var span = new ReadOnlySpan<byte>(utf8Bytes);
            if (!JsonReader.Skip(ref span, out var skipped))
                return false;

            int whitespace = 0;
            if (span.Length != 0)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    switch (span[i])
                    {
                        case (byte)' ': // Whitespace
                        case (byte)'\n':
                        case (byte)'\r':
                        case (byte)'\t':
                            whitespace++;
                            break;
                        default:
                            return false;
                    }
                }
            }
            if (skipped.Length != utf8Bytes.Length - whitespace)
                return false;
            return true;
        }
    }
}
