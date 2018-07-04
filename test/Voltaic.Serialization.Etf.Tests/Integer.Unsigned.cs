using System;
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
            foreach (var x in UnsignedIntHelpers.Reads<byte>(0, 0)) yield return x;
            foreach (var x in UnsignedIntHelpers.Reads<byte>(255, 255)) yield return x; // Max
            foreach (var x in UnsignedIntHelpers.FailReads<byte>(256)) ; // Max + 1

            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0x00 }, 0);
            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0xFF }, 255);
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
            foreach (var x in UnsignedIntHelpers.Reads<ushort>(0, 0)) yield return x;
            foreach (var x in UnsignedIntHelpers.Reads<ushort>(65535, 65535)) yield return x; // Max
            foreach (var x in UnsignedIntHelpers.FailReads<ushort>(65536)) ; // Max + 1
            
            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0x00 }, 0);
            yield return Write(EtfTokenType.Integer, new byte[] { 0x00, 0x00, 0xFF, 0xFF }, 65535);
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
            foreach (var x in UnsignedIntHelpers.Reads<uint>(0, 0)) yield return x;
            foreach (var x in UnsignedIntHelpers.Reads<uint>(4294967295, 4294967295)) yield return x; // Max
            foreach (var x in UnsignedIntHelpers.FailReads<uint>(4294967296)) ; // Max + 1

            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0x00 }, 0);
            yield return Write(EtfTokenType.Integer, new byte[] { 0x7F, 0xFF, 0xFF, 0xFF }, 2147483647);
            yield return Write(EtfTokenType.SmallBig, new byte[] { 0x04, 0x00, 0xFF, 0xFF, 0xFF, 0xFF }, 4294967295);
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
            foreach (var x in UnsignedIntHelpers.Reads<ulong>(0, 0)) yield return x;
            foreach (var x in UnsignedIntHelpers.Reads<ulong>(18446744073709551615, 18446744073709551615)) yield return x; // Max
            FailRead(EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x09, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }); // Max + 1

            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0x00 }, 0);
            yield return Write(EtfTokenType.SmallBig, new byte[] { 0x08, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, 18446744073709551615);
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
            => CreateTests(TestType.FailRead, value, default(T));
        public static IEnumerable<object[]> Reads<T>(ulong value, T expectedValue)
            => CreateTests(TestType.Read, value, expectedValue);
        public static IEnumerable<object[]> CreateTests<T>(TestType type, ulong value, T expectedValue)
        {
            if (value <= byte.MaxValue)
            {
                var byteValue = new byte[] { (byte)value };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallInteger, byteValue, expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x01, 0x00 }.Concat(byteValue), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00 }.Concat(byteValue), expectedValue) };
            }
            if (value <= ushort.MaxValue)
            {
                var ushortValueBe = new byte[2];
                var ushortValueLe = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(ushortValueBe, (ushort)value);
                BinaryPrimitives.WriteUInt16LittleEndian(ushortValueLe, (ushort)value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x02, 0x00 }.Concat(ushortValueLe), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x02, 0x00 }.Concat(ushortValueLe), expectedValue) };
            }
            if (value <= int.MaxValue)
            {
                var shortValueBe = new byte[4];
                BinaryPrimitives.WriteInt32BigEndian(shortValueBe, (int)value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.Integer, shortValueBe, expectedValue) };
            }
            if (value <= uint.MaxValue)
            {
                var uintValueBe = new byte[4];
                var uintValueLe = new byte[4];
                BinaryPrimitives.WriteUInt32BigEndian(uintValueBe, (uint)value);
                BinaryPrimitives.WriteUInt32LittleEndian(uintValueLe, (uint)value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x04, 0x00 }.Concat(uintValueLe), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x04, 0x00 }.Concat(uintValueLe), expectedValue) };

                // Test non-standard integer sizes
                var uint56ValueLe = new byte[7];
                BinaryPrimitives.WriteUInt32LittleEndian(uint56ValueLe, (uint)value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x07, 0x00 }.Concat(uint56ValueLe), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x07, 0x00 }.Concat(uint56ValueLe), expectedValue) };
            }
            // if (value <= ulong.MaxValue)
            {
                var ulongValueLe = new byte[8];
                BinaryPrimitives.WriteUInt64LittleEndian(ulongValueLe, value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x08, 0x00 }.Concat(ulongValueLe), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x08, 0x00 }.Concat(ulongValueLe), expectedValue) };
            }
        }
    }
}
