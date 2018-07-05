using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class SByteTests : BaseTest<sbyte>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            foreach (var x in SignedIntHelpers.FailReads<sbyte>(-129)) ; // Min - 1
            foreach (var x in SignedIntHelpers.Reads<sbyte>(-128, -128)) yield return x; // Min
            foreach (var x in SignedIntHelpers.Reads<sbyte>(0, 0)) yield return x;
            foreach (var x in SignedIntHelpers.Reads<sbyte>(127, 127)) yield return x; // Max
            foreach (var x in SignedIntHelpers.FailReads<sbyte>(128)) ; // Max + 1

            yield return Write(EtfTokenType.Integer, new byte[] { 0xFF, 0xFF, 0xFF, 0x80 }, -128);
            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0x00 }, 0);
            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0x7F }, 127);
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.SByteTests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.SByteTests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.SByteTests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<sbyte> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<sbyte> data) => RunTest(data, new SByteEtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<sbyte> data) => RunTest(data, new SByteEtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<sbyte> data) => RunTest(data, new SByteEtfConverter('X'));
    }

    public class Int16Tests : BaseTest<short>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            foreach (var x in SignedIntHelpers.FailReads<short>(-32769)) ; // Min - 1
            foreach (var x in SignedIntHelpers.Reads<short>(-32768, -32768)) yield return x; // Min
            foreach (var x in SignedIntHelpers.Reads<short>(0, 0)) yield return x;
            foreach (var x in SignedIntHelpers.Reads<short>(32767, 32767)) yield return x; // Max
            foreach (var x in SignedIntHelpers.FailReads<short>(32768)) ; // Max + 1

            yield return Write(EtfTokenType.Integer, new byte[] { 0xFF, 0xFF, 0x80, 0x00 }, -32768);
            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0x00 }, 0);
            yield return Write(EtfTokenType.Integer, new byte[] { 0x00, 0x00, 0x7F, 0xFF }, 32767);
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.Int16Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.Int16Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.Int16Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<short> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<short> data) => RunTest(data, new Int16EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<short> data) => RunTest(data, new Int16EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<short> data) => RunTest(data, new Int16EtfConverter('X'));
    }

    public class Int32Tests : BaseTest<int>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            foreach (var x in SignedIntHelpers.FailReads<int>(-2147483649)) ; // Min - 1
            foreach (var x in SignedIntHelpers.Reads<int>(-2147483648, -2147483648)) yield return x; // Min
            foreach (var x in SignedIntHelpers.Reads<int>(0, 0)) yield return x;
            foreach (var x in SignedIntHelpers.Reads<int>(2147483647, 2147483647)) yield return x; // Max
            foreach (var x in SignedIntHelpers.FailReads<int>(2147483648)) ; // Max + 1

            yield return Write(EtfTokenType.Integer, new byte[] { 0x80, 0x00, 0x00, 0x00 }, -2147483648);
            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0x00 }, 0);
            yield return Write(EtfTokenType.Integer, new byte[] { 0x7F, 0xFF, 0xFF, 0xFF }, 2147483647);
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.Int32Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.Int32Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.Int32Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<int> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<int> data) => RunTest(data, new Int32EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<int> data) => RunTest(data, new Int32EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<int> data) => RunTest(data, new Int32EtfConverter('X'));
    }

    public class Int64Tests : BaseTest<long>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            yield return FailRead(EtfTokenType.SmallBig, new byte[] { 0x08, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80 }); // Min - 1
            yield return FailRead(EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x08, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80 }); // Min - 1
            foreach (var x in SignedIntHelpers.Reads<long>(-9223372036854775808, -9223372036854775808)) yield return x; // Min
            foreach (var x in SignedIntHelpers.Reads<long>(0, 0)) yield return x;
            foreach (var x in SignedIntHelpers.Reads<long>(9223372036854775807, 9223372036854775807)) yield return x; // Max
            yield return FailRead(EtfTokenType.LargeBig, new byte[] { 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80 }); // Max + 1
            yield return FailRead(EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80 }); // Max + 1

            yield return Write(EtfTokenType.SmallBig, new byte[] { 0x08, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80 }, -9223372036854775808);
            yield return Write(EtfTokenType.SmallInteger, new byte[] { 0x00 }, 0);
            yield return Write(EtfTokenType.SmallBig, new byte[] { 0x08, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x7F }, 9223372036854775807);
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.Int64Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.Int64Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.Int64Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<long> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<long> data) => RunTest(data, new Int64EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<long> data) => RunTest(data, new Int64EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<long> data) => RunTest(data, new Int64EtfConverter('X'));
    }

    internal class SignedIntHelpers
    {
        public static IEnumerable<object[]> FailReads<T>(long value)
            => CreateTests(TestType.FailRead, value, default(T));
        public static IEnumerable<object[]> Reads<T>(long value, T expectedValue)
            => CreateTests(TestType.Read, value, expectedValue);
        public static IEnumerable<object[]> CreateTests<T>(TestType type, long value, T expectedValue)
        {
            byte signByte = value >= 0 ? (byte)0x00 : (byte)0x01;
            ulong absValue = value >= 0 ? (ulong)value : (value != long.MinValue ? (ulong)-value : 9223372036854775808);

            if (value >= byte.MinValue && value <= byte.MaxValue)
            {
                var byteValue = new byte[] { (byte)absValue };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallInteger, byteValue, expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x01, signByte }.Concat(byteValue), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x01, signByte }.Concat(byteValue), expectedValue) };
            }
            if (value >= short.MinValue && value <= short.MaxValue)
            {
                var ushortValueBe = new byte[2];
                var ushortValueLe = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(ushortValueBe, (ushort)absValue);
                BinaryPrimitives.WriteUInt16LittleEndian(ushortValueLe, (ushort)absValue);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x02, signByte }.Concat(ushortValueLe), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x02, signByte }.Concat(ushortValueLe), expectedValue) };
            }
            if (absValue <= int.MaxValue)
            {
                var intValueBe = new byte[4];
                BinaryPrimitives.WriteInt32BigEndian(intValueBe, (int)value);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.Integer, intValueBe, expectedValue) };
            }
            if (absValue <= uint.MaxValue)
            {
                var uintValueBe = new byte[4];
                var uintValueLe = new byte[4];
                BinaryPrimitives.WriteUInt32BigEndian(uintValueBe, (uint)absValue);
                BinaryPrimitives.WriteUInt32LittleEndian(uintValueLe, (uint)absValue);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x04, signByte }.Concat(uintValueLe), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x04, signByte }.Concat(uintValueLe), expectedValue) };

                // Test non-standard integer sizes
                var uint56ValueLe = new byte[7];
                BinaryPrimitives.WriteUInt32LittleEndian(uint56ValueLe, (uint)absValue);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x07, signByte }.Concat(uint56ValueLe), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x07, signByte }.Concat(uint56ValueLe), expectedValue) };
            }
            // if (absValue <= ulong.MaxValue)
            {
                var ulongValueLe = new byte[8];
                BinaryPrimitives.WriteUInt64LittleEndian(ulongValueLe, absValue);
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.SmallBig, new byte[] { 0x08, signByte }.Concat(ulongValueLe), expectedValue) };
                yield return new object[] { new BinaryTestData<T>(type, EtfTokenType.LargeBig, new byte[] { 0x00, 0x00, 0x00, 0x08, signByte }.Concat(ulongValueLe), expectedValue) };
            }
        }
    }
}
