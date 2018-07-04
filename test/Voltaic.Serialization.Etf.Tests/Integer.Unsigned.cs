using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class ByteTests : BaseTest<byte>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            FailRead(EtfTokenType.SmallBig, new byte[] { 0x01, 0x01, 0x01 }); // Min - 1
            FailRead(EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x01, 0x01 }); // Min - 1
            foreach (var x in UnsignedIntHelpers.ReadWrites<byte>(0, 0)) yield return x;
            foreach (var x in UnsignedIntHelpers.ReadWrites<byte>(255, 255)) yield return x; // Max
            foreach (var x in UnsignedIntHelpers.FailReads<byte>(256)) ; // Max + 1
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.ByteTests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.ByteTests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.ByteTests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<byte> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<byte> data) => RunTest(data, new ByteEtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<byte> data) => RunTest(data, new ByteEtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<byte> data) => RunTest(data, new ByteEtfConverter('X'));
    }

    public class UInt16Tests : BaseTest<ushort>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            FailRead(EtfTokenType.SmallBig, new byte[] { 0x01, 0x01, 0x01 }); // Min - 1
            FailRead(EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x01, 0x01 }); // Min - 1
            foreach (var x in UnsignedIntHelpers.ReadWrites<ushort>(0, 0)) yield return x;
            foreach (var x in UnsignedIntHelpers.ReadWrites<ushort>(65535, 65535)) yield return x; // Max
            foreach (var x in UnsignedIntHelpers.FailReads<ushort>(65536)) ; // Max + 1
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.UInt16Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.UInt16Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.UInt16Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<ushort> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<ushort> data) => RunTest(data, new UInt16EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<ushort> data) => RunTest(data, new UInt16EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<ushort> data) => RunTest(data, new UInt16EtfConverter('X'));
    }

    public class UInt32Tests : BaseTest<uint>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            FailRead(EtfTokenType.SmallBig, new byte[] { 0x01, 0x01, 0x01 }); // Min - 1
            FailRead(EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x01, 0x01 }); // Min - 1
            foreach (var x in UnsignedIntHelpers.ReadWrites<uint>(0, 0)) yield return x;
            foreach (var x in UnsignedIntHelpers.ReadWrites<uint>(4294967295, 4294967295)) yield return x; // Max
            foreach (var x in UnsignedIntHelpers.FailReads<uint>(4294967296)) ; // Max + 1
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.UInt32Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.UInt32Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.UInt32Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<uint> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<uint> data) => RunTest(data, new UInt32EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<uint> data) => RunTest(data, new UInt32EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<uint> data) => RunTest(data, new UInt32EtfConverter('X'));
    }

    public class UInt64Tests : BaseTest<ulong>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            FailRead(EtfTokenType.SmallBig, new byte[] { 0x01, 0x01, 0x01 }); // Min - 1
            FailRead(EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x01, 0x01 }); // Min - 1
            foreach (var x in UnsignedIntHelpers.ReadWrites<ulong>(0, 0)) yield return x;
            foreach (var x in UnsignedIntHelpers.ReadWrites<ulong>(18446744073709551615, 18446744073709551615)) yield return x; // Max
            FailRead(EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x09, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }); // Max + 1
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.UInt64Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.UInt64Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.UInt64Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<ulong> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<ulong> data) => RunTest(data, new UInt64EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<ulong> data) => RunTest(data, new UInt64EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<ulong> data) => RunTest(data, new UInt64EtfConverter('X'));
    }

    internal class UnsignedIntHelpers
    {
        public static IEnumerable<object[]> FailReads<T>(ulong value)
            => CreateTests(TestType.ReadWrite, value, default(T));
        public static IEnumerable<object[]> ReadWrites<T>(ulong value, T expectedValue)
            => CreateTests(TestType.ReadWrite, value, expectedValue);
        public static IEnumerable<object[]> CreateTests<T>(TestType type, ulong value, T expectedValue)
        {
            if (value <= byte.MaxValue)
            {
                var byteValue = new byte[] { (byte)value };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallInteger, byteValue, expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x01, 0x00 }.Concat(byteValue), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00 }.Concat(byteValue), expectedValue) };
            }
            if (value <= (ushort)short.MaxValue)
            {
                var shortValue = new byte[2];
                BinaryPrimitives.WriteInt16BigEndian(shortValue, (short)value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.Integer, shortValue, expectedValue) };
            }
            if (value <= ushort.MaxValue)
            {
                var ushortValue = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(ushortValue, (ushort)value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x02, 0x00 }.Concat(ushortValue), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x02, 0x00 }.Concat(ushortValue), expectedValue) };
            }
            if (value <= uint.MaxValue)
            {
                var uintValue = new byte[4];
                BinaryPrimitives.WriteUInt32BigEndian(uintValue, (uint)value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x04, 0x00 }.Concat(uintValue), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x04, 0x00 }.Concat(uintValue), expectedValue) };

                // Test non-standard integer sizes
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x07, 0x00, 0x00, 0x00, 0x00 }.Concat(uintValue), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00 }.Concat(uintValue), expectedValue) };
            }
            // if (value <= ulong.MaxValue)
            {
                var ulongValue = new byte[8];
                BinaryPrimitives.WriteUInt64BigEndian(ulongValue, value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x08, 0x00 }.Concat(ulongValue), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x08, 0x00 }.Concat(ulongValue), expectedValue) };
            }
        }
    }
}
