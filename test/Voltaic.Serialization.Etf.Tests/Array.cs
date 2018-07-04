using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class ArrayTests : BaseTest<int[]>
    {
        private class Comparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y) => (x == null && y == null) || (x != null && y != null && x.SequenceEqual(y));
            public int GetHashCode(int[] obj) => 0; // Ignore
        }

        public static IEnumerable<object[]> GetData()
        {
            yield return FailRead(EtfTokenType.List, new byte[] { });
            yield return FailRead(EtfTokenType.Binary, new byte[] { });
            yield return FailRead(EtfTokenType.String, new byte[] { });
            yield return ReadWrite(EtfTokenType.SmallAtom, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return ReadWrite(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x6A }, new int[0]);
            yield return Read(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x00 }, new int[0]);
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x00 }, new int[0]);

            yield return FailRead(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x61, 0x00 }); // w/ tail element
            yield return FailRead(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x00 }); // incomplete
            yield return FailRead(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x6A }); // incomplete
            yield return FailRead(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x01 }); // incomplete
            yield return FailRead(EtfTokenType.LargeTuple, new byte[] { 0x00, 0x00, 0x00, 0x01 }); // incomplete
            yield return FailRead(EtfTokenType.String, new byte[] { 0x00, 0x01 }); // incomplete
            yield return FailRead(EtfTokenType.SmallTuple, new byte[] {  0x01 }); // incomplete

            foreach (var x in CollectionTests.Reads(new int[0])) yield return x;
            foreach (var x in CollectionTests.Reads(new int[] { 1 })) yield return x;
            foreach (var x in CollectionTests.Reads(new int[] { 1, 2, 3 })) yield return x;

            yield return Write(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x61, 0x01, 0x6A }, new int[] { 1 });
            yield return Write(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x03, 0x61, 0x01, 0x61, 0x02, 0x61, 0x03, 0x6A }, new int[] { 1, 2, 3 });
        }

        public ArrayTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Array(BinaryTestData<int[]> data) => RunTest(data);
    }

    public class ListTests : BaseTest<List<int>>
    {
        private class Comparer : IEqualityComparer<List<int>>
        {
            public bool Equals(List<int> x, List<int> y) => (x == null && y == null) || (x != null && y != null && x.SequenceEqual(y));
            public int GetHashCode(List<int> obj) => 0; // Ignore
        }

        public static IEnumerable<object[]> GetData()
        {
            yield return FailRead(EtfTokenType.List, new byte[] { });
            yield return FailRead(EtfTokenType.Binary, new byte[] { });
            yield return FailRead(EtfTokenType.String, new byte[] { });
            yield return ReadWrite(EtfTokenType.SmallAtom, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return ReadWrite(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x6A }, new List<int>());
            yield return Read(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x00 }, new List<int>());
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x00 }, new List<int>());

            yield return FailRead(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x61, 0x00 }); // w/ tail element
            yield return FailRead(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x00 }); // incomplete
            yield return FailRead(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x6A }); // incomplete
            yield return FailRead(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x01 }); // incomplete
            yield return FailRead(EtfTokenType.LargeTuple, new byte[] { 0x00, 0x00, 0x00, 0x01 }); // incomplete
            yield return FailRead(EtfTokenType.String, new byte[] { 0x00, 0x01 }); // incomplete
            yield return FailRead(EtfTokenType.SmallTuple, new byte[] { 0x01 }); // incomplete

            foreach (var x in CollectionTests.Reads(new int[0], new List<int>())) yield return x;
            foreach (var x in CollectionTests.Reads(new int[] { 1 }, new List<int>() { 1 })) yield return x;
            foreach (var x in CollectionTests.Reads(new int[] { 1, 2, 3 }, new List<int>() { 1, 2, 3 })) yield return x;

            yield return Write(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x61, 0x01, 0x6A }, new List<int>() { 1 });
            yield return Write(EtfTokenType.List, new byte[] { 0x00, 0x00, 0x00, 0x03, 0x61, 0x01, 0x61, 0x02, 0x61, 0x03, 0x6A }, new List<int>() { 1, 2, 3 });
        }

        public ListTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Array(BinaryTestData<List<int>> data) => RunTest(data);
    }

    internal class CollectionTests
    {
        public static IEnumerable<object[]> Reads(int[] value)
            => CreateTests(TestType.Read, value, value);
        public static IEnumerable<object[]> Reads<T>(int[] value, T expectedValue)
            => CreateTests(TestType.Read, value, expectedValue);
        public static IEnumerable<object[]> CreateTests<T>(TestType type, int[] value, T expectedValue)
        {
            byte[] data = value.Select(x => (byte)x).SelectMany(x => new byte[] { 0x61, x }).ToArray();
            byte[] stringData = value.Select(x => (byte)x).ToArray();

            if (value.Length <= byte.MaxValue)
            {
                var header = new byte[] { (byte)value.Length };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallTuple, header.Concat(data), expectedValue) };
            }
            if (value.Length <= ushort.MaxValue)
            {
                var header = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(header, (ushort)value.Length);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.String, header.Concat(stringData), expectedValue) };
            }
            // if (value.Length <= uint.MaxValue)
            {
                var header = new byte[4];
                BinaryPrimitives.WriteUInt32BigEndian(header, (uint)value.Length);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.Binary, header.Concat(stringData), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeTuple, header.Concat(data), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.List, header.Concat(data).Concat(new byte[] { 0x6A }), expectedValue) };
            }
        }
    }
}
